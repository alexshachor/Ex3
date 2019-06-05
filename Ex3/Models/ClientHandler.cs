using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class ClientHandler
    {
        private Dictionary<string, string> commandsMap;

        public ClientHandler()
        {
            //maps the property and the matching message
            commandsMap = new Dictionary<string, string>
            {
                {"lon", "get /position/longitude-deg"},
                {"lat","get /position/latitude-deg"},
                {"rudder", "get /controls/flight/rudder"},
                {"throttle", "get /controls/engines/current-engine/throttle"}
            };
        }

        public Location GetLocation(string ip, int port)
        {
            Location location = null;
            Client client = Client.Instance;

            if (!client.IsConnected)
            {
                client.ConnectToServer(ip, port);
            }

            if (client.IsConnected)
            {
                string lon = client.GetResponse(commandsMap["lon"]);
                string lat = client.GetResponse(commandsMap["lat"]);

                location = new Location()
                {
                    Lat = getValueFromResponse(lat),
                    Lon = getValueFromResponse(lon)
                };
            }
            return location;
        }

        public FlightData GetFlightData(string ip, int port)
        {
            FlightData flightData = null;
            Client client = Client.Instance;
            if (!client.IsConnected)
            {
                client.ConnectToServer(ip, port);
            }
            if (client.IsConnected)
            {
                string rudder = client.GetResponse(commandsMap["rudder"]);
                string throttle = client.GetResponse(commandsMap["throttle"]);

                flightData = new FlightData()
                {
                    FlightLocation = GetLocation(ip, port),
                    Rudder = getValueFromResponse(rudder),
                    Throttle = getValueFromResponse(throttle)
                };
            }
            return flightData;
        }

        private double getValueFromResponse(string response)
        {
            if (response == String.Empty)
            {
                return 0;
            }

            string[] separated = response.Split('\'');
            if (separated.Length < 3)
            {
                return 0;
            }

            double value = 0;
            double.TryParse(separated[1], out value);
            return value;
        }
    }
}