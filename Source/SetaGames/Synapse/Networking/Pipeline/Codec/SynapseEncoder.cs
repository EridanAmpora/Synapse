using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SetaGames.Synapse.Networking.Pipeline.Channel;

namespace SetaGames.Synapse.Networking.Pipeline.Codec {

    /// <summary>
    /// Represents a class used to encode a message sent
    /// to a channel
    /// </summary>
    public interface SynapseEncoder {

        /// <summary>
        /// Handles the encoding of a packet to a channel
        /// </summary>
        /// 
        /// <param name="channel">
        /// The channel
        /// </param>
        /// 
        /// <param name="packet">
        /// The packet object to send
        /// </param>
        Object encode(ConnectionChannel channel, Object packet);
    }
}
