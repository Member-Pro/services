using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MemberPro.Core.Data;
using MemberPro.Core.Entities.Plans;
using MemberPro.Core.Exceptions;
using MemberPro.Core.Models.Plans;
using MemberPro.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberPro.Core.Services.Plans
{
    public interface IMembershipPlanService
    {
        Task<MembershipPlanModel> FindByIdAsync(int id);

        Task<IEnumerable<MembershipPlanModel>> GetAllAsync();

        Task<MembershipPlanModel> CreateAsync(MembershipPlanModel model);
        Task<MembershipPlanModel> UpdateAsync(MembershipPlanModel model);
    }

    public class MembershipPlanService : IMembershipPlanService
    {
        private readonly IRepository<MembershipPlan> _planRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<MembershipPlanService> _logger;
        private readonly IMapper _mapper;

        public MembershipPlanService(IRepository<MembershipPlan> planRepository,
            IDateTimeService dateTimeService,
            ILogger<MembershipPlanService> logger,
            IMapper mapper)
        {
            _planRepository = planRepository;
            _dateTimeService = dateTimeService;
            _logger = logger;
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

        public async Task<MembershipPlanModel> CreateAsync(MembershipPlanModel model)
        {
            try
            {
                var plan = new MembershipPlan
                {
                    Name = model.Name,
                    SKU = model.SKU,
                    Description = model.Description,
                    AvailableStartDate = model.AvailableStartDate,
                    AvailableEndDate = model.AvailableEndDate,
                    Price = model.Price,
                    DurationInMonths = model.DurationInMonths,
                    CreatedOn = _dateTimeService.NowUtc,
                    UpdatedOn = _dateTimeService.NowUtc,
                };

                await _planRepository.CreateAsync(plan);

                return await FindByIdAsync(plan.Id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating plan");
                throw;
            }
        }

        public async Task<MembershipPlanModel> UpdateAsync(MembershipPlanModel model)
        {
            var plan = await _planRepository.GetByIdAsync(model.Id);
            if (plan == null)
            {
                throw new ItemNotFoundException($"Plan ID {model.Id} not found.");
            }

            try
            {
                plan.Name = model.Name;
                plan.SKU = model.SKU;
                plan.Description = model.Description;
                plan.AvailableStartDate = model.AvailableStartDate;
                plan.AvailableEndDate = model.AvailableEndDate;
                plan.Price = model.Price;
                plan.DurationInMonths = model.DurationInMonths;
                plan.UpdatedOn = _dateTimeService.NowUtc;

                await _planRepository.UpdateAsync(plan);

                return _mapper.Map<MembershipPlanModel>(plan);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error updating plan ID {model.Id}");
                throw ex;
            }
        }
    }
}
