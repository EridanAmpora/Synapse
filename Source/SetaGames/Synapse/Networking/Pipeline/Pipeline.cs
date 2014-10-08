using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SetaGames.Synapse.Networking.Pipeline.Codec;
using SetaGames.Synapse.Networking.Pipeline.Handler;

namespace SetaGames.Synapse.Networking.Pipeline {

    /// <summary>
    /// Represents the pipeline associated with a channel
    /// </summary>
    public class Pipeline {

        /// <summary>
        /// The decoder to use for the channel's pipeline
        /// </summary>
        private SynapseDecoder decoder;

        /// <summary>
        /// The encoder to use for the channel's pipeline
        /// </summary>
        private SynapseEncoder encoder;

        /// <summary>
        /// The handler used for channel operations
        /// </summary>
        private ChannelHandler handler;

        /// <summary>
        /// Sets the decoder for a pipeline instance
        /// </summary>
        /// 
        /// <param name="decoder">
        /// The decoder to set
        /// </param>
        public void setDecoder(SynapseDecoder decoder) {
            this.decoder = decoder;
        }

        /// <summary>
        /// Sets the encoder for a pipeline instance
        /// </summary>
        /// 
        /// <param name="encoder">
        /// The encoder to set
        /// </param>
        public void setEncoder(SynapseEncoder encoder) {
            this.encoder = encoder;
        }

        /// <summary>
        /// Sets the handler associated with this pipeline
        /// </summary>
        /// 
        /// <param name="handler">
        /// The handler instance
        /// </param>
        public void setHandler(ChannelHandler handler) {
            this.handler = handler;
        }

        /// <summary>
        /// The decoder to use for this pipeline
        /// </summary>
        /// 
        /// <returns>
        /// The decoder
        /// </returns>
        public SynapseDecoder getDecoder() {
            return decoder;
        }

        /// <summary>
        /// The encoder used for this pipeline
        /// </summary>
        /// 
        /// <returns>
        /// The encoder
        /// </returns>
        public SynapseEncoder getEncoder() {
            return encoder;
        }

        /// <summary>
        /// The channel handler instance used to handle
        /// channel operations
        /// </summary>
        /// 
        /// <returns>
        /// The pipeline channel handler instance
        /// </returns>
        public ChannelHandler getHandler() {
            return handler;
        }
    }
}
