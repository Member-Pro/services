using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Configuration;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MemberPro.Core.Services.Members
{
    public interface IMemberService
    {
        Task<MemberModel> FindByIdAsync(int id);
        Task<MemberModel> FindByEmailAsync(string email);
        Task<MemberModel> FindBySubjectIdAsync(string subjectId);

        Task<IEnumerable<MemberModel>> SearchAsync(SearchMembersModel search = null);
        Task<MemberModel> RegisterAsync(RegisterUserModel model);
        Task<MemberModel> UpdateAsync(MemberModel model);
    }

    public class MemberService : IMemberService
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoIdentityProvider;
        private readonly IRepository<Member> _memberRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<MemberService> _logger;
        private readonly AwsConfig _awsConfig;

        public MemberService(IAmazonCognitoIdentityProvider cognitoIdentityProvider,
            IRepository<Member> memberRepository,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<MemberService> logger,
            IOptions<AwsConfig> awsOptions)
        {
            _cognitoIdentityProvider = cognitoIdentityProvider;
            _memberRepository = memberRepository;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
            _awsConfig = awsOptions.Value;
        }

        public async Task<MemberModel>  FindByIdAsync(int id)
        {
            var member = await _memberRepository.TableNoTracking
                .Include(x => x.StateProvince)
                .Include(x => x.Country)
                .Include(x => x.Division)
                .Include(x => x.Region)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (member == null)
                return null;

            return _mapper.Map<MemberModel>(member);
        }

        public async Task<MemberModel> FindByEmailAsync(string email)
        {
            var member = await _memberRepository.TableNoTracking.FirstOrDefaultAsync(x => x.EmailAddress == email);
            if (member == null)
                return null;

            return _mapper.Map<MemberModel>(member);
        }

        public async Task<MemberModel> FindBySubjectIdAsync(string subjectId)
        {
            var member = await _memberRepository.TableNoTracking.FirstOrDefaultAsync(x => x.SubjectId == subjectId);
            if (member == null)
                return null;

            return _mapper.Map<MemberModel>(member);
        }

        public async Task<IEnumerable<MemberModel>> SearchAsync(SearchMembersModel searchModel = null)
        {
            if (searchModel is null)
            {
                searchModel = new SearchMembersModel();
            }

            var members = await _memberRepository.TableNoTracking
                .Include(x => x.Country)
                .Include(x => x.StateProvince)
                .Include(x => x.Region)
                .Include(x => x.Division)
                .WhereIf(!string.IsNullOrEmpty(searchModel.FirstName), x => x.FirstName.ToLower().Contains(searchModel.FirstName.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(searchModel.LastName), x => x.LastName.ToLower().Contains(searchModel.LastName.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(searchModel.EmailAddress), x => x.EmailAddress.ToLower().Contains(searchModel.EmailAddress.ToLower()))
                .WhereIf(searchModel.CountryId.HasValue, x => x.CountryId == searchModel.CountryId.Value)
                .WhereIf(searchModel.StateProvinceId.HasValue, x => x.StateProvinceId == searchModel.StateProvinceId.Value)
                .WhereIf(searchModel.ShowInDirectory.HasValue, x => x.ShowInDirectory == searchModel.ShowInDirectory.Value)
                .WhereIf(searchModel.RegionId.HasValue, x => x.RegionId == searchModel.RegionId.Value)
                .WhereIf(searchModel.DivisionId.HasValue, x => x.DivisionId == searchModel.DivisionId.Value)
                .WhereIf(searchModel.PlanId.HasValue, x => x.CurrentPlanId == searchModel.PlanId.Value)
                .WhereIf(searchModel.JoinedOnFrom.HasValue, x => x.JoinedOn.Date >= searchModel.JoinedOnFrom.Value.Date)
                .WhereIf(searchModel.JoinedOnTo.HasValue, x => x.JoinedOn.Date <= searchModel.JoinedOnTo.Value.Date)
                .WhereIf(searchModel.DateOfBirthFrom.HasValue, x => x.DateOfBirth.Date >= searchModel.DateOfBirthFrom.Value.Date)
                .WhereIf(searchModel.DateOfBirthTo.HasValue, x => x.DateOfBirth.Date <= searchModel.DateOfBirthTo.Value.Date)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ProjectTo<MemberModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return members;
        }

        public async Task<MemberModel> RegisterAsync(RegisterUserModel model)
        {
            // TODO: Validate and save the member's plan
            // TODO: Workflow (emails, etc)

            try
            {
                string userSubjectId = null;

                try
                {
                    var cognitoSignupRequest = new SignUpRequest
                    {
                        ClientId = _awsConfig.UserPoolClientId,
                        Username = model.EmailAddress,
                        Password = model.Password,
                    };

                    // TODO: If the UserPoolClient has a secret, a secret hash will need to be generated
                    // Couldn't figure out how to get this to work yet though...
                    // cognitoSignupRequest.SecretHash = GenerateCognitoSecretHash(model.EmailAddress);

                    cognitoSignupRequest.UserAttributes = new List<AttributeType>
                    {
                        new AttributeType { Name = "email", Value = model.EmailAddress },
                        new AttributeType { Name = "given_name", Value = model.FirstName },
                        new AttributeType { Name = "family_name", Value = model.LastName },
                    };

                    var cognitoSignupResponse = await _cognitoIdentityProvider.SignUpAsync(cognitoSignupRequest);
                    userSubjectId = cognitoSignupResponse.UserSub;
                }
                catch(Exception ex)
                {
                    // TODO: Be more specific about which exceptions we are catching and return the appropriate response
                    _logger.LogError(ex, "Error creating user in Cognito");
                    throw;
                }

                var member = new Member
                {
                    SubjectId = userSubjectId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Status = MemberStatus.Active,
                    JoinedOn = _dateTimeService.NowUtc, // TODO: Probably shouldn't assume this either
                    EmailAddress = model.EmailAddress,
                    CountryId = model.CountryId,
                    StateProvinceId = model.StateProvinceId,
                    Address = model.Address,
                    Address2 = model.Address2,
                    City = model.City,
                    PostalCode = model.PostalCode,
                    PhoneNumber = model.PhoneNumber,
                };

                await _memberRepository.CreateAsync(member);

                var result = await FindByIdAsync(member.Id);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error registering member");
                throw new ApplicationException("Error registering member", ex);
            }
        }

        public async Task<MemberModel> UpdateAsync(MemberModel model)
        {
            var member = await _memberRepository.GetByIdAsync(model.Id);
            if (member == null)
            {
                throw new ItemNotFoundException($"Member {model.Id} not found.");
            }

            // TODO: Validate country, state, region and division

            try
            {
                member.FirstName = model.FirstName;
                member.LastName = model.LastName;
                member.EmailAddress = model.EmailAddress;
                member.DateOfBirth = model.DateOfBirth;
                member.CountryId = model.CountryId;
                member.StateProvinceId = model.StateProvinceId;
                member.Address = model.Address;
                member.Address2 = model.Address2;
                member.City = model.City;
                member.PostalCode = model.PostalCode;
                member.PhoneNumber = model.PhoneNumber;
                member.RegionId = model.RegionId;
                member.DivisionId = model.DivisionId;
                member.ShowInDirectory = model.ShowInDirectory;
                member.Biography = model.Biography;
                member.Interests = model.Interests;

                await _memberRepository.UpdateAsync(member);

                return await FindByIdAsync(member.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating member");
                throw;
            }
        }

        private string GenerateCognitoSecretHash(string username)
        {
            var key = $"{username}{_awsConfig.UserPoolClientId}";

            using var md5 = MD5.Create();

            var inputBytes = Encoding.ASCII.GetBytes(key);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for(var idx = 0; idx < hashBytes.Length; idx++)
            {
                sb.Append(hashBytes[idx].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
