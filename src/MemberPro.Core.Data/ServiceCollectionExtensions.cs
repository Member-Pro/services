using MemberPro.Core.Data.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MemberPro.Core.Data
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbContext, MemberProDbContext>();

            services.AddDbContext<MemberProDbContext>(options =>
            {
                //options.UseSqlServer(configuration.GetConnectionString());
                options.UseNpgsql(configuration.GetConnectionString())
                    .UseSnakeCaseNamingConvention();
            });

            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
        }

        private static string GetConnectionString(this IConfiguration configuration) =>
            configuration.GetConnectionString("MemberProDb");

    }
}
