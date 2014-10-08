using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SetaGames.Synapse.Networking.Pipeline.Codec;
using SetaGames.Synapse.Networking.Pipeline.Handler;
using SetaGames.Synapse.Networking.Pipeline.Codec.Default;

namespace SetaGames.Synapse.Networking.Pipeline {

    /// <summary>
    /// Represents a factory used to configure the pipeline, for
    /// decoding, encoding and handling of connections
    /// </summary>
    public abstract class ChannelPipelineFactory {

        /// <summary>
        /// Gets the default pipeline to assign to new
        /// connection channels
        /// </summary>
        /// 
        /// <returns>
        /// The default pipeline
        /// </returns>
        public virtual Pipeline getPipeline() {
            return new Pipeline();
        }
    }
}
