﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Display", "display/{ip}/{port}/{interval}",
              defaults: new { controller = "FlightBoard", action = "DisplayFlight", interval = "0" });

            routes.MapRoute("GetFlightData", "flightData/{ip}/{port}",
              defaults: new { controller = "FlightBoard", action = "GetFlightData" });

            routes.MapRoute("GetLocation", "flightData/location/{ip}/{port}",
              defaults: new { controller = "FlightBoard", action = "GetLocation" });

            routes.MapRoute("GetFlightDataFromFile", "flightData/{fileName}",
              defaults: new { controller = "FlightBoard", action = "GetFlightDataListFromFile" });


            routes.MapRoute("Save", "save/{ip}/{port}/{interval}/{duration}/{fileName}",
                defaults: new { controller = "SaveFlight", action = "SaveFlight" });


            routes.MapRoute("SaveFlightDataList", "save/{fileName}",
                defaults: new { controller = "SaveFlight", action = "SaveFlightDataList" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
