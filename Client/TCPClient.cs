using System.Net.Sockets;
using System.Text;

public class TCPClient()
{
    private const string ServerAddress = "127.0.0.1";
    private const int ServerPort = 13000;

    public static async Task GenerateDiscountCodes(ushort count, byte length)
    {
        var message = $"GENERATE|{count}|{length}";

        await ServerCall(message);
    }

    public static async Task UseDiscountCode(string code)
    {
        var message = $"USECODE|{code}";

        await ServerCall(message);
    }

    private static async Task ServerCall(string message)
    {
        var _tcpClient = new TcpClient(ServerAddress, ServerPort);
        var stream = _tcpClient.GetStream();

        var data = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(data);

        var responseBuffer = new byte[256];
        int bytesRead = await stream.ReadAsync(responseBuffer);

        var response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
        Console.WriteLine($"Server response: {response}\n\n\n");
    }
}
