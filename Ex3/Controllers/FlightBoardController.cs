using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ex3.Controllers
{
    public class FlightBoardController : Controller
    {
        // GET: Display
        [HttpGet]
        public ActionResult DisplayFlight(string ip, int port, double interval)
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetFlightData(string ip, int port, double interval)
        {
            ClientHandler clientHandler = new ClientHandler();
            //Location currentLocation = clientHandler.GetLocation(ip, port);
            Location currentLocation = new Location();
            currentLocation.Lat = 100;
            currentLocation.Lon = 10;
            return Json(currentLocation, JsonRequestBehavior.AllowGet);
        }
    }
}