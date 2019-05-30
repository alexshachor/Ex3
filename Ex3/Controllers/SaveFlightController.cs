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
        public ActionResult SaveFlightData(string ip, int port, double interval, double duration, string fileName)
        {
            return View();
        }
    }
}