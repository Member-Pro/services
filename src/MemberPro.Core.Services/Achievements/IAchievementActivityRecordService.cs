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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Achievements
{
    public interface IAchievementActivityService
    {
        Task<AchievementActivityModel> FindByIdAsync(int id);

        Task<IEnumerable<AchievementActivityModel>> GetAllAsync();
        Task<IEnumerable<AchievementActivityModel>> GetByMemberIdAsync(int achievementId,
            int memberId, int? requirementId = null);

        Task<AchievementActivityModel> CreateAsync(CreateAchievementActivityModel model);
        Task UpdateAsync(AchievementActivityModel model);
        Task DeleteAsync(int id);
    }

    public class AchievementActivityService : IAchievementActivityService
    {
        private readonly IRepository<AchievementActivity> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AchievementActivityService> _logger;

        public AchievementActivityService(IRepository<AchievementActivity> repository,
            IMapper mapper,
            ILogger<AchievementActivityService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AchievementActivityModel> FindByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return null;

            return _mapper.Map<AchievementActivityModel>(item);
        }

        public async Task<IEnumerable<AchievementActivityModel>> GetAllAsync()
        {
            var items = await _repository.TableNoTracking
                .ProjectTo<AchievementActivityModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return items;
        }

        public async Task<IEnumerable<AchievementActivityModel>> GetByMemberIdAsync(int achievementId,
            int memberId, int? componentId = null)
        {
            var activities = await _repository.TableNoTracking
                .Include(x => x.Achievement)
                .Include(x => x.Component)
                .Where(x => x.AchievementId == achievementId
                    && x.MemberId == memberId
                    && (!componentId.HasValue || x.ComponentId == componentId.Value))
                .OrderBy(x => x.ActivityDate)
                .ProjectTo<AchievementActivityModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return activities;
        }

        public async Task<AchievementActivityModel> CreateAsync(CreateAchievementActivityModel model)
        {
            try
            {
                var record = new AchievementActivity
                {
                    AchievementId = model.AchievementId,
                    ComponentId = model.ComponentId,
                    MemberId = model.MemberId,
                    ActivityDate = model.ActivityDate,
                    Description = model.Description,
                    QuantityCompleted = model.QuantityCompleted,
                    Comments = model.Comments,
                };

                await _repository.CreateAsync(record);

                var result = await FindByIdAsync(record.Id);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error creating {nameof(AchievementActivity)}");
                throw;
            }
        }

        public async Task UpdateAsync(AchievementActivityModel model)
        {
            var record = await _repository.GetByIdAsync(model.Id);
            if (record == null || record.MemberId != model.MemberId)
            {
                throw new ItemNotFoundException($"{nameof(AchievementActivity)} not found");
            }

            try
            {
                record.ActivityDate = model.ActivityDate;
                record.Description = model.Description;
                record.QuantityCompleted = model.QuantityCompleted;
                record.Comments = model.Comments;

                await _repository.UpdateAsync(record);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating {nameof(AchievementActivity)}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            // TODO: Validate user can delete record

            var record = await _repository.GetByIdAsync(id);
            if (record != null)
            {
                throw new ItemNotFoundException($"{nameof(AchievementActivity)} not found");
            }

            try
            {
                await _repository.DeleteAsync(record);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error deleting {nameof(AchievementActivity)}");
                throw;
            }
        }
    }
}
