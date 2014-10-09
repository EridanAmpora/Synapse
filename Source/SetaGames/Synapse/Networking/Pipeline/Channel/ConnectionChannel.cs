using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

using SetaGames.Synapse.Networking.Pipeline;

namespace SetaGames.Synapse.Networking.Pipeline.Channel {

    /// <summary>
    /// Represents a channel, connected to the bootstrap
    /// </summary>
    public class ConnectionChannel {
        
        /// <summary>
        /// The socket associated with this channel
        /// </summary>
        private Socket socket;

        /// <summary>
        /// The pipeline associated with this channel
        /// </summary>
        private Pipeline pipeline;

        /// <summary>
        /// The network stream, for the socket
        /// </summary>
        private NetworkStream stream;

        /// <summary>
        /// Creates the channel instance
        /// </summary>
        /// 
        /// <param name="socket">
        /// The socket associated with the channel
        /// </param>
        /// 
        /// <param name="pipeline">
        /// The pipeline instance for the channel
        /// </param>
        public ConnectionChannel(Socket socket, Pipeline pipeline) {
            this.socket = socket;
            this.pipeline = pipeline;
            this.stream = new NetworkStream(socket);
        }

        /// <summary>
        /// Writes a packet to the channel
        /// </summary>
        /// 
        /// <param name="message">
        /// The message to write
        /// </param>
        public void write(Object message) {

            //Ensure the message is not null
            if (message == null) {
                return;
            }

            //Encode the message
            Object packet = pipeline.getEncoder().encode(this, message);

            //Convert the packet to a payload
            byte[] payload = (byte[]) packet;

            //Write the bytes
            stream.Write(payload, 0, payload.Length);

        }

        /// <summary>
        /// Disconnects the channel session
        /// </summary>
        public void disconnect() {
            socket.Disconnect(false);
        }

        /// <summary>
        /// Gets the socket associated with this channel
        /// </summary>
        /// 
        /// <returns>
        /// The channel socket
        /// </returns>
        public Socket getSocket() {
            return socket;
        }

        /// <summary>
        /// Gets the pipeline instance associated with this channel
        /// </summary>
        /// 
        /// <returns>
        /// The pipeline instance
        /// </returns>
        public Pipeline getPipeline() {
            return pipeline;
        }
    }
}
