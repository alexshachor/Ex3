using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class ClientHandler
    {
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
                string getLatCmd = "get /position/longitude-deg";
                string getLonCmd = "get /position/latitude-deg";
                string lat = client.GetResponse(getLatCmd);
                string lon = client.GetResponse(getLonCmd);

                location = new Location();
                location.Lat = getValueFromString(lat);
                location.Lon = getValueFromString(lon);
            }
            return location;
        }

        private double getValueFromString(string response)
        {
            return 0;
        }
    }
}