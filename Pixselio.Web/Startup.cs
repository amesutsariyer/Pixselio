using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Pixselio.Business.Managers;
using Pixselio.Business.Services;
using Pixselio.Core.Mapper;
using Pixselio.Data;
using Pixselio.Entity;
using Pixselio.Web.Settings;

namespace Pixselio.Web
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
            #region DbContext

            services.AddDbContext<IdentityDbContext>(_ => _.UseSqlServer(Configuration["ConnectionStrings:IdentityConnection"]), ServiceLifetime.Transient);
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();

            #endregion

            #region Identity Options

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie();
            #endregion

            #region IdentityCookie Options
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Admin/Login";
                options.AccessDeniedPath = "/Home/Error";
                options.SlidingExpiration = true;
            });
            #endregion

            #region AutoMapper

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            // Add our Config object so it can be injected
            services.Configure<SettingsMapModel>(Configuration.GetSection("SettingsMapModel"));
            #region Services
            services.AddScoped<IUserService, Business.Managers.UserManager>();
            services.AddScoped<IPhotoService, PhotoManager>();
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IGenericRepository<Tag>, GenericRepository<Tag>>();
            services.AddScoped<IGenericRepository<Photo>, GenericRepository<Photo>>();
            services.AddScoped<IGenericRepository<PhotosTag>, GenericRepository<PhotosTag>>();

            services.AddIdentityCore<User>();
            services.TryAddScoped<UserManager<User>>();
            services.TryAddScoped<SignInManager<User>>();
        
            #endregion


            services.AddControllersWithViews();
            IMvcBuilder builder = services.AddRazorPages();

#if DEBUG
            builder.AddRazorRuntimeCompilation();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           // InitializeDatabase(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        //private static void InitializeDatabase(IApplicationBuilder app)
        //{
        //    using (var serviceScope = app.ApplicationServices
        //        .GetRequiredService<IServiceScopeFactory>()
        //        .CreateScope())
        //    {
        //        using (var context = serviceScope.ServiceProvider.GetService<IdentityDbContext>())
        //        {
        //            context.Database.Migrate();
        //        }
        //    }
        //}
    }
}
