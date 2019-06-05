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
                Session["FileName"] = ip;
                Session["Interval"] = port;
                return View("ViewFlightData");
            }
        }

        [HttpGet]
        public ActionResult GetLocation(string ip, int port)
        {
            ClientHandler clientHandler = new ClientHandler();
            Location currentLocation = clientHandler.GetLocation(ip, port);

            return Json(currentLocation, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFlightData(string ip, int port)
        {
            ClientHandler clientHandler = new ClientHandler();
            //FlightData flightData = clientHandler.GetFlightData(ip, port);
            Random rnd = new Random();
            Location currentLocation = new Location();
            currentLocation.Lat = rnd.NextDouble() * 50;
            currentLocation.Lon = rnd.NextDouble() * 50;
            FlightData flightData = new FlightData()
            {
                FlightLocation = currentLocation,
                Throttle = rnd.NextDouble() * 50,
                Rudder = rnd.NextDouble() * 50
            };
            return Json(flightData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFlightDataListFromFile(string fileName)
        {
            if (fileName == String.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "File name is empty");
            }

            FileManager<List<FlightData>> fManager = new FileManager<List<FlightData>>();
            List<FlightData> flightDataList = fManager.LoadData(fileName);

            return Json(flightDataList, JsonRequestBehavior.AllowGet);
        }

    }
}