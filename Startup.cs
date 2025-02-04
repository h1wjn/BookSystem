using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using BooksStore.Pages;
using BooksSystem.Data;

namespace BooksSystem
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<AuthenticationService>();
            services.AddAuthorizationCore();
            services.AddDbContext<BooksContext>(options =>
            options.UseMySql(
            Configuration.GetConnectionString("BooksDBConnection"),
            new MySqlServerVersion(new Version(8, 0, 0))
                )
            );
            // 注入 DatabaseBackupService，获取配置
            services.AddSingleton<DatabaseBackupService>(provider =>
            {
                string server = Configuration["Database:Server"];
                string user = Configuration["Database:User"];
                string password = Configuration["Database:Password"];
                string databaseName = Configuration["Database:Name"];

                return new DatabaseBackupService(server, user, password, databaseName);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            // 获取 DatabaseBackupService 并触发备份操作
            var backupService = app.ApplicationServices.GetRequiredService<DatabaseBackupService>();
            backupService.BackupDatabase();  // 在应用启动时进行备份

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }

}
