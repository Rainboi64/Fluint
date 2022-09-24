//
// MessageReceivedEventArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Networking.Client.EventArgs
{
    public class MessageReceivedEventArgs : System.EventArgs
    {
        public MessageReceivedEventArgs(ClientData sender, string messageData)
        {
            Sender = sender;
            MessageData = messageData;
        }

        /// <summary>
        /// The Client who sent the message.
        /// </summary>
        public ClientData Sender
        {
            get;
        }

        /// <summary>
        /// The message text (string).
        /// </summary>
        public string MessageData
        {
            get;
        }
    }
}