namespace TestTcpServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class TestServer
    {
        private string[] data = new string[] 
        { 
            "#90#010102#27100322;100323#91", 
            "#90#010102#27100322;100323#91", 
            "#90#010102#27100322;100323#91", 
            "#90#010102#27100322;100323#91" 
        };

        private IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000);
        private TcpListener server;
        public TestServer()
        {
            server = new TcpListener(ipEndPoint);
        }

        public async void StartServer()
        {
            try
            {
                server.Start();
                Console.WriteLine("Server started");

                while (true)
                {
                    using var client = await server.AcceptTcpClientAsync();
                    Console.WriteLine($"Opened connection: {client.Client.RemoteEndPoint}");
                }
            }

            finally
            {
                server.Stop();
            }
        }
    }
}
