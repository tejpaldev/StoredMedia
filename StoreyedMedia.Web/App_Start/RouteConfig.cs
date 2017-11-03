using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StoreyedMedia.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            //routes.MapRoute( "Contact",  "Contact/StarAContact/{id}", new { controller = "Contact", action = "StarAContact", id = UrlParameter.Optional });

            //routes.MapRoute("Alert", "Alert/GetAlertDetails/{id}",  new { controller = "Alert", action = "GetAlertDetails", id = UrlParameter.Optional });

            routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
         name: "Categories",
         url: "{controller}/{action}/{id}",
         defaults: new { controller = "Categories", action = "Index", id = UrlParameter.Optional }
           );


            routes.MapRoute(
           name: "Tags",
           url: "{controller}/{action}/{id}",
           defaults: new { controller = "Tags", action = "Index", id = UrlParameter.Optional }
             );


        }
    }
}
