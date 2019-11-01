using _50Pixels.Data;
using _50Pixels.Models;
using _50Pixels.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace _50Pixels
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(this._config.GetConnectionString("50PixelsDb")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/SignIn");
            services.AddPaging();

            services.AddScoped<ILikeService, LikeRepository>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IFileProcessor, FileProcessor>();
            services.AddScoped<IPhotoService, PhotoRepository>();
            services.AddScoped<IPhotoFileProcessor, PhotoFIleProcessor>();
            services.AddScoped<IUserSessionService, UserSessionService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
