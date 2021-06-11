using System;
using System.Collections.Generic;
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
    public interface ITrackedAchievementService
    {
        Task<TrackedAchievementModel> FindById(int id);
        Task<IEnumerable<TrackedAchievementModel>> GetByMemberId(int memberId);
        Task<TrackedAchievementModel> Create(CreateTrackedAchievementModel model);

        Task Update(int memberId, TrackedAchievementModel model);
        Task Delete(int id);
    }

    public class TrackedAchievementService : ITrackedAchievementService
    {
        private readonly IRepository<TrackedAchievement> _trackedAchievementRepo;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<TrackedAchievementService> _logger;

        public TrackedAchievementService(IRepository<TrackedAchievement> trackedAchievementRepo,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<TrackedAchievementService> logger)
        {
            _trackedAchievementRepo = trackedAchievementRepo;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TrackedAchievementModel> FindById(int id)
        {
            var achievement = await _trackedAchievementRepo.TableNoTracking
                .Include(x => x.Member)
                .Include(x => x.Achievement)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (achievement == null)
                return null;

            var model = _mapper.Map<TrackedAchievementModel>(achievement);
            return model;
        }

        public async Task<IEnumerable<TrackedAchievementModel>> GetByMemberId(int memberId)
        {
            var achievements = await _trackedAchievementRepo.TableNoTracking
                .Include(x => x.Achievement)
                .ProjectTo<TrackedAchievementModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return achievements;
        }

        public async Task<TrackedAchievementModel> Create(CreateTrackedAchievementModel model)
        {
            if (await IsAchievementTracked(model.MemberId, model.AchievementId))
            {
                throw new ApplicationException("User is already tracking achievement");
            }

            try
            {
                var achievement = new TrackedAchievement
                {
                    MemberId = model.MemberId,
                    AchievementId = model.AchievementId,
                    Notes = model.Notes,
                    CreatedOn = _dateTimeService.NowUtc,
                };

                await _trackedAchievementRepo.CreateAsync(achievement);

                var result = await FindById(achievement.Id);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error creating {nameof(TrackedAchievement)}");
                throw;
            }
        }

        public async Task Update(int memberId, TrackedAchievementModel model)
        {
            // TODO: WOuld be nice to not pass in the memberId to verify  owner
            var achievement = await _trackedAchievementRepo.GetByIdAsync(model.Id);
            if (achievement == null || achievement.MemberId != memberId)
            {
                throw new ItemNotFoundException($"Tracked achievement {model.Id} not found");
            }

            try
            {
                // Notes are the only thing you can update
                achievement.Notes = model.Notes;

                await _trackedAchievementRepo.UpdateAsync(achievement);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating {nameof(TrackedAchievement)}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            var achievement = await _trackedAchievementRepo.GetByIdAsync(id);
            if (achievement == null)
            {
                throw new ItemNotFoundException($"Tracked achievement {id} not found");
            }

            try
            {
                await _trackedAchievementRepo.DeleteAsync(achievement);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error deleting {nameof(TrackedAchievement)}");
                throw;
            }
        }

        public async Task<bool> IsAchievementTracked(int memberId, int achievementId)
        {
            var tracked = await _trackedAchievementRepo.TableNoTracking
                .AnyAsync(x => x.MemberId == memberId && x.AchievementId == achievementId);
            return tracked;
        }
    }
}
