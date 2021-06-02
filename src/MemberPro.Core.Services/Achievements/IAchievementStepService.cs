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
    public interface IAchievementStepService
    {
        Task<AchievementStepModel> FindByIdAsync(int id);
        Task<IEnumerable<AchievementStepModel>> GetByAchievementIdAsync(int achievementId);

        Task<AchievementStepModel> CreateAsync(CreateAchievementStepModel model);
    }

    public class AchievementStepService : IAchievementStepService
    {
        private readonly IRepository<AchievementStep> _stepRepository;
        private readonly IAchievementService _achievementService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<AchievementStepService> _logger;

        public AchievementStepService(IRepository<AchievementStep> stepRepository,
            IAchievementService achievementService,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<AchievementStepService> logger)
        {
            _stepRepository = stepRepository;
            _achievementService = achievementService;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AchievementStepModel> FindByIdAsync(int id)
        {
            var step = await _stepRepository.GetByIdAsync(id);
            if (step == null)
                return null;

            var model = _mapper.Map<AchievementStepModel>(step);
            return model;
        }

        public async Task<IEnumerable<AchievementStepModel>> GetByAchievementIdAsync(int achievementId)
        {
            var steps = await _stepRepository.TableNoTracking
                .Where(x => x.AchievementId == achievementId)
                .OrderBy(x => x.Name)
                .ProjectTo<AchievementStepModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return steps;
        }

        public async Task<AchievementStepModel> CreateAsync(CreateAchievementStepModel model)
        {
            var achievement = await _achievementService.FindByIdAsync(model.AchievementId);
            if (achievement == null)
            {
                throw new ItemNotFoundException($"Achievement ID {model.AchievementId} not found");
            }

            try
            {
                var step = new AchievementStep
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

                await _stepRepository.CreateAsync(step);

                var newStep = await FindByIdAsync(step.Id);
                return newStep;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating achievement step");

                throw new ApplicationException("Error creating achievement step", ex);
            }
        }
    }
}
