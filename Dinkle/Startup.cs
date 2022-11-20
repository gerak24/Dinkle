using System;
using System.Reflection;
using Dinkle.Application.Accounts.Utils;
using Dinkle.Core.Buses;
using Dinkle.Infrastructure.Buses;
using Dinkle.Infrastructure.Database;
using Dinkle.Infrastructure.Piplines;
using FluentValidation.AspNetCore;
using Marten;
using Marten.Services;
using Marten.Services.Json;
using Marten.Storage;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Weasel.Core;

namespace Dinkle
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [Obsolete("Obsolete")]
        public void ConfigureServices(IServiceCollection services)
        {
            #region Mediatrr

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipline<,>));
            services.AddScoped<ICommandBus, MediatrCommandBus>();
            services.AddScoped<IQueryBus, MediatrQueryBus>();
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #endregion

            #region Marten

            services.AddDatabaseManager();
            services.AddMarten(options =>
            {
                options.Connection(Configuration.GetConnectionString("service"));
                options.UseDefaultSerialization(
                    nonPublicMembersStorage: NonPublicMembersStorage.NonPublicConstructor |
                                             NonPublicMembersStorage.NonPublicSetters,
                    serializerType: SerializerType.SystemTextJson
                );
                options.Serializer(new SystemTextJsonSerializer {EnumStorage = EnumStorage.AsString});
                options.Events.TenancyStyle = TenancyStyle.Conjoined;
                options.Policies.AllDocumentsAreMultiTenanted();
            });

            #endregion

            #region Authentication

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });

            #endregion

            #region Services

            services.AddMemoryCache();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #endregion

            services.AddControllers()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}