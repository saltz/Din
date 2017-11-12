using System.Web.Mvc;
using System.Web.Routing;

namespace Din_Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "login",
                url: "Home",
                defaults: new { controller = "Home", action = "Login" }
            );

            routes.MapRoute(
                name: "logout",
                url: "Logout",
                defaults: new { controller = "Home", action = "Logout" }
            );

            routes.MapRoute(
                name: "searchResults",
                url: "Results",
                defaults: new { controller = "Main", action = "SearchMovie" }
            );

            routes.MapRoute(
                name: "addMovie",
                url: "AddMovie",
                defaults: new { controller = "Main", action = "AddMovie" }
            );

            routes.MapRoute(
                name: "MovieInfo",
                url: "MovieInfo",
                defaults: new { controller = "Main", action = "GetMovieStatus" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
