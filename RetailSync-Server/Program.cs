using System;
using RetailSync_Server;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var server = new SocketServer(5000);
        Console.CancelKeyPress += (sender, e) =>
        {
            e.Cancel = true;
            server.Stop();
        };
        await server.StartAsync();
    }
}