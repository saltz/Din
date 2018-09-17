using AutoMapper;
using Din.Config;
using Din.Data;
using Din.Mapping.Profiles;
using Din.Service.Clients.Concrete;
using Din.Service.Clients.Interfaces;
using Din.Service.Config.Concrete;
using Din.Service.Config.Interfaces;
using Din.Service.Services.Concrete;
using Din.Service.Services.Interfaces;
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
                .AddEncryptedProvider();
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

            //HttpClientFactory
            services.AddHttpClient();

            //Inject Configurations
            services.AddSingleton<IDownloadClientConfig>(new DownloadClientConfig(Configuration["DownloadClient:Url"],
                Configuration["DownloadClient:Pwd"]));
            services.AddSingleton<IGiphyClientConfig>(new GiphyClientConfig(Configuration["GiphyClient:Url"],
                Configuration["GiphyClient:Key"]));
            services.AddSingleton<IIpStackClientConfig>(new IpStackClientConfig(Configuration["IpStackClient:Url"],
                Configuration["IpStackClient:Key"]));
            services.AddSingleton<IMovieClientConfig>(new MovieClientConfig(Configuration["MovieClient:Url"],
                Configuration["MovieClient:Key"], Configuration["MovieClient:SaveLocation"]));
            services.AddSingleton<ITvShowClientConfig>(new TvShowClientConfig(Configuration["TvShowClient:Url"],
                Configuration["TvShowClient:Key"], Configuration["TvShowClient:SaveLocation"]));
            services.AddSingleton<IUnsplashClientConfig>(new UnsplashClientConfig(Configuration["UnsplashClient:Url"],
                Configuration["UnsplashClient:Key"]));
            services.AddSingleton<ITMDBClientConfig>(new TMDBClientConfig(Configuration["TMDBClient:Key"]));

            //Inject Services
            services.AddSingleton<IMediaService, MediaService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<ITvShowService, TvShowService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IStatusCodeService, StatusCodeService>();

            //Injecting Clients
            services.AddTransient<IDownloadClient, DownloadClient>();
            services.AddTransient<IGiphyClient, GiphyClient>();
            services.AddTransient<IIpStackClient, IpStackClient>();
            services.AddTransient<IMovieClient, MovieClient>();
            services.AddTransient<ITvShowClient, TvShowClient>();
            services.AddTransient<IUnsplashClient, UnsplashClient>();

            //Initialize Mapper Profiles
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelProfile());
                cfg.AddProfile(new EntityProfile());
                cfg.AddProfile(new DtoProfile());
            }));

            services.AddSingleton<IMapper>(mapper);
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
                    defaults: new {controller = "Main", action = "Exit"});
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}