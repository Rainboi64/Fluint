namespace Fluint.Layer.Networking
{
    /// <summary>
    /// A data structure to be transported
    /// </summary>
    /// 

    // TODO: what the fuck?
    public class NetworkPacket
    {
        const string ConnectTask = "CLIENT_CONNECT_REQUEST";
        const string DisconnectTask = "CLIENT_DISCONNECT_REQUEST";
        const string ChatTask = "CLIENT_SENT_MESSAGE";
        const string TickTask = "SERVER_TICK";

        public string Task { get; }
        public object[] Arguments { get; }
    }
}