using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Achievements;
using MemberPro.Core.Models.Achievements;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Achievements
{
    public interface IMemberRequirementService
    {
        Task<IEnumerable<MemberRequirementStateModel>> GetStatesForAchievementIdAsync(int memberId, int achievementId);
        Task<MemberRequirementStateModel> GetStateForRequirementAsync(int memberId, int requirementId);
        Task<MemberRequirementStateModel> UpdateAsync(MemberRequirementStateModel model);
    }

    public class MemberRequirementService : IMemberRequirementService
    {
        private readonly IRepository<MemberRequirementState> _memberRequirementStateRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly ILogger<MemberRequirementService> _logger;

        public MemberRequirementService(IRepository<MemberRequirementState> memberRequirementStateRepository,
            IDateTimeService dateTimeService,
            IMapper mapper,
            ILogger<MemberRequirementService> logger)
        {
            _memberRequirementStateRepository = memberRequirementStateRepository;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MemberRequirementStateModel>> GetStatesForAchievementIdAsync(int memberId, int achievementId)
        {
            var states = await _memberRequirementStateRepository.TableNoTracking
                .Where(x => x.MemberId == memberId && x.Requirement.Component.AchievementId == achievementId)
                .ToListAsync();

            var result = _mapper.Map<List<MemberRequirementStateModel>>(states);
            return result;
        }

        public async Task<MemberRequirementStateModel> GetStateForRequirementAsync(int memberId, int requirementId)
        {
            var state = await GetStateEntityAsync(memberId, requirementId);

            var model = _mapper.Map<MemberRequirementStateModel>(state);
            return model;
        }

        public async Task<MemberRequirementStateModel> UpdateAsync(MemberRequirementStateModel model)
        {
            var stateEntity = await GetStateEntityAsync(model.MemberId, model.RequirementId);
            if (stateEntity == null)
            {
                stateEntity = new MemberRequirementState
                {
                    MemberId = model.MemberId,
                    RequirementId = model.RequirementId
                };

                await _memberRequirementStateRepository.CreateAsync(stateEntity);
            }

            stateEntity.UpdatedOn = _dateTimeService.NowUtc;
            stateEntity.Data = model.Data;

            await _memberRequirementStateRepository.UpdateAsync(stateEntity);

            return _mapper.Map<MemberRequirementStateModel>(stateEntity);
        }

        private async Task<MemberRequirementState> GetStateEntityAsync(int memberId, int requirementId) =>
            await _memberRequirementStateRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.MemberId == memberId
                    && x.RequirementId == requirementId);
    }
}
