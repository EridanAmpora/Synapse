using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using SetaGames.Utility.Logging;
using System.Threading;

using SetaGames.Synapse.Worker;
using SetaGames.Synapse.Networking.Pipeline;

namespace SetaGames.Synapse.Networking {

    /// <summary>
    /// The server bootstrap for the Synapse library,
    /// which is used to initialize a connection listener
    /// </summary>
    public class ServerBootstrap {

        /// <summary>
        /// The base socket instance
        /// </summary>
        private Socket Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly Logger Logger = LoggerFactory.GetLogger(typeof(ServerBootstrap));

        /// <summary>
        /// The maximum amount of concurrent connections to accept
        /// </summary>
        private int maxConnections = 1024;

        /// <summary>
        /// The pipeline factory to use
        /// </summary>
        private readonly ChannelPipelineFactory pipelineFactory;

        /// <summary>
        /// Creates the bootstrap instance
        /// </summary>
        /// 
        /// <param name="maxConnections">
        /// The maximum amount of concurrent connections to handle
        /// </param>
        public ServerBootstrap(ChannelPipelineFactory pipelineFactory, int maxConnections = 1024) {
            
            //Ensure the pipeline is not null
            if (pipelineFactory == null) {
                Logger.Severe("Invalid pipeline factory specified!");
                Environment.Exit(0);
            }

            //Ensure a channel handler is set
            if (pipelineFactory.getPipeline().getHandler() == null) {
                Logger.Severe("Invalid channel handler specified!");
                Environment.Exit(0);
            }

            //Ensure an encoder is set
            if (pipelineFactory.getPipeline().getEncoder() == null) {
                Logger.Severe("Invalid encoder specified!");
                Environment.Exit(0);
            }

            //Ensure a decoder is set
            if (pipelineFactory.getPipeline().getDecoder() == null) {
                Logger.Severe("Invalid decoder specified!");
                Environment.Exit(0);
            }

            this.pipelineFactory = pipelineFactory;
            this.maxConnections = maxConnections;
        }

        /// <summary>
        /// Binds the socket to a port, and listens for
        /// incoming connections
        /// </summary>
        /// 
        /// <param name="port">
        ///  The port to bind to
        /// </param>
        public void bind(int port) {

            //The end point 
            IPEndPoint IPEndPoint = new IPEndPoint(IPAddress.Any, port);

            //Bind to the port
            Socket.Bind(IPEndPoint);

            //Commence listening
            Socket.Listen(port);

            //Create the worker
            BootstrapWorker worker = new BootstrapWorker(this.pipelineFactory, Socket);
            
            //Print to the console
            Logger.Info("Listening on " + IPEndPoint);
        }
    }
}
