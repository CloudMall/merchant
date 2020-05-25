using CloudMall.Services.Merchant.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WeihanLi.Common;
using WeihanLi.Common.Helpers;
using WeihanLi.Common.Services;
using WeihanLi.EntityFramework;
using WeihanLi.EntityFramework.Audit;
using WeihanLi.Web.Extensions;

namespace CloudMall.Services.Merchant
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

            services.AddDbContext<MerchantDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Merchant")));
            services.AddEFRepository();

            // register HttpContextUserIdProvider
            services.AddHttpContextUserIdProvider(options =>
            {
                options.UserIdFactory = context => $"{context.User.GetUserId()}:{context.GetUserIP()}";
            });

            // swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("merchant", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Merchant API", Version = "1.0" });

                options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"), true);
            });

            DependencyResolver.SetDependencyResolver(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                KnownProxies = { },
                KnownNetworks = { },
                ForwardLimit = null,
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseStaticFiles();
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.RoutePrefix = string.Empty;
                    c.SwaggerEndpoint($"/swagger/merchant/swagger.json", "merchant API");
                    c.DocumentTitle = "Merchant API";
                });

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Init(app.ApplicationServices);
        }

        private void Init(IServiceProvider serviceProvider)
        {
            var userIdProvider = serviceProvider.GetService<IUserIdProvider>();
            AuditConfig.Configure(builder =>
            {
                builder.WithUserIdProvider(userIdProvider);
                builder.EnrichWithProperty("Application", ApplicationHelper.ApplicationName);
            });
        }
    }
}