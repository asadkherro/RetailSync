using System.Net.Sockets;
using System.Text.Json;
using RetailSync_Models.Socket;

namespace RetailSync_Server
{
    public class ClientHandler
    {
        private readonly TcpClient _tcpClient;
        private readonly NetworkStream _stream;
        private readonly SocketServer _server;
        public ClientHandler(TcpClient tcpClient, SocketServer server) 
        {
            _tcpClient = tcpClient;
            _stream = tcpClient.GetStream();
            _server = server;
        }

        public async Task HandleClientAsync()
        {
            try
            {
                byte[] buffer = new byte[4096];

                while (_tcpClient.Connected)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    string messageJson = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var message = JsonSerializer.Deserialize<SocketMessage>(messageJson);
                    var response = await _server.ProcessCommandAsync(message);
                    var responseJson = JsonSerializer.Serialize(response);
                    var responseBytes = System.Text.Encoding.UTF8.GetBytes(responseJson);

                    await _stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
            finally
            {
                _stream?.Close();
                _tcpClient?.Close();
                _server.RemoveClient(this);
            }
        }
    }
}
