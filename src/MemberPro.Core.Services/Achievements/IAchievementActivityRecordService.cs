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
    public interface IAchievementActivityRecordService
    {
        Task<AchievementActivityRecordModel> FindByIdAsync(int id);

        Task<IEnumerable<AchievementActivityRecordModel>> GetAllAsync();
        Task<IEnumerable<AchievementActivityRecordModel>> GetByMemberIdAsync(int achievementId,
            int memberId, int? requirementId = null);

        Task<AchievementActivityRecordModel> CreateAsync(CreateAchievementActivityRecordModel model);
        Task UpdateAsync(AchievementActivityRecordModel model);
        Task DeleteAsync(int id);
    }

    public class AchievementActivityRecordService : IAchievementActivityRecordService
    {
        private readonly IRepository<AchievementActivityRecord> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AchievementActivityRecordService> _logger;

        public AchievementActivityRecordService(IRepository<AchievementActivityRecord> repository,
            IMapper mapper,
            ILogger<AchievementActivityRecordService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AchievementActivityRecordModel> FindByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return null;

            return _mapper.Map<AchievementActivityRecordModel>(item);
        }

        public async Task<IEnumerable<AchievementActivityRecordModel>> GetAllAsync()
        {
            var items = await _repository.TableNoTracking
                .ProjectTo<AchievementActivityRecordModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return items;
        }

        public async Task<IEnumerable<AchievementActivityRecordModel>> GetByMemberIdAsync(int achievementId,
            int memberId, int? requirementId = null)
        {
            var activities = await _repository.TableNoTracking
                .Include(x => x.Achievement)
                .Include(x => x.Requirement)
                .Where(x => x.AchievementId == achievementId
                    && x.MemberId == memberId
                    && (!requirementId.HasValue || x.RequirementId == requirementId.Value))
                .OrderBy(x => x.ActivityDate)
                .ProjectTo<AchievementActivityRecordModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return activities;
        }

        public async Task<AchievementActivityRecordModel> CreateAsync(CreateAchievementActivityRecordModel model)
        {
            try
            {
                var record = new AchievementActivityRecord
                {
                    AchievementId = model.AchievementId,
                    RequirementId = model.RequirementId,
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
                _logger.LogError(ex, $"Error creating {nameof(AchievementActivityRecord)}");
                throw;
            }
        }

        public async Task UpdateAsync(AchievementActivityRecordModel model)
        {
            var record = await _repository.GetByIdAsync(model.Id);
            if (record == null || record.MemberId != model.MemberId)
            {
                throw new ItemNotFoundException($"{nameof(AchievementActivityRecord)} not found");
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
                _logger.LogError(ex, $"Error updating {nameof(AchievementActivityRecord)}");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            // TODO: Validate user can delete record

            var record = await _repository.GetByIdAsync(id);
            if (record != null)
            {
                throw new ItemNotFoundException($"{nameof(AchievementActivityRecord)} not found");
            }

            try
            {
                await _repository.DeleteAsync(record);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error deleting {nameof(AchievementActivityRecord)}");
                throw;
            }
        }
    }
}
