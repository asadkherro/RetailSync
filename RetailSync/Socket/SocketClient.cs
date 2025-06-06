using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Text;
using RetailSync_Models.Socket;
using RetailSync_Models.DbModels;

namespace RetailSync.Socket
{
    public class SocketClient
    {
        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private int _requestCounter = 0 ;
        private readonly Dictionary<int, TaskCompletionSource<SocketResponse>> _pendingRequests =
        new Dictionary<int, TaskCompletionSource<SocketResponse>>();


        public async Task<bool> ConnectAsync(string host, int port)
        {
            try
            {
                _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(host, port);
                _stream = _tcpClient.GetStream();

                // Start listening for responses
                _ = Task.Run(ListenForResponsesAsync);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return false;
            }
        }

        private async Task ListenForResponsesAsync()
        {
            try
            {
                byte[] buffer = new byte[4096];

                while (_tcpClient?.Connected == true)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    var response = JsonSerializer.Deserialize<SocketResponse>(responseJson);

                    if (_pendingRequests.TryGetValue(response.RequestId, out var tcs))
                    {
                        _pendingRequests.Remove(response.RequestId);
                        tcs.SetResult(response);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Listen error: {ex.Message}");
            }
        }

        public async Task<SocketResponse> SendCommandAsync(string command, string data = null)
        {
            if (_stream == null || !_tcpClient.Connected)
                throw new InvalidOperationException("Not connected to server");

            int requestId = ++_requestCounter;
            var message = new SocketMessage
            {
                Command = command,
                Data = data,
                RequestId = requestId
            };

            var tcs = new TaskCompletionSource<SocketResponse>();
            _pendingRequests[requestId] = tcs;

            try
            {
                string messageJson = JsonSerializer.Serialize(message);
                byte[] messageBytes = Encoding.UTF8.GetBytes(messageJson);

                await _stream.WriteAsync(messageBytes, 0, messageBytes.Length);

                // Wait for response with timeout
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(360));
                var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    _pendingRequests.Remove(requestId);
                    throw new TimeoutException("Request timed out");
                }

                return await tcs.Task;
            }
            catch
            {
                _pendingRequests.Remove(requestId);
                throw;
            }
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            var response = await SendCommandAsync("GET_USERS");
            if (response.Success)
            {
                return JsonSerializer.Deserialize<List<UserModel>>(response.Data);
            }
            throw new Exception(response.Error);
        }

        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            var userData = JsonSerializer.Serialize(user);
            var response = await SendCommandAsync("ADD_USER", userData);
            if (response.Success)
            {
                return JsonSerializer.Deserialize<UserModel>(response.Data);
            }
            throw new Exception(response.Error);
        }

        public async Task<UserModel> LoginUserAsync(string email, string password)
        {
            var loginData = JsonSerializer.Serialize(new LoginModel { Email = email, Password = password} );
            var response = await SendCommandAsync("LOGIN_USER", loginData);
            if (response.Success)
            {
                return JsonSerializer.Deserialize<UserModel>(response.Data);
            }
            throw new Exception(response.Error);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var response = await SendCommandAsync("DELETE_USER", userId.ToString());
            if (!response.Success)
            {
                throw new Exception(response.Error);
            }
        }

        public void Dispose()
        {
            _stream?.Close();
            _tcpClient?.Close();
        }


    }
}
