using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using MemberPro.Api.Swagger;
using MemberPro.Core.Data;
using MemberPro.Core.Security;
using MemberPro.Core.Services;
using MemberPro.Core.Services.Members;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MemberPro.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDataAccess(Configuration);
            services.AddHttpContextAccessor();
            services.AddAppServices(Configuration);

            services.AddCors(x =>
            {
                x.AddPolicy("memberpro", policy =>
                {
                    policy.WithOrigins(Configuration["CORS:AllowedOrigin"]);
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowCredentials();
                });
            });

            var awsCognitoUrl = Configuration["AWS:CognitoUrl"];
            var awsCognitoAuthUrlBase = Configuration["AWS:CognitoAuthUrlBase"];
            var awsCognitoApiClientId = Configuration["AWS:UserPoolClientId"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = awsCognitoUrl;
                    options.Audience = awsCognitoApiClientId;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = awsCognitoUrl,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateLifetime = false,
                        ValidateAudience = false
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var userSubjectId = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IMemberService>();
                            var user = await userService.FindBySubjectIdAsync(userSubjectId);

                            if (user != null)
                            {
                                var appClaims = new List<Claim>
                                {
                                    new Claim(AppClaimTypes.UserId, user.Id.ToString(CultureInfo.InvariantCulture), null, AppClaimTypes.AppClaimsIssuer),
                                };

                                var appIdentity = new ClaimsIdentity(appClaims);
                                context.Principal.AddIdentity(appIdentity);
                            }
                        }
                    };
                });

            services.AddAuthorization();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MemberPro API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    OpenIdConnectUrl = new Uri($"{awsCognitoUrl}/.well-known/openid-configuration"),
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{awsCognitoAuthUrlBase}oauth2/authorize"),
                            TokenUrl = new Uri($"{awsCognitoAuthUrlBase}oauth2/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "" },
                                { "profile", "" }
                            }
                        }
                    }
                });

                // Security requirement is only applied to operations that require auth
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("memberpro");

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "MemberPro API v1");
                config.RoutePrefix = "api-docs";
                config.DocumentTitle = "MemberPro API";

                config.OAuthClientId(configuration["AWS:SwaggerClientId"]);
                config.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
                {
                    { "nonce", Guid.NewGuid().ToString().Replace("-", "") }
                });

                config.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

                config.EnableDeepLinking();
            });

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
