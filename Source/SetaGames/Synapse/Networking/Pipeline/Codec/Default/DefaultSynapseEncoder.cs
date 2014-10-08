using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SetaGames.Synapse.Networking.Pipeline.Channel;

namespace SetaGames.Synapse.Networking.Pipeline.Codec.Default {

    /// <summary>
    /// Represents the default encoder, which is used if none
    /// are specified in the factory
    /// </summary>
    public class DefaultSynapseEncoder : SynapseEncoder {

        public Object encode(ConnectionChannel channel, Object packet) {
            return null;
        }
    }
}
