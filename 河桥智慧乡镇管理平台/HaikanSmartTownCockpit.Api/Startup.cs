using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using HaikanSmartTownCockpit.Api.Auth;
using HaikanSmartTownCockpit.Api.Configurations;
using HaikanSmartTownCockpit.Api.Extensions.AuthContext;
using HaikanSmartTownCockpit.Api.Extensions.CustomException;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace HaikanSmartTownCockpit.Api
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
            services.AddCors(o =>
                o.AddPolicy("cors",
                builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .WithOrigins(new[] { "http://localhost:9000", "http://localhost:9001","http://localhost:8080", "*", "http://192.168.0.221:4402", "http://192.168.0.221:4401", "http://ljfl.hztlcgj.com:88", "http://127.0.0.1:4472" ,
                        "http://localhost:54321","http://192.168.0.221:4473","http://192.168.0.221:4472","http://localhost:44398","http://localhost:44399","http://haikan.51vip.biz:55082","http://112.17.130.233:4472","http://112.17.130.233:4473","http://hqjscht1.hzgx.info","http://hqjscht2.hzgx.info","https://hqjscht1.hzgx.info","https://hqjscht2.hzgx.info","https://hqjscht1.hzgx.info:4472","https://hqjscht2.hzgx.info:4473","http://hqjscht1.hzgx.info:4472","http://hqjscht2.hzgx.info:4473", "http://localhost:80", "http://localhost:4398","http://172.18.19.212:4472","http://172.18.19.212:4473","http://localhost:4473","http://101.71.242.174:4472","http://101.71.242.174:4473","http://localhost:4472" })));
            services.AddMemoryCache();

            services.AddHttpContextAccessor();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppAuthenticationSettings>(appSettingsSection);
            services.Configure<MdDesEncrypt>(Configuration.GetSection("MdDesEncrypt"));

            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
            // JWT
            var appSettings = appSettingsSection.Get<AppAuthenticationSettings>();
            services.AddJwtBearerAuthentication(appSettings);

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<WebEncoderOptions>(options =>
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs)
            );

            services
                .AddMvc(config =>
                {
                    //config.Filters.Add(new ValidateModelAttribute());
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<HaikanSmartTownCockpit.Api.Entities.haikanHeQiaoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo{ Title = "RBAC Management System API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // ×¢ÈëÈÕÖ¾
            services.AddLogging(config =>
            {
                config.AddLog4Net();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseFileServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("cors");
            app.ConfigureCustomExceptionMiddleware();

            var serviceProvider = app.ApplicationServices;
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            AuthContextService.Configure(httpContextAccessor);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            //app.UseSwagger(o =>
            //{
            //    o.PreSerializeFilters.Add((document, request) =>
            //    {
            //        document.Paths = (Microsoft.OpenApi.Models.OpenApiPaths)document.Paths.ToDictionary(p => p.Key.ToLowerInvariant(), p => p.Value);
            //    });
            //});

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RBAC API V1");
            });
        }
    }
}
