using FoxTwoLabs.Workflow.Application.Workflows.Queries;
using FoxTwoLabs.Workflow.Infrastructure.Data;
using FoxTwoLabs.Workflow.Infrastructure.Options;
using FoxTwoLabs.Workflow.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace MyWebApplication
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

            services.AddCors(options => {
                options.AddPolicy("AllowMyOrigin",
                builder => builder.WithOrigins("*"));
            });

            
            // services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<WorkflowDbContext>
                  (o => o.UseSqlServer(Configuration.
                    GetConnectionString("WorkflowDbContext")));

            services.AddSwaggerGen();

            // Register MediatR 
            var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            services.AddAutoMapper(domainAssemblies);
            services.AddMediatR(typeof(GetWorkflowsQueryHandler));

            services.AddTransient<IAuthorizationHandler, APIKeyRequirementHandler>();
            var apiKey = Configuration.GetSection(nameof(AppOptions))[nameof(AppOptions.ApiKey)];
            services.AddAuthorization(authConfig =>
            {
                authConfig.AddPolicy("ApiKeyPolicy",
                    policyBuilder => policyBuilder
                        .AddRequirements(new APIKeyAuthorizationRequirement(apiKey.ToString())));
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

          app.UseRouting();
          app.UseCors("AllowMyOrigin");
          app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
           });

      

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

         


        }
    }
}
