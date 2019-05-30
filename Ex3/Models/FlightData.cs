using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    [Serializable]
    public class FlightData
    {
        public Location FlightLocation { get; set; }
        public double Throttle { get; set; }
        public double Rudder { get; set; }
    }
}