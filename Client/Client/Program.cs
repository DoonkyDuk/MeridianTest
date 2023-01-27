using System.Net;
using System.Net.Sockets;
using System.Text;


string? input;
do
{
    using TcpClient tcpClient = new TcpClient();
    await tcpClient.ConnectAsync(IPAddress.Loopback, 20000);
    var stream = tcpClient.GetStream();

    var buffer = new byte[512];
    int bytes = await stream.ReadAsync(buffer);

    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, bytes));
    tcpClient.Close();

    input = Console.ReadLine();
}
while (input != "f");