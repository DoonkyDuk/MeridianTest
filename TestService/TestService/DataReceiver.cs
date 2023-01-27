namespace TestService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Sockets;
    using System.Net;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;

    public class DataReceiver
    {
        private TcpClient tcpClient;
        private List<string> recievedStrings;
        private List<IPEndPoint> serversEndPoints;

        public DataReceiver()
        {
            recievedStrings = new List<string>();
            serversEndPoints = new List<IPEndPoint>();
            serversEndPoints = GetIPEndPointsFromSettings("..\\..\\..\\Servers.json");
        }
        private async Task<string> GetData()
        {
            byte[] buffer = new byte[512];
            var stream = tcpClient.GetStream();

            int result = await stream.ReadAsync(buffer);

            string data = Encoding.UTF8.GetString(buffer, 0, result);
            return data;
            //Console.WriteLine($"Data received: {data}");
        }

        public async Task<List<string>> GetDataFromServers()
        {
            foreach (var serverEndPoint in serversEndPoints)
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(serverEndPoint);
                recievedStrings.Add(await GetData());
                tcpClient.Close();
            }

            return recievedStrings;
        }

        private List<IPEndPoint> GetIPEndPointsFromSettings(string settingPath)
        {
            List<IPEndPoint> result = new List<IPEndPoint>();

            JObject setting = JObject.Parse(File.ReadAllText(settingPath));
            var servers = setting.Value<JObject>("Servers").Properties();

            foreach (var server in servers)
            {
                var ipAddress = IPAddress.Parse(server.Value.SelectToken("IPAddress").ToString());
                var port = int.Parse(server.Value.SelectToken("Port").ToString());

                result.Add(new IPEndPoint(ipAddress, port));
            }

            return result;
        }
    }
}
