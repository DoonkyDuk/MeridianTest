using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestService
{
    public class DataSender
    {
        DataReceiver dataReceiver;
        TcpListener tcpListener;

        public DataSender(IPEndPoint serverAddress)
        {
            dataReceiver = new DataReceiver();
            tcpListener = new TcpListener(serverAddress);
        }
        public async Task Start()
        {
            try
            {
                tcpListener.Start();
                Console.WriteLine($"Service started: Address: {tcpListener.Server.LocalEndPoint}");

                while (true)
                {
                    using var tcpClient = await tcpListener.AcceptTcpClientAsync();

                    List<string> recievedData = await dataReceiver.GetDataFromServers();
                    string result = Helper.BuildOutputData(recievedData);

                    var stream = tcpClient.GetStream();

                    await stream.WriteAsync(Encoding.UTF8.GetBytes(result));

                    Console.WriteLine($"Result has been sent to {tcpClient.Client.RemoteEndPoint}");
                    recievedData.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                tcpListener.Stop();
            }
        }

        public void Stop()
        {
            tcpListener.Stop();
        }
    } 
}
