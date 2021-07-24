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
    public interface IRequirementService
    {
        Task<RequirementModel> FindByIdAsync(int id);

        Task<IEnumerable<RequirementModel>> GetByAchievementIdAsync(int achievementId, int? memberId = null);
        Task<IEnumerable<RequirementModel>> GetByComponentIdAsync(int componentId);

        Task<RequirementModel> CreateAsync(int componentId, RequirementModel model);
        Task UpdateAsync(RequirementModel model);
    }

    public class RequirementService : IRequirementService
    {
        private readonly IRepository<Requirement> _requirementRepository;
        private readonly IAchievementComponentService _componentService;
        private readonly IMemberRequirementService _memberRequirementService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<RequirementService> _logger;

        public RequirementService(IRepository<Requirement> requirementRepository,
            IAchievementComponentService componentService,
            IMemberRequirementService memberRequirementService,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<RequirementService> logger)
        {
            _requirementRepository = requirementRepository;
            _componentService = componentService;
            _memberRequirementService = memberRequirementService;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RequirementModel> FindByIdAsync(int id)
        {
            var Component = await _requirementRepository.GetByIdAsync(id);
            if (Component == null)
                return null;

            var model = _mapper.Map<RequirementModel>(Component);
            return model;
        }

        public async Task<IEnumerable<RequirementModel>> GetByAchievementIdAsync(int achievementId, int? memberId = null)
        {
            // NOTE: Cannot map to the model as part of the query due to JSON property (Parts)
            var requirements = await _requirementRepository.TableNoTracking
                .Where(x => x.Component.AchievementId == achievementId)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();

            var models = _mapper.Map<List<RequirementModel>>(requirements);

            if (memberId.HasValue)
            {
                var requirementStates = await _memberRequirementService.GetStatesForAchievementIdAsync(memberId.Value, achievementId);
                foreach(var reqModel in models)
                {
                    var paramData = requirementStates.FirstOrDefault(x => x.RequirementId == reqModel.Id);
                    if (paramData != null)
                    {
                        foreach(var param in reqModel.ValidationParameters)
                        {
                            param.Value = paramData.Data[param.Key]?.ToString();
                        }
                    }
                }
            }

            return models;
        }

        private static object GetValue(object source, string paramKey)
        {
            if (source == null) return null;

            return source.GetType()?.GetProperty(paramKey)?.GetValue(source, null);
        }

        public async Task<IEnumerable<RequirementModel>> GetByComponentIdAsync(int componentId)
        {
            // NOTE: Cannot map to the model as part of the query due to JSON property (Parts)
            var requirements = await _requirementRepository.TableNoTracking
                .Where(x => x.ComponentId == componentId)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ToListAsync();

            var models = requirements.Select(x => _mapper.Map<RequirementModel>(x));
            return models;
        }

        public async Task<RequirementModel> CreateAsync(int componentId, RequirementModel model)
        {
            var component = await _componentService.FindByIdAsync(componentId);
            if (component == null)
            {
                throw new ItemNotFoundException($"AchievementComponent ID {componentId} not found");
            }

            try
            {
                var validationParams = _mapper.Map<RequirementValidationParameter[]>(model.ValidationParameters);

                var requirement = new Requirement
                {
                    ComponentId = component.Id,
                    Name = model.Name,
                    Description = model.Description,
                    DisplayOrder = model.DisplayOrder,
                    ValidatorTypeName = model.ValidatorTypeName,
                    ValidationParameters = validationParams,
                    Type = model.Type,
                    MinCount = model.MinCount,
                    MaxCount = model.MaxCount,
                };

                await _requirementRepository.CreateAsync(requirement);

                var newRequirement = await FindByIdAsync(requirement.Id);
                return newRequirement;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating requirement");

                throw new ApplicationException("Error creating requirement", ex);
            }
        }

        public async Task UpdateAsync(RequirementModel model)
        {
            var requirement = await _requirementRepository.GetByIdAsync(model.Id);
            if (requirement == null)
            {
                throw new ItemNotFoundException($"Component ID {model.Id} not found.");
            }

            try
            {
                var validationParams = _mapper.Map<RequirementValidationParameter[]>(model.ValidationParameters);

                requirement.Name = model.Name;
                requirement.Description = model.Description;
                requirement.DisplayOrder = model.DisplayOrder;
                requirement.ValidatorTypeName = model.ValidatorTypeName;
                requirement.ValidationParameters = validationParams;
                requirement.Type = model.Type;
                requirement.MinCount = model.MinCount;
                requirement.MaxCount = model.MaxCount;

                await _requirementRepository.UpdateAsync(requirement);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating requirement");

                throw new ApplicationException("Error updating requirement", ex);
            }
        }
    }
}
