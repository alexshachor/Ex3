﻿using Ex3.Models;
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

                ClientHandler clientHandler = new ClientHandler();
                clientHandler.CloseConnection();

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
            FlightData flightData = clientHandler.GetFlightData(ip, port);

            return Json(flightData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFlightDataListFromFile(string fileName)
        {
            if (fileName == String.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "file name is empty");
            }

            FileManager<List<FlightData>> fManager = new FileManager<List<FlightData>>();
            List<FlightData> flightDataList;
            try
            {
                flightDataList = fManager.LoadData(fileName);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "file not foundt");
            }

            return Json(flightDataList, JsonRequestBehavior.AllowGet);
        }

    }
}