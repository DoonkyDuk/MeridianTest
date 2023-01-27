using System.Net;
using System.Net.Sockets;
using System.Text;

var tcpListener = new TcpListener(IPAddress.Any, 10001);
Random rand = new Random();

string[] dataArray = new string[] 
{
    "#90#010102#27100321;100323#91",
    "#90#010102#27100322;100323#91",
    "#90#010102#27100323;100320#91",
    "#90#010102#27100324;100319#91",
    "#90#010102#27100325;100323#91",
    "#90#010102#27100326;100323#91",
};
try
{
    tcpListener.Start();
    Console.WriteLine("Server started. Waiting for connection.");

    while (true)
    {
        using var tcpClient = await tcpListener.AcceptTcpClientAsync();

        int dataArrayIndex = rand.Next(0, dataArray.Length - 1);

        var stream = tcpClient.GetStream();
        byte[] data = Encoding.UTF8.GetBytes(dataArray[dataArrayIndex]);
        await stream.WriteAsync(data);

        Console.WriteLine($"{tcpClient.Client.RemoteEndPoint}: Data has been sent: {dataArray[dataArrayIndex]}");
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