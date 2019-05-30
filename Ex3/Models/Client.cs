using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;

namespace Ex3.Models
{
    public class Client
    {
        #region Singleton
        private static Client myInstance = null;
        public static Client Instance
        {
            get
            {
                if (myInstance == null)
                {
                    myInstance = new Client();
                }
                return myInstance;
            }
        }
        #endregion

        #region Private members
        private TcpClient client;
        #endregion

        #region Constructor
        private Client()
        {
            client = new TcpClient();
        }
        #endregion

        #region Properties
        public bool IsConnected
        {
            get
            {
                return client.Connected;
            }
        }
        #endregion

        #region Private functions
        private IPEndPoint GetServerAddress(string ip, int port)
        {
            //get simulator server IP and port
            IPAddress ipAddress = IPAddress.Parse(ip);
            return new IPEndPoint(ipAddress, port);
        }
        #endregion

        #region Public functions 
        public bool ConnectToServer(string ip, int port)
        {
            if (ip == String.Empty || port <= 0)
            {
                return false;
            }
            IPEndPoint serverAddress = GetServerAddress(ip, port);
            try
            {
                //loop until managed to connect the simulator
                while (!client.Connected)
                {
                    client.Connect(serverAddress);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return client.Connected;
        }

        public void CloseConnection()
        {
            client.Close();
        }

        public string GetResponse(string command)
        {
            int offset = 0, responseMaxSize = 1024;

            if (!client.Connected)
            {
                return String.Empty;
            }

            //the simulator expects the command string to end with new-line.
            string newLine = "\r\n";
            if (!command.EndsWith(newLine))
            {
                command += newLine;
            }
            NetworkStream clientStream = client.GetStream();
            byte[] bytesMsg = Encoding.ASCII.GetBytes(command);

            //send the command to the server
            clientStream.Write(bytesMsg, offset, bytesMsg.Length);

            byte[] bufferResponse = new byte[responseMaxSize];
            int bytesRead = clientStream.Read(bufferResponse, offset, bufferResponse.Length);
            string response = Encoding.ASCII.GetString(bufferResponse, offset, bytesRead);

            return response;
        }
        #endregion
    }
}