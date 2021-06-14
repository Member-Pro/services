using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Members
{
    public interface IMemberAchievementService
    {
        Task<MemberAchievementModel> FindById(int id);

        Task<IEnumerable<MemberAchievementModel>> GetByMemberId(int memberId);

        Task<MemberAchievementModel> Create(CreateMemberAchievementModel model);
    }

    public class MemberAchievementService : IMemberAchievementService
    {
        private readonly IRepository<MemberAchievement> _memberAchievementRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<MemberAchievementService> _logger;

        public MemberAchievementService(IRepository<MemberAchievement> memberAchievementRepository,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<MemberAchievementService> logger)
        {
            _memberAchievementRepository = memberAchievementRepository;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MemberAchievementModel> FindById(int id)
        {
            var achievement = await _memberAchievementRepository.TableNoTracking
                .Include(x => x.Member)
                .Include(x => x.Achievement)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (achievement == null)

            return null;

            var model = _mapper.Map<MemberAchievementModel>(achievement);
            return model;
        }

        public async Task<IEnumerable<MemberAchievementModel>> GetByMemberId(int memberId)
        {
            var achievements = await _memberAchievementRepository.TableNoTracking
                .Include(x => x.Achievement)
                .Where(x => x.MemberId == memberId)
                .OrderByDescending(x => x.EarnedOn)
                .ProjectTo<MemberAchievementModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return achievements;
        }

        public async Task<MemberAchievementModel> Create(CreateMemberAchievementModel model)
        {
            try
            {
                var achievement = new MemberAchievement
                {
                    MemberId = model.MemberId,
                    AchievementId = model.AchievementId,
                    EarnedOn = model.EarnedOn,
                    DisplayPublicly = model.DisplayPublicly,
                    CreatedOn = _dateTimeService.NowUtc,
                    CreatedByMemberId = model.CreatedByMemberId,


                };

                await _memberAchievementRepository.CreateAsync(achievement);

                var result = await FindById(achievement.Id);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error creating {nameof(MemberAchievement)}");
                throw;
            }
        }
    }
}
