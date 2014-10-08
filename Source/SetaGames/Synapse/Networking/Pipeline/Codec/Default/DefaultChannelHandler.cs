using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SetaGames.Synapse.Networking.Pipeline.Handler;
using SetaGames.Synapse.Networking.Pipeline.Channel;

namespace SetaGames.Synapse.Networking.Pipeline.Codec.Default {

    /// <summary>
    /// Represents the default channel handler, which is used
    /// if none is specified in the pipeline factory
    /// </summary>
    public class DefaultChannelHandler : ChannelHandler {

        public override void newConnection(ConnectionChannel channel) {
            Console.WriteLine("New connection!");
        }

        public override void messageReceived(ConnectionChannel channel, Object message) {
            Console.WriteLine("Message received!");
        }

        public override void connectionTerminated(ConnectionChannel channel) {
            Console.WriteLine("Connection terminated!");
        }

    }
}
