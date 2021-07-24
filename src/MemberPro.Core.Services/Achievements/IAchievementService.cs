using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Achievements;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Achievements
{
    public interface IAchievementService
    {
        Task<AchievementModel> FindByIdAsync(int id);

        Task<IEnumerable<AchievementModel>> GetAllAsync();

        Task<AchievementModel> CreateAsync(CreateAchievementModel model);
    }

    public class AchievementService : IAchievementService
    {
        private readonly IRepository<Achievement> _achievementRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<AchievementService> _logger;

        public AchievementService(IRepository<Achievement> achievementRepository,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<AchievementService> logger)
        {
            _achievementRepository = achievementRepository;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AchievementModel> FindByIdAsync(int id)
        {
            var achievement = await _achievementRepository.GetByIdAsync(id);
            if (achievement == null)
                return null;

            var model = _mapper.Map<AchievementModel>(achievement);
            return model;
        }

        public async Task<IEnumerable<AchievementModel>> GetAllAsync()
        {
            var achievements = await _achievementRepository.TableNoTracking
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Name)
                .ProjectTo<AchievementModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return achievements;
        }

        public async Task<AchievementModel> CreateAsync(CreateAchievementModel model)
        {
            try
            {
                var achievement = new Achievement
                {
                    Name = model.Name,
                    Description = model.Description,
                    InfoUrl = model.InfoUrl,
                    ImageFilename = model.ImageFilename,
                    DisplayOrder = model.DisplayOrder,
                    IsDisabled = model.IsDisabled,
                    CreatedOn = _dateTimeService.NowUtc,
                    UpdatedOn = _dateTimeService.NowUtc,
                };

                await _achievementRepository.CreateAsync(achievement);

                var result = await FindByIdAsync(achievement.Id);

                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating achievement");

                throw new ApplicationException("Error creating achievement", ex);
            }
        }
    }
}
