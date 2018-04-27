using FinpeApi.Banks;
using FinpeApi.Categories;
using FinpeApi.Models;
using FinpeApi.Overviews;
using FinpeApi.Statements;
using FinpeApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinpeApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FinpeDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<FinpeDbContext, FinpeDbContext>();
            services.AddTransient<StatementRepository, StatementRepository>();
            services.AddTransient<CategoryRepository, CategoryRepository>();
            services.AddTransient<BankRepository, BankRepository>();
            services.AddTransient<IDateService, DateService>();
            services.AddTransient<StatementController, StatementController>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options => 
                options.AddPolicy("CorsPolicy", builder => {
                    builder
                        .WithOrigins("http://localhost:8080", "https://finpe-app.firebaseapp.com")
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader();
                }));
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<OverviewHub>("/hubs/overview");
            });
            app.UseMvc();
        }
    }
}
