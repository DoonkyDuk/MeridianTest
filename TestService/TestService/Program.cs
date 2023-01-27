using System.Net;
using System.Net.Sockets;
using System.Text;
using TestService;


IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 20000);
DataSender dataSender = new DataSender(endPoint);

await dataSender.Start();








