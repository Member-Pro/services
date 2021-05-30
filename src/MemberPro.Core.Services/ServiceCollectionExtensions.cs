using MemberPro.Core.Models;
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

            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IStateProvinceService, StateProvinceService>();
            services.AddTransient<IMembershipPlanService, MembershipPlanService>();
            services.AddTransient<IMemberService, MemberService>();
        }
        
    }
}
