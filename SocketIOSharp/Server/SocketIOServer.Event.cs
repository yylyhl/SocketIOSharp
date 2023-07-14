using EmitterSharp;
using SocketIOSharp.Common;
using SocketIOSharp.Server.Client;
using System;

namespace SocketIOSharp.Server
{
    partial class SocketIOServer
    {
        private class SocketIOConnectionHandler : Emitter<SocketIOConnectionHandler, string, object> { }
        private readonly SocketIOConnectionHandler ConnectionHandlerManager = new SocketIOConnectionHandler();
        public SocketIOServer OnConnection(Action<SocketIOSocket> Callback)
        {
            ConnectionHandlerManager.On(SocketIOEvent.CONNECTION, (Client) => Callback(Client as SocketIOSocket));
            return this;
        }

        public SocketIOServer OnConnecting(Action<Tuple<System.Collections.Specialized.NameValueCollection, string>> Callback)
        {
            ConnectionHandlerManager.On(SocketIOEvent.CONNECTING, (Client) => Callback(Client as Tuple<System.Collections.Specialized.NameValueCollection, string>));
            return this;
        }
    }
}
