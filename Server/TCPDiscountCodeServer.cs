using System.Net;
using System.Net.Sockets;
using System.Text;

class TCPDiscountCodeServer
{
    private static readonly DiscountCodeManager manager = new();

    public static void StartServer()
    {
        // Bind to port 13000
        var server = new TcpListener(IPAddress.Any, 13000);
        server.Start();
        Console.WriteLine("Server started and waiting for requests...");

        while (true)
        {
            var client = server.AcceptTcpClient();
            Console.WriteLine("Connected to client.");

            // Handle each client in a separate thread
            ThreadPool.QueueUserWorkItem(HandleClient, client);
        }
    }

    private static void HandleClient(object? obj)
    {
        if (obj != null)
        {
            var client = (TcpClient)obj;
            var stream = client.GetStream();

            var buffer = new byte[256];
            var bytesRead = stream.Read(buffer, 0, buffer.Length);

            var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            var response = ProcessRequest(request);

            var responseBytes = Encoding.UTF8.GetBytes(response);
            stream.Write(responseBytes, 0, responseBytes.Length);

            client.Close();
        }
    }

    private static string ProcessRequest(string request)
    {
        // delimiter-based protocol
        var parts = request.Split('|');
        var command = parts[0];

        if (command == "GENERATE")
        {
            var count = ushort.Parse(parts[1]);
            var length = byte.Parse(parts[2]);
            var success = manager.GenerateDiscountCodes(count, length);
            return success ? "SUCCESS" : "FAILURE";
        }
        else if (command == "USECODE")
        {
            var code = parts[1];
            var success = manager.UseDiscountCode(code);
            return success ? "CODE USED" : "CODE NOT FOUND OR USED";
        }

        return "INVALID COMMAND";
    }
}
