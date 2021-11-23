using MemberPro.Core.Data;
using MemberPro.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MemberPro.Tools.DataGenerator
{
    public partial class Program
    {
        public static IConfigurationRoot? Configuration;

        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddEnvironmentVariables();

                    if (args is not null)
                    {
                        config.AddCommandLine(args);
                    }

                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

                    Configuration = config.Build();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddDataAccess(Configuration);
                    services.AddAppServices(Configuration);

                    services.AddHostedService<DataGeneratorService>();
                });

            await builder.RunConsoleAsync();
        }
    }
}
