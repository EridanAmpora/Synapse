using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SetaGames.Synapse.Networking.Pipeline.Channel;
using Packet = SetaGames.Synapse.Networking.Packet;

namespace SetaGames.Synapse.Networking.Pipeline.Codec {

    /// <summary>
    /// Represents a class used to decode a packet received
    /// from a socket connections
    /// </summary>
    public interface SynapseDecoder {

        /// <summary>
        /// Decodes a packet received from a connection
        /// </summary>
        /// 
        /// <param name="channel">
        /// The channel the associated with the packet
        /// </param>
        /// 
        /// <param name="buffer">
        /// The packet buffer
        /// </param>
        Object decode(ConnectionChannel channel, Packet.Buffer buffer);
    }
}
