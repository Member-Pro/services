using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Organizations;
using MemberPro.Core.Enums;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Organizations;
using MemberPro.Core.Services.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Organizations
{
    public interface IOfficerService
    {
        Task<OfficerPositionModel> FindPositionByIdAsync(int id);

        Task<IEnumerable<OfficerPositionModel>> GetPositionsForOrganizationAsync(int orgId);

        Task<OfficerPositionModel> CreatePositionAsync(CreateOfficerPositionModel model);
        Task UpdatePositionAsync(OfficerPositionModel model);

        Task<OfficerModel> FindOfficerByIdAsync(int id);

        Task<IEnumerable<OfficerModel>> GetCurrentOfficersForOrganizationAsync(int orgId, DateOnly asOf,
            OfficerPositionType? positionType = null, bool includeParentOrgs = false);

        Task<OfficerModel> CreateOfficerAsync(CreateOfficerModel model);
        Task UpdateOfficerAsync(UpdateOfficerModel model);
    }

    public class OfficerService : IOfficerService
    {
        private readonly IRepository<Officer> _officerRepository;
        private readonly IRepository<OfficerPosition> _positionRepository;
        private readonly IOrganizationService _organizationService;
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;
        private readonly ILogger<OfficerService> _logger;

        public OfficerService(IRepository<Officer> officerRepository,
            IRepository<OfficerPosition> positionRepository,
            IOrganizationService organizationService,
            IMemberService memberService ,
            IMapper mapper,
            ILogger<OfficerService> logger)
        {
            _officerRepository = officerRepository;
            _positionRepository = positionRepository;
            _organizationService = organizationService;
            _memberService = memberService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OfficerPositionModel> FindPositionByIdAsync(int id)
        {
            var position = await _positionRepository.TableNoTracking
                .Include(x => x.Organization)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (position is null)
                return null;

            return _mapper.Map<OfficerPosition, OfficerPositionModel>(position);
        }

        public async Task<IEnumerable<OfficerPositionModel>> GetPositionsForOrganizationAsync(int orgId)
        {
            var positions = await _positionRepository.TableNoTracking
                .Include(x => x.Organization)
                .Where(x => x.OrganizationId == orgId)
                .OrderBy(x => x.PositionType)
                .ThenBy(x => x.Title)
                .ProjectTo<OfficerPositionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return positions;
        }

        public async Task<OfficerPositionModel> CreatePositionAsync(CreateOfficerPositionModel model)
        {
            // TODO: Verify user can create this resource

            var organization = await _organizationService.FindById(model.OrganizationId);
            if (organization is null)
            {
                throw new ItemNotFoundException($"Organization ID {model.OrganizationId} not found.");
            }

            try
            {
                var position = new OfficerPosition
                {
                    OrganizationId = organization.Id,
                    PositionType = model.PositionType,
                    Title = model.Title,
                };

                await _positionRepository.CreateAsync(position);

                return await FindPositionByIdAsync(position.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating officer position.");
                throw;
            }
        }

        public async Task UpdatePositionAsync(OfficerPositionModel model)
        {
            // TODO: Verify user can update this resource

            var position = await _positionRepository.GetByIdAsync(model.Id);
            if (position is null)
            {
                throw new ItemNotFoundException($"Position ID {model.Id} not found.");
            }

            try
            {
                position.PositionType = model.PositionType;
                position.Title = model.Title;

                await _positionRepository.UpdateAsync(position);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating position {model.Id}");
                throw;
            }
        }

        public async Task<OfficerModel> FindOfficerByIdAsync(int id)
        {
            var officer = await _officerRepository.TableNoTracking
                .Include(x => x.Position)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (officer is null)
                return null;

            return _mapper.Map<Officer, OfficerModel>(officer);
        }

        public async Task<IEnumerable<OfficerModel>> GetCurrentOfficersForOrganizationAsync(int orgId, DateOnly asOf,
            OfficerPositionType? positionType = null, bool includeParentOrgs = false)
        {
            List<int> orgIds = new();
            if (includeParentOrgs)
            {
                var organizations = await _organizationService.GetOrganizationWithParents(orgId);
                orgIds = organizations.Select(x => x.Id).ToList();
            }

            var officers = await _officerRepository.TableNoTracking
                .Include(x => x.Position)
                .Include(x => x.Member)
                .Where(x => x.TermStart >= asOf && x.TermEnd <= asOf)
                .WhereIf(positionType.HasValue, x => x.Position.PositionType == positionType.Value)
                .WhereIf(includeParentOrgs && orgIds.Any(), x => orgIds.Contains(x.Position.OrganizationId))
                .WhereIf(!includeParentOrgs, x => x.Position.OrganizationId == orgId)
                .OrderBy(x => x.Position.PositionType)
                .ThenBy(x => x.Member.LastName)
                .ThenBy(x => x.Member.FirstName)
                .ProjectTo<OfficerModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return officers;
        }

        public async Task<OfficerModel> CreateOfficerAsync(CreateOfficerModel model)
        {
            // TODO: Verify user can create this resource

            var position = await FindPositionByIdAsync(model.PositionId);
            if (position is null)
            {
                throw new ItemNotFoundException($"Position ID {model.PositionId} not found.");
            }

            // TODO: Ensure member is in organization that this position is a part of
            var member = await _memberService.FindByIdAsync(model.MemberId);
            if (member is null)
            {
                throw new ItemNotFoundException($"Member ID {model.MemberId} not found.");
            }

            try
            {
                var officer = new Officer
                {
                    PositionId = position.Id,
                    MemberId = member.Id,
                    TermStart = model.TermStart,
                    TermEnd = model.TermEnd,
                };

                await _officerRepository.CreateAsync(officer);

                return await FindOfficerByIdAsync(officer.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating officer.");
                throw;
            }
        }

        public async Task UpdateOfficerAsync(UpdateOfficerModel model)
        {
            // TODO: Verify user can update this resource

            var officer = await _officerRepository.GetByIdAsync(model.Id);
            if (officer is null)
            {
                throw new ItemNotFoundException($"Officer ID {model.Id} not found.");
            }

            if (model.PositionId != officer.PositionId)
            {
                // Position has changed, validate it
                var position = await FindPositionByIdAsync(model.PositionId);
                if (position is null)
                {
                    throw new ItemNotFoundException($"Position ID {model.PositionId} not found.");
                }
            }

            try
            {
                // Can't update the member
                officer.PositionId = model.PositionId;
                officer.TermStart = model.TermStart;
                officer.TermEnd = model.TermEnd;

                await _officerRepository.UpdateAsync(officer);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating officer {model.Id}");
                throw;
            }

        }
    }
}
