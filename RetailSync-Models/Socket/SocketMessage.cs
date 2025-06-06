namespace RetailSync_Models.Socket
{
    public class SocketMessage
    {
        public string Command { get; set; }
        public string Data { get; set; }
        public int RequestId { get; set; }
    }
}
