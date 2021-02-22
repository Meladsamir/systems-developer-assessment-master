using System.Web.Mvc;
using System.Web.Routing;

namespace NetC.JuniorDeveloperExam.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "BlogPosts", action = "Home", id = UrlParameter.Optional },
                   namespaces: new[] { "NetC.JuniorDeveloperExam.Web.Controllers" }
                );


            //routes.MapRoute(
            //         name: "Blog",
            //         url: "Blog/{id}",
            //         defaults: new
            //         {
            //             controller = "Blog",
            //             action = "Blog",
            //             id = UrlParameter.Optional
            //         },
            //       namespaces: new[] { "NetC.JuniorDeveloperExam.Web.Controllers" }
            //     );


        }


    }
}
