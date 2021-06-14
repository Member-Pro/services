using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Members
{
    public interface IMemberService
    {
        Task<MemberModel> FindByIdAsync(int id);
        Task<MemberModel> FindByEmailAsync(string email);
        Task<MemberModel> FindBySubjectIdAsync(string subjectId);

        Task<IEnumerable<MemberModel>> SearchAsync();
        Task<MemberModel> RegisterAsync(RegisterUserModel model);
        Task<MemberModel> UpdateAsync(MemberModel model);
    }

    public class MemberService : IMemberService
    {
        private readonly IRepository<Member> _memberRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<MemberService> _logger;

        public MemberService(IRepository<Member> memberRepository,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<MemberService> logger)
        {
            _memberRepository = memberRepository;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
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

        public async Task<IEnumerable<MemberModel>> SearchAsync()
        {
            var members = await _memberRepository.TableNoTracking
                .Include(x => x.Country)
                .Include(x => x.StateProvince)
                .Include(x => x.Region)
                .Include(x => x.Division)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ProjectTo<MemberModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return members;
        }

        public async Task<MemberModel> RegisterAsync(RegisterUserModel model)
        {
            try
            {
                var member = new Member
                {
                    SubjectId = model.SubjectId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Status = MemberStatus.Active, // TODO: do we want to assume active?
                    JoinedOn = _dateTimeService.NowUtc, // TODO: Probably shouldn't assume this either
                    EmailAddress = model.EmailAddress,
                    DateOfBirth = model.DateOfBirth,
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
    }
}
