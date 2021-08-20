using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Models.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Members
{
    public interface IRoleService
    {
        Task<RoleModel> FindByIdAsync(int id);

        Task<IEnumerable<RoleModel>> GetAllAsync();

        Task<RoleModel> CreateAsync(RoleModel model);
    }

    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IRepository<Role> roleRepository,
            IMapper mapper,
            ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RoleModel> FindByIdAsync(int id)
        {
            var role = await _roleRepository.TableNoTracking.SingleOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<RoleModel>(role);
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            var roles = await _roleRepository.TableNoTracking
                .OrderBy(x => x.Name)
                .ProjectTo<RoleModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return roles;
        }

        public async Task<RoleModel> CreateAsync(RoleModel model)
        {
            try
            {
                var role = new Role
                {
                    Name = model.Name,
                };

                await _roleRepository.CreateAsync(role);

                return await FindByIdAsync(role.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating role");
                throw;
            }
        }
    }
}
