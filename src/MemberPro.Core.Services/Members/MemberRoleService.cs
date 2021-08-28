using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Members
{
    public interface IMemberRoleService
    {
        Task<MemberRoleModel> FindByIdAsync(int id);

        Task<IEnumerable<MemberRoleModel>> GetByMemberIdAsync(int memberId);

        Task<bool> IsMemberInRoleAsync(int memberId, int roleId);

        Task<MemberRoleModel> CreateAsync(CreateMemberRoleModel model);
    }

    public class MemberRoleService : IMemberRoleService
    {
        private readonly IRepository<MemberRole> _roleRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<MemberRoleService> _logger;

        public MemberRoleService(IRepository<MemberRole> roleRepository,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<MemberRoleService> logger)
        {
            _roleRepository = roleRepository;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MemberRoleModel> FindByIdAsync(int id)
        {
            var role = await _roleRepository.TableNoTracking
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<MemberRoleModel>(role);
        }

        public async Task<IEnumerable<MemberRoleModel>> GetByMemberIdAsync(int memberId)
        {
            var roles = await _roleRepository.TableNoTracking
                .Include(x => x.Role)
                .Where(x => x.MemberId == memberId)
                .OrderBy(x => x.Role.Name)
                .ProjectTo<MemberRoleModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return roles;
        }

        public async Task<bool> IsMemberInRoleAsync(int memberId, int roleId)
        {
            return await _roleRepository.TableNoTracking
                .AnyAsync(x => x.MemberId == memberId && x.RoleId == roleId);
        }

        public async Task<MemberRoleModel> CreateAsync(CreateMemberRoleModel model)
        {
            // TODO: Validate user is not in role
            // TODO: Validate user and role exist
            try
            {
                var memberRole = new MemberRole
                {
                    MemberId = model.MemberId,
                    RoleId = model.RoleId,
                    AddedOn = _dateTimeService.NowUtc,
                };

                await _roleRepository.CreateAsync(memberRole);

                return await FindByIdAsync(memberRole.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error adding member to role");
                throw;
            }
        }
    }
}
