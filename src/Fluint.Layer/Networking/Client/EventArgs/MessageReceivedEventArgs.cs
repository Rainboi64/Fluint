//
// MessageReceivedEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Networking.Client
{
    public class MessageReceivedEventArgs : System.EventArgs
    {
        public MessageReceivedEventArgs(IClientData sender, string messageData)
        {
            Sender = sender;
            MessageData = messageData;
        }

        /// <summary>
        /// The Client who sent the message.
        /// </summary>
        public IClientData Sender { get; }

        /// <summary>
        /// The message text (string).
        /// </summary>
        public string MessageData { get; }
    }
}
