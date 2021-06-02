namespace Fluint.Layer.Networking.Server
{
    /// <summary>
    /// an interface for storing the server's data. 
    /// </summary>
    [Initialization(InitializationMethod.Scoped)]
    public interface IServerData : IModule
    {
        /// <summary>
        /// The IP Address of the server.
        /// </summary>
        string IpAddress { get; }

        /// <summary>
        /// The Port Number for clients to connect through.
        /// </summary>
        int Port { get; }
        
        /// <summary>
        /// The tick delay of the server state, dedicates the tick rate.
        /// </summary>
        /// <value></value>
        int TickDelay { get; }

        /// <summary>
        /// The name/tag of the server. (Can be changed at runtime)
        /// </summary>
        string Name { get; set; }
    }
}