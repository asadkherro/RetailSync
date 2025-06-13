using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RetailSync_Models.DbModels;
using RetailSync_Models.Socket;

namespace RetailSync_Server
{
    public class SocketServer
    {
        private TcpListener _listener;
        private bool _isRunning;
        private readonly List<ClientHandler> _clients = new List<ClientHandler>();
        private readonly AppDbContext _context;

        public SocketServer(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public async Task StartAsync()
        {
            _listener.Start();
            _isRunning = true;
            Console.WriteLine($"Server started on port {((IPEndPoint)_listener.LocalEndpoint).Port}");

            while (_isRunning)
            {
                try
                {
                    var tcpClient = await _listener.AcceptTcpClientAsync();
                    var clientHandler = new ClientHandler(tcpClient, this);
                    _clients.Add(clientHandler);
                    _ = Task.Run(() => clientHandler.HandleClientAsync());
                }
                catch (ObjectDisposedException)
                {

                    break;
                }
            }

        }

        public void Stop()
        {
            _isRunning = false;
            _listener?.Stop();
            _context.Dispose();
        }

        public async Task<SocketResponse> ProcessCommandAsync(SocketMessage message)
        {
            try
            {
                switch (message.Command.ToUpper())
                {
                    case "ADD_USER":
                        var newUser = JsonSerializer.Deserialize<UserModel>(message.Data);
                        var result = await _context.Users.AddAsync(newUser);
                        var ress = await _context.SaveChangesAsync();

                        return new SocketResponse
                        {
                            RequestId = message.RequestId,
                            Success = true,
                            Data = JsonSerializer.Serialize(newUser)
                        };

                    case "LOGIN_USER":
                        var userData = JsonSerializer.Deserialize<LoginModel>(message.Data);
                        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userData.Email && u.Password == userData.Password);
                        if (user != null)
                        {
                            return new SocketResponse
                            {
                                RequestId = message.RequestId,
                                Success = true,
                                Data = JsonSerializer.Serialize(user)
                            };
                        }
                        return new SocketResponse
                        {
                            RequestId = message.RequestId,
                            Success = false,
                            Data = ""
                        };
                    default:
                        return new SocketResponse
                        {
                            Success = false,
                            Error = "Unknown command",
                            RequestId = message.RequestId
                        };
                }
            }
            catch(Exception ex)
            {
                return new SocketResponse
                {
                    RequestId = message.RequestId,
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public void RemoveClient(ClientHandler clientHandler)
        {
            _clients.Remove(clientHandler);
        }
    }
}
