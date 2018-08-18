using Din.Data;
using Din.Service.Classes;
using Din.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Din
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
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { options.LoginPath = "/"; });
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession(options => { options.Cookie.Name = "DinCookie"; });
            var mysqlConnectionString = Configuration.GetConnectionString("MysqlConnectionString");
            services.AddDbContext<DinContext>(options =>
                options.UseMySql(
                    mysqlConnectionString)
            );

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Adding My Services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IContentService, ContentService>();
        }

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
            app.UseForwardedHeaders();
            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute("Login", "",
                    defaults: new {controller = "Authentication", action = "LoginAsync"});
                routes.MapRoute("Logout", "Logout",
                    defaults: new {controller = "Authentication", action = "LogoutAsync"});
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}