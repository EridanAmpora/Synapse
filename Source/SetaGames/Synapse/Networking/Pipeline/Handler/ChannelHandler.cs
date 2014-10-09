using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SetaGames.Synapse.Networking.Pipeline.Channel;

namespace SetaGames.Synapse.Networking.Pipeline.Handler {

    /// <summary>
    /// Represents a channel handler instance
    /// </summary>
    public abstract class ChannelHandler {

        /// <summary>
        /// Called when a new connection is made to the handler
        /// </summary>
        /// 
        /// <param name="channel">
        /// The channel instance
        /// </param>
        public virtual void newConnection(ConnectionChannel channel) {
        }

        /// <summary>
        /// Called when a message is received from a channel's decoder
        /// </summary>
        /// 
        /// <param name="channel">
        /// The channel instance
        /// </param>
        /// 
        /// <param name="message">
        /// The message received
        /// </param>
        public virtual void messageReceived(ConnectionChannel channel, Object message) {
        }

        /// <summary>
        /// Called when a connection is terminated or declared disconnected
        /// </summary>
        /// 
        /// <param name="channel">
        /// The channel instance
        /// </param>
        public virtual void connectionTerminated(ConnectionChannel channel) {
        }

        /// <summary>
        /// Called when an exception is thrown while handling a channel
        /// </summary>
        /// 
        /// <param name="channel">
        /// The channel
        /// </param>
        /// 
        /// <param name="e">
        /// The exception
        /// </param>
        public virtual void exceptionCaught(ConnectionChannel channel, Exception e) {
        }
    }
}
