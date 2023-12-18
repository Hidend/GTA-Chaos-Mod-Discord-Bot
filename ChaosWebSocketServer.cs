using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GTA_SA_Chaos_Mod_Discord.Models;

namespace GTA_SA_Chaos_Mod_Discord
{
    class ChaosSession : WsSession
    {
        public ChaosSession(WsServer server) : base(server) { }


        public override void OnWsConnected(HttpResponse response)
        {
            Console.WriteLine($"Chaos WebSocket client connected a new session with Id {Id}");
        }

        public override void OnWsDisconnected()
        {
            Console.WriteLine($"Chaos WebSocket session with Id {Id} disconnected!");
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chaos WebSocket session caught an error with code {error}");
        }
        protected override void OnConnected()
        {
            Console.WriteLine($"Chat TCP session with Id {Id} connected!");
            Console.WriteLine($"Current Sessions Count {Server.ConnectedSessions}");
        }

        protected override void OnDisconnected()
        {

            Console.WriteLine($"Chat TCP session with Id {Id} and disconnected!");
            Console.WriteLine($"Current Sessions Count {Server.ConnectedSessions}");
        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            Console.WriteLine("Incoming: " + message);
        }

    }

    class ChaosWebSocketServer : WsServer
    {
        public ChaosWebSocketServer(IPAddress address, int port) : base(address, port) { }

        protected override TcpSession CreateSession() { return new ChaosSession(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Chaos WebSocket server caught an error with code {error}");
        }

    }
}
