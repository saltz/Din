using Din.Config;
using Din.Data;
using Din.Service.Concrete;
using Din.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Din
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddEncryptedProvider()
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

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
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options => { options.Cookie.Name = "DinCookie"; });
            services.AddDbContext<DinContext>(options =>
                options.UseMySql(Configuration["Database:ConString"])
            );

            //Inject Configuration
            services.AddSingleton<IConfiguration>(Configuration);

            //Inject Services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IContentService, ContentService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IStatusCodeService, StatusCodeService>();
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