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
    public interface IFavoriteAchievementService
    {
        Task<FavoriteAchievementModel> FindById(int id);
        Task<IEnumerable<FavoriteAchievementModel>> GetByMemberId(int memberId);
        Task<FavoriteAchievementModel> Create(CreateFavoriteAchievementModel model);

        Task Update(int memberId, FavoriteAchievementModel model);
        Task Delete(int id);
    }

    public class FavoriteAchievementService : IFavoriteAchievementService
    {
        private readonly IRepository<FavoriteAchievement> _favoriteAchievementRepo;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<FavoriteAchievementService> _logger;

        public FavoriteAchievementService(IRepository<FavoriteAchievement> favoriteAchievementRepo,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<FavoriteAchievementService> logger)
        {
            _favoriteAchievementRepo = favoriteAchievementRepo;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<FavoriteAchievementModel> FindById(int id)
        {
            var achievement = await _favoriteAchievementRepo.TableNoTracking
                .Include(x => x.Member)
                .Include(x => x.Achievement)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (achievement == null)
                return null;

            var model = _mapper.Map<FavoriteAchievementModel>(achievement);
            return model;
        }

        public async Task<IEnumerable<FavoriteAchievementModel>> GetByMemberId(int memberId)
        {
            var achievements = await _favoriteAchievementRepo.TableNoTracking
                .Include(x => x.Achievement)
                .ProjectTo<FavoriteAchievementModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return achievements;
        }

        public async Task<FavoriteAchievementModel> Create(CreateFavoriteAchievementModel model)
        {
            if (await IsAchievementTracked(model.MemberId, model.AchievementId))
            {
                throw new ApplicationException("User is already tracking achievement");
            }

            try
            {
                var achievement = new FavoriteAchievement
                {
                    MemberId = model.MemberId,
                    AchievementId = model.AchievementId,
                    Notes = model.Notes,
                    CreatedOn = _dateTimeService.NowUtc,
                };

                await _favoriteAchievementRepo.CreateAsync(achievement);

                var result = await FindById(achievement.Id);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error creating {nameof(FavoriteAchievement)}");
                throw;
            }
        }

        public async Task Update(int memberId, FavoriteAchievementModel model)
        {
            // TODO: WOuld be nice to not pass in the memberId to verify  owner
            var achievement = await _favoriteAchievementRepo.GetByIdAsync(model.Id);
            if (achievement == null || achievement.MemberId != memberId)
            {
                throw new ItemNotFoundException($"Tracked achievement {model.Id} not found");
            }

            try
            {
                // Notes are the only thing you can update
                achievement.Notes = model.Notes;

                await _favoriteAchievementRepo.UpdateAsync(achievement);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating {nameof(FavoriteAchievement)}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
            var achievement = await _favoriteAchievementRepo.GetByIdAsync(id);
            if (achievement == null)
            {
                throw new ItemNotFoundException($"Tracked achievement {id} not found");
            }

            try
            {
                await _favoriteAchievementRepo.DeleteAsync(achievement);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error deleting {nameof(FavoriteAchievement)}");
                throw;
            }
        }

        public async Task<bool> IsAchievementTracked(int memberId, int achievementId)
        {
            var tracked = await _favoriteAchievementRepo.TableNoTracking
                .AnyAsync(x => x.MemberId == memberId && x.AchievementId == achievementId);
            return tracked;
        }
    }
}
