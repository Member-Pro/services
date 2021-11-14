using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Organizations;
using MemberPro.Core.Models.Organizations;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Organizations
{
    public interface IOrganizationService
    {
        Task<OrganizationModel> FindById(int id);

        Task<IEnumerable<OrganizationModel>> GetAll();
        Task<IEnumerable<OrganizationModel>> GetByParentId(int parentId);

        Task<OrganizationModel> Create(CreateOrganizationModel model);
    }

    public class OrganizationService : IOrganizationService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IRepository<Organization> _organizationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationService> _logger;

        public OrganizationService(IDateTimeService dateTimeService,
            IRepository<Organization> organizationRepository,
            IMapper mapper,
            ILogger<OrganizationService> logger)
        {
            _dateTimeService = dateTimeService;
            _organizationRepository = organizationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrganizationModel> FindById(int id)
        {
            var organization = await _organizationRepository.TableNoTracking
                .Include(x => x.Parent)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (organization == null)
                return null;

            return _mapper.Map<OrganizationModel>(organization);
        }

        public async Task<IEnumerable<OrganizationModel>> GetAll()
        {
            var organizations = await _organizationRepository.TableNoTracking
                .Include(x => x.Parent)
                .OrderBy(x => x.ParentId != null)
                .ThenBy(x => x.ParentId)
                .ThenBy(x => x.Name)
                .ProjectTo<OrganizationModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return organizations;
        }

        public async Task<IEnumerable<OrganizationModel>> GetByParentId(int parentId)
        {
            var organizations = await _organizationRepository.TableNoTracking
                .Include(x => x.Parent)
                .Where(x => x.ParentId == parentId)
                .OrderBy(x => x.Name)
                .ProjectTo<OrganizationModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return organizations;
        }

        public async Task<OrganizationModel> Create(CreateOrganizationModel model)
        {
            try
            {
                var Organization = new Organization
                {
                    ParentId = model.ParentId, // TODO: Validate me
                    Name = model.Name,
                    Abbreviation = model.Abbreviation,
                    Description = model.Description,
                    CreatedOn = _dateTimeService.NowUtc,
                    UpdatedOn = _dateTimeService.NowUtc,
                };

                await _organizationRepository.CreateAsync(Organization);

                return await FindById(Organization.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating organization.");
                throw;
            }
        }
    }
}
