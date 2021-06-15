using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Achievements;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Achievements
{
    public interface IAchievementRequirementService
    {
        Task<AchievementRequirementModel> FindByIdAsync(int id);
        Task<IEnumerable<AchievementRequirementModel>> GetByAchievementIdAsync(int achievementId);

        Task<AchievementRequirementModel> CreateAsync(CreateAchievementRequirementModel model);
    }

    public class AchievementRequirementService : IAchievementRequirementService
    {
        private readonly IRepository<AchievementRequirement> _requirementRepository;
        private readonly IAchievementService _achievementService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<AchievementRequirementService> _logger;

        public AchievementRequirementService(IRepository<AchievementRequirement> requirementRepository,
            IAchievementService achievementService,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<AchievementRequirementService> logger)
        {
            _requirementRepository = requirementRepository;
            _achievementService = achievementService;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AchievementRequirementModel> FindByIdAsync(int id)
        {
            var requirement = await _requirementRepository.GetByIdAsync(id);
            if (requirement == null)
                return null;

            var model = _mapper.Map<AchievementRequirementModel>(requirement);
            return model;
        }

        public async Task<IEnumerable<AchievementRequirementModel>> GetByAchievementIdAsync(int achievementId)
        {
            var requirements = await _requirementRepository.TableNoTracking
                .Where(x => x.AchievementId == achievementId)
                .OrderBy(x => x.Name)
                .ProjectTo<AchievementRequirementModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return requirements;
        }

        public async Task<AchievementRequirementModel> CreateAsync(CreateAchievementRequirementModel model)
        {
            var achievement = await _achievementService.FindByIdAsync(model.AchievementId);
            if (achievement == null)
            {
                throw new ItemNotFoundException($"Achievement ID {model.AchievementId} not found");
            }

            try
            {
                var requirement = new AchievementRequirement
                {
                    AchievementId = achievement.Id,
                    Name = model.Name,
                    Description = model.Description,
                    IsRequired = model.IsRequired,
                    MinimumCount = model.MinimumCount,
                    IsDisabled = model.IsDisabled,
                    CreatedOn = _dateTimeService.NowUtc,
                    UpdatedOn = _dateTimeService.NowUtc,
                };

                await _requirementRepository.CreateAsync(requirement);

                var newRequirement = await FindByIdAsync(requirement.Id);
                return newRequirement;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating achievement requirement");

                throw new ApplicationException("Error creating achievement requirement", ex);
            }
        }
    }
}
