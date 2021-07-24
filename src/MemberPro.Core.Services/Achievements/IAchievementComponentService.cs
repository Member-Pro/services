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
    public interface IAchievementComponentService
    {
        Task<AchievementComponentModel> FindByIdAsync(int id);
        Task<IEnumerable<AchievementComponentModel>> GetByAchievementIdAsync(int achievementId);

        Task<AchievementComponentModel> CreateAsync(int achievementId, CreateAchievementComponentModel model);
        Task UpdateAsync(AchievementComponentModel model);
    }

    public class AchievementComponentService : IAchievementComponentService
    {
        private readonly IRepository<AchievementComponent> _componentRepository;
        private readonly IAchievementService _achievementService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<AchievementComponentService> _logger;

        public AchievementComponentService(IRepository<AchievementComponent> ComponentRepository,
            IAchievementService achievementService,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<AchievementComponentService> logger)
        {
            _componentRepository = ComponentRepository;
            _achievementService = achievementService;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AchievementComponentModel> FindByIdAsync(int id)
        {
            var Component = await _componentRepository.GetByIdAsync(id);
            if (Component == null)
                return null;

            var model = _mapper.Map<AchievementComponentModel>(Component);
            return model;
        }

        public async Task<IEnumerable<AchievementComponentModel>> GetByAchievementIdAsync(int achievementId)
        {
            // NOTE: Cannot map to the model as part of the query due to JSON property (Parts)
            var Components = await _componentRepository.TableNoTracking
                .Where(x => x.AchievementId == achievementId)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();

            var models = Components.Select(x => _mapper.Map<AchievementComponentModel>(x));
            return models;
        }

        public async Task<AchievementComponentModel> CreateAsync(int achievementId, CreateAchievementComponentModel model)
        {
            var achievement = await _achievementService.FindByIdAsync(achievementId);
            if (achievement == null)
            {
                throw new ItemNotFoundException($"Achievement ID {achievementId} not found");
            }

            try
            {
                var Component = new AchievementComponent
                {
                    AchievementId = achievement.Id,
                    Name = model.Name,
                    Description = model.Description,
                    DisplayOrder = model.DisplayOrder,
                    IsDisabled = model.IsDisabled,
                    CreatedOn = _dateTimeService.NowUtc,
                    UpdatedOn = _dateTimeService.NowUtc,
                };

                await _componentRepository.CreateAsync(Component);

                var newComponent = await FindByIdAsync(Component.Id);
                return newComponent;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating achievement Component");

                throw new ApplicationException("Error creating achievement Component", ex);
            }
        }

        public async Task UpdateAsync(AchievementComponentModel model)
        {
            var Component = await _componentRepository.GetByIdAsync(model.Id);
            if (Component == null)
            {
                throw new ItemNotFoundException($"Component ID {model.Id} not found.");
            }

            try
            {
                Component.Name = model.Name;
                Component.Description = model.Description;
                Component.DisplayOrder = model.DisplayOrder;
                Component.IsDisabled = model.IsDisabled;
                Component.UpdatedOn = _dateTimeService.NowUtc;

                await _componentRepository.UpdateAsync(Component);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating achievement Component");

                throw new ApplicationException("Error updating achievement Component", ex);
            }
        }
    }
}
