using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ex3.Controllers
{
    public class SaveFlightController : Controller
    {
        [HttpGet]
        public ActionResult FlightData(string ip, int port, double interval, double duration, string fileName)
        {
            Session["IP"] = ip;
            Session["Port"] = port;
            Session["Interval"] = interval;
            Session["Duration"] = duration;

            return View();
        }

        [HttpPost]
        public ActionResult SaveFlightData(string fileName, List<FlightData> flightData)
        {
            if (fileName == String.Empty || flightData.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "File name or data is missing");
            }

            FileManager<List<FlightData>> fManager = new FileManager<List<FlightData>>();
            bool hasDataSaved = fManager.SaveData(fileName, flightData);
            if (!hasDataSaved)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Saving data has failed");
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}