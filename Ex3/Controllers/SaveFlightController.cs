using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ex3.Controllers
{
    public class SaveFlightController : Controller
    {
        [HttpGet]
        public ActionResult FlightData(string ip, int port, double interval, double duration, string fileName)
        {
            return View();
        }

        public bool SaveFlightData(string fileName, List<FlightData> flightData)
        {
            if (fileName == String.Empty || flightData.Count == 0)
            {
                return false;
            }

            FileManager<List<FlightData>> fManager = new FileManager<List<FlightData>>();
            return fManager.SaveData(fileName, flightData);
        }
    }
}