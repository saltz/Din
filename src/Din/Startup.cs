using Din.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession(options => { options.Cookie.Name = "DinCookie"; });
            var mysqlConnectionString = Configuration.GetConnectionString("MysqlConnectionString");
            services.AddDbContext<DinWebsiteContext>(options =>
                options.UseMySql(
                    mysqlConnectionString)
            );
            services.AddLocalization(o => o.ResourcesPath = "Resources");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute("Login", "Home",
                    defaults: new { controller = "Authentication", action = "Login" });
                routes.MapRoute("Logout", "Logout",
                    defaults: new {controller = "Authentication", action = "Logout"});
                routes.MapRoute("SearchMovie", "MovieResults",
                    defaults: new {controller = "Content", Action = "SearchMovie"});
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
           
            });
        }
    }
}
