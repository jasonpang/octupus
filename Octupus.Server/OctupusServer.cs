using Octupus.Common.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tempest;
using Tempest.Providers.Network;

namespace Octupus.Server
{
    public class OctupusServer : TempestServer
    {
        public OctupusServer(IConnectionProvider provider)
            : base(provider, MessageTypes.Reliable) { }

        private readonly List<IConnection> connections = new List<IConnection>();

        protected override void OnConnectionMade(object sender, ConnectionMadeEventArgs e)
        {
            Debug.WriteLine(String.Format("Octupus: Connected to {0}:{1}", e.Connection.RemoteTarget.Hostname, e.Connection.RemoteTarget.Port));
            lock (this.connections)
                this.connections.Add(e.Connection);

            base.OnConnectionMade(sender, e);
        }

        protected override void OnConnectionDisconnected(object sender, DisconnectedEventArgs e)
        {
            Debug.WriteLine(String.Format("Octupus: Disconnected from {0}:{1}", e.Connection.RemoteTarget.Hostname, e.Connection.RemoteTarget.Port));
            lock (this.connections)
                this.connections.Remove(e.Connection);

            base.OnConnectionDisconnected(sender, e);
        }

        public void BroadcastMessage(Message message)
        {
            lock (this.connections)
                foreach (IConnection connection in this.connections)
                    connection.SendAsync(message);
        }

        public static OctupusServer StartNew()
        {
            // NetworkConnectionProvider requires that you tell it what local target to listen
            // to and the maximum number of connections you'll allow.
            var provider = new NetworkConnectionProvider(OctupusProtocol.Instance, new Target(Target.AnyIP, 58291), 10);

            var server = new OctupusServer(provider);
            server.Start();
            return server;
        }
    }
}
