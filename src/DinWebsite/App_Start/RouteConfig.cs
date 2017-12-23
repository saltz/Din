using System.Web.Mvc;
using System.Web.Routing;

namespace DinWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "login",
                "Home",
                new {controller = "Home", action = "Login"}
            );

            routes.MapRoute(
                "logout",
                "Logout",
                new {controller = "Home", action = "Logout"}
            );

            routes.MapRoute(
                "searchResults",
                "Results",
                new {controller = "Main", action = "SearchMovie"}
            );

            routes.MapRoute(
                "addMovie",
                "AddMovie",
                new {controller = "Main", action = "AddMovie"}
            );

            routes.MapRoute(
                "MovieInfo",
                "MovieInfo",
                new {controller = "Main", action = "GetMovieStatus"}
            );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );
        }
    }
}