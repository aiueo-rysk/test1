﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AddressManagement.Data;
using AddressManagement.Models;
using AddressManagement.Services;

namespace AddressManagement
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
            // DBコンテキスト
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // AddIdentityを使用する
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // 外部サービス認証を使用する
            services.AddAuthentication()
                .AddMicrosoftAccount(microsoftOptions => {
                    microsoftOptions.ClientId = Configuration["MicrosoftAPIOptions:ClientId"];
                    microsoftOptions.ClientSecret = Configuration["MicrosoftAPIOptions:ClientSecret"];
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["GoogleAPIOptions:ClientId"];
                    googleOptions.ClientSecret = Configuration["GoogleAPIOptions:ClientSecret"];
                })
                //.AddTwitter(twitterOptions => { ... })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["FacebookAPIOptions:AppId"];
                    facebookOptions.AppSecret = Configuration["FacebookAPIOptions:AppSecret"];
                });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
