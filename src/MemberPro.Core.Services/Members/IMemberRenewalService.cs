using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Members;
using MemberPro.Core.Models.Members;
using MemberPro.Core.Services.Plans;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Members
{
    public interface IMemberRenewalService
    {
        Task<MemberRenewalModel> GetCurrentRenewalForMemberAsync(int memberId, DateTime asOf);

        Task<MemberRenewalModel> GetLatestRenewalForMemberAsync(int memberId);

        Task<IEnumerable<MemberRenewalModel>> GetRenewalsForMemberAsync(int memberId);

        Task<MemberRenewalModel> CreateRenewalAsync(CreateRenewalModel model);
    }

    public class MemberRenewalService : IMemberRenewalService
    {
        private readonly IMemberService _memberService;
        private readonly IMembershipPlanService _planService;
        private readonly IRepository<MemberRenewal> _renewalRepository;
        private readonly ILogger<MemberRenewalService> _logger;
        private readonly IMapper _mapper;

        public MemberRenewalService(IMemberService memberService,
            IMembershipPlanService planService,
            IRepository<MemberRenewal> renewalRepository,
            ILogger<MemberRenewalService> logger,
            IMapper mapper)
        {
            _memberService = memberService;
            _planService = planService;
            _renewalRepository = renewalRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<MemberRenewalModel> GetCurrentRenewalForMemberAsync(int memberId, DateTime asOf)
        {
            var renewal = await _renewalRepository.TableNoTracking
                .Include(x => x.Plan)
                .Where(x => x.MemberId == memberId
                    && x.EndDate >= asOf)
                .OrderByDescending(x => x.StartDate)
                .ProjectTo<MemberRenewalModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return renewal;
        }

        public async Task<MemberRenewalModel> GetLatestRenewalForMemberAsync(int memberId)
        {
            var renewal = await _renewalRepository.TableNoTracking
                .Include(x => x.Plan)
                .Where(x => x.MemberId == memberId)
                .OrderByDescending(x => x.StartDate)
                .ProjectTo<MemberRenewalModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return renewal;
        }

        public async Task<IEnumerable<MemberRenewalModel>> GetRenewalsForMemberAsync(int memberId)
        {
            var renewals = await _renewalRepository.TableNoTracking
                .Include(x => x.Plan)
                .Where(x => x.MemberId == memberId)
                .OrderByDescending(x => x.StartDate)
                .ProjectTo<MemberRenewalModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return renewals;
        }

        public async Task<MemberRenewalModel> CreateRenewalAsync(CreateRenewalModel model)
        {
            var renewPlan = await _planService.FindByIdAsync(model.PlanId);
            if (renewPlan is null)
            {
                throw new ApplicationException($"Plan ID {model.PlanId} not found.");
            }

            var member = _memberService.FindByIdAsync(model.MemberId);
            if (member is null)
            {
                throw new ApplicationException($"Member ID {model.MemberId} not found.");
            }

            var currentRenewal = await GetCurrentRenewalForMemberAsync(model.MemberId, model.EffectiveDate);
            if (currentRenewal is not null)
            {

            }

            throw new NotImplementedException();

        }
    }
}
