namespace RetailSync_Models.Socket
{
    public class SocketResponse
    {
        public bool Success { get; set; }
        public string Data { get; set; }
        public string Error { get; set; }
        public int RequestId { get; set; }
    }
}
