//
// DisconnectionReason.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Networking;

public enum DisconnectionReason
{
    /// <summary>
    ///     when a client times out (when he is not responding).
    /// </summary>
    TimedOut,

    /// <summary>
    ///     when a client logs off manually (by himself).
    /// </summary>
    UserLogOff,

    /// <summary>
    ///     when a client has unmatching plugins with the server/other clients.
    /// </summary>
    PluginMismatch,

    /// <summary>
    ///     when a client is kicked.
    /// </summary>
    KickedByServer
}