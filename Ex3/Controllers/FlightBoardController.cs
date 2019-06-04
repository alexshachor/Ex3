using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            IPAddress ipAddr;
            if (IPAddress.TryParse(ip, out ipAddr))
            {
                Session["IP"] = ip;
                Session["Port"] = port;
                Session["Interval"] = interval;
                return View("DisplayFlight");
            }
            else
            {
                return View("LoadFlightData");
            }
        }


        [HttpGet]
        public ActionResult GetLocation(string ip, int port, double interval)
        {
            ClientHandler clientHandler = new ClientHandler();
            //Location currentLocation = clientHandler.GetLocation(ip, port);
            Random rnd = new Random();

            Location currentLocation = new Location();
            currentLocation.Lat = rnd.NextDouble()*30;
            currentLocation.Lon = rnd.NextDouble() * 30;
            return Json(currentLocation, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFlightData(string fileName)
        {
            if (fileName == String.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "File Not Found");
            }

            FileManager<List<FlightData>> fManager = new FileManager<List<FlightData>>();
            List<FlightData> flightData = fManager.LoadData(fileName);

            return Json(flightData, JsonRequestBehavior.AllowGet);
            // TODO return flightData;
        }
    }
}