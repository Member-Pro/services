using MemberPro.Core.Models;
using MemberPro.Core.Services.Achievements;
using MemberPro.Core.Services.Common;
using MemberPro.Core.Services.Geography;
using MemberPro.Core.Services.Members;
using MemberPro.Core.Services.Plans;
using Microsoft.Extensions.DependencyInjection;

namespace MemberPro.Core.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ModelMapper));

            services.AddTransient<IAchievementService, AchievementService>();
            services.AddTransient<IAchievementStepService, AchievementStepService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IMembershipPlanService, MembershipPlanService>();
            services.AddTransient<IStateProvinceService, StateProvinceService>();
        }
    }
}
