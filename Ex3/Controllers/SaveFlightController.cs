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
        public ActionResult SaveFlight(string ip, int port, double interval, double duration, string fileName)
        {
            if (fileName == String.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "File name is empty");
            }

            Session["IP"] = ip;
            Session["Port"] = port;
            Session["Interval"] = interval;
            Session["Duration"] = duration;
            Session["FileName"] = fileName;

            return View();
        }

        [HttpPost]
        public ActionResult SaveFlightDataList(string fileName, List<FlightData> flightDataList)
        {

            ClientHandler clientHandler = new ClientHandler();
            clientHandler.CloseConnection();

            if (fileName == String.Empty || flightDataList == null || flightDataList.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "File name or data is missing");
            }

            FileManager<List<FlightData>> fManager = new FileManager<List<FlightData>>();
            bool hasDataSaved = fManager.SaveData(fileName, flightDataList);
            if (!hasDataSaved)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Saving data has failed");
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}