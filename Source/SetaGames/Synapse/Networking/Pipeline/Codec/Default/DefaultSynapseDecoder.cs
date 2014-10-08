using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SetaGames.Synapse.Networking.Pipeline.Channel;
using Packet = SetaGames.Synapse.Networking.Packet;

namespace SetaGames.Synapse.Networking.Pipeline.Codec.Default {

    /// <summary>
    /// Represents the default decoder, which is used if none
    /// are specified in the factory
    /// </summary>
    public class DefaultSynapseDecoder : SynapseDecoder {

        public Object decode(ConnectionChannel channel, Packet.Buffer buffer) {
            Console.WriteLine("Decoding...");
            return null;
        }
    }
}
