using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Plans;
using MemberPro.Core.Models.Plans;
using Microsoft.EntityFrameworkCore;

namespace MemberPro.Core.Services.Plans
{
    public interface IMembershipPlanService
    {
        Task<MembershipPlanModel> FindByIdAsync(int id);

        Task<IEnumerable<MembershipPlanModel>> GetAllAsync();

        Task<MembershipPlanModel> CreateAsync(MembershipPlan plan);
        Task<MembershipPlanModel> UpdateAsync(MembershipPlan plan);        
    }

    public class MembershipPlanService : IMembershipPlanService
    {
        private readonly IRepository<MembershipPlan> _planRepository;
        private readonly IMapper _mapper;

        public MembershipPlanService(IRepository<MembershipPlan> planRepository,
            IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<MembershipPlanModel> FindByIdAsync(int id)
        {
            var plan = await _planRepository.GetByIdAsync(id);
            if (plan == null)
                return null;

            return _mapper.Map<MembershipPlanModel>(plan);
        }

        public async Task<IEnumerable<MembershipPlanModel>> GetAllAsync()
        {
            var plans = await _planRepository.TableNoTracking
                   .OrderBy(x => x.Name)
                   .ProjectTo<MembershipPlanModel>(_mapper.ConfigurationProvider)
                   .ToListAsync();

            return plans;
        }

        public async Task<MembershipPlanModel> CreateAsync(MembershipPlan plan)
        {
            throw new NotImplementedException();
        }

        public async Task<MembershipPlanModel> UpdateAsync(MembershipPlan plan)
        {
            throw new NotImplementedException();
        }
    }
}
