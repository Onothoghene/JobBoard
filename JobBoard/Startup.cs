using AutoMapper;
using JobBoard.Handlers;
using JobBoard.Logic;
using JobBoard.Logic.Interfaces;
using JobBoard.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace JobBoard
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllersWithViews();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddDbContext<JobBoardContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDBContext>(option => 
            option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<ApplicationUser, IdentityRole>(
              options =>
              {
                  //options.Password.RequireDigit = false;
                  //options.Password.RequiredLength = 4;
                  //options.Password.RequireNonAlphanumeric = false;
                  //options.Password.RequireUppercase = false;
                  //options.Password.RequireLowercase = false;
              }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();


            //services.AddAuthentication();
            services.AddAuthentication().AddCookie("UserData", config =>{
                config.LoginPath = "/Account/Authenticate";
                config.LogoutPath = "/Account/LogOut";
            });
            services.AddAuthorization();

            

            services.AddSingleton(Configuration);
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IJob, EFJob>();
            services.AddScoped<IAccount, EFAccount>();
            services.AddScoped<IUser, EFUser>();
            services.AddScoped<IRoles, EFRoles>();
            services.AddScoped<ISearch, EFSearch>();

            //Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingAction());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
