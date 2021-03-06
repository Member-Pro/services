using Amazon.CognitoIdentityProvider;
using Amazon.S3;
using Amazon.SimpleNotificationService;
using MemberPro.Core.Configuration;
using MemberPro.Core.Models;
using MemberPro.Core.Services.Achievements;
using MemberPro.Core.Services.Common;
using MemberPro.Core.Services.Geography;
using MemberPro.Core.Services.Media;
using MemberPro.Core.Services.Members;
using MemberPro.Core.Services.Organizations;
using MemberPro.Core.Services.Plans;
using MemberPro.Core.Services.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace MemberPro.Core.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddMvc();

            services.AddAutoMapper(typeof(ModelMapper));

            services.AddTransient<IAchievementService, AchievementService>();
            services.AddTransient<IAchievementActivityService, AchievementActivityService>();
            services.AddTransient<IAchievementComponentService, AchievementComponentService>();
            services.AddTransient<IAttachmentService, AttachmentService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IMediaHelper, MediaHelper>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IMemberAchievementService, MemberAchievementService>();
            services.AddTransient<IMemberRoleService, MemberRoleService>();
            services.AddTransient<IMembershipPlanService, MembershipPlanService>();
            services.AddTransient<IOfficerService, OfficerService>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IRazorViewRenderer, RazorViewRenderer>();
            services.AddTransient<IRequirementService, RequirementService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IStateProvinceService, StateProvinceService>();
            services.AddTransient<IFavoriteAchievementService, FavoriteAchievementService>();
            services.AddTransient<IWorkContext, ApiWorkContext>();

            // Add the various requirement validatiors; there can be multiple
            services.AddTransient<IRequirementValidator, RequirementParameterValidator>();

            services.Configure<AwsConfig>(configuration.GetSection("AWS"));
            services.AddAWSService<IAmazonCognitoIdentityProvider>();

            services.Configure<FileStorageConfig>(configuration.GetSection("FileStorage"));

            var fileStorageProvider = configuration["FileStorage:StorageProvider"];
            if (fileStorageProvider == "AWS_S3")
            {
                services.AddDefaultAWSOptions(configuration.GetAWSOptions());
                services.AddAWSService<IAmazonS3>();
                services.AddAWSService<IAmazonSimpleNotificationService>();
                services.AddTransient<IFileStorageService, AmazonS3StorageService>();
            }
        }
    }
}
