using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using SetaGames.Utility.Logging;
using SetaGames.Synapse.Networking.Pipeline;
using SetaGames.Synapse.Networking.Pipeline.Channel;
using Packet = SetaGames.Synapse.Networking.Packet.Buffer;

namespace SetaGames.Synapse.Worker {

    /// <summary>
    /// The worker thread, used to handle execution of a
    /// server bootstrap instance
    /// </summary>
    class BootstrapWorker {

        /// <summary>
        /// The thread
        /// </summary>
        private Thread thread;

        /// <summary>
        /// If the worker thread is still running
        /// </summary>
        private bool running = false;

        /// <summary>
        /// The socket to use for connections
        /// </summary>
        private Socket socket;

        /// <summary>
        /// The pipeline factory to use for channels
        /// </summary>
        private readonly ChannelPipelineFactory pipelineFactory;

        /// <summary>
        /// The logger instance
        /// </summary>
        private Logger Logger = LoggerFactory.GetLogger(typeof(BootstrapWorker));

        /// <summary>
        /// A list of channels connected to the socket
        /// </summary>
        private List<ConnectionChannel> connectedChannels = new List<ConnectionChannel>();

        /// <summary>
        /// Creates a new worker instance
        /// </summary>
        /// 
        /// <param name="pipelineFactory">
        /// The pipeline factory to use
        /// </param>
        /// 
        /// <param name="socket">
        /// The socket to use
        /// </param>
        public BootstrapWorker(ChannelPipelineFactory pipelineFactory, Socket socket) {

            //Set the socket
            this.socket = socket;

            //Set the pipeline factory
            this.pipelineFactory = pipelineFactory;

            //Create the thread
            this.thread = new Thread(new ThreadStart(pulse));
            this.thread.Name = "SynapseWorker";
            this.thread.Priority = ThreadPriority.AboveNormal;

            //Start the thread
            this.running = true;
            this.thread.Start();
        }

        /// <summary>
        /// Handles the pulse of the worker thread. This is executes
        /// every 50ms
        /// </summary>
        public void pulse() {

            //Log the worker creation
            Logger.Info(String.Format("[Name={0}, Priority={1}] has started.", thread.Name, thread.Priority));

            try {

                while (isRunning()) {

                    //The channels to remove
                    List<ConnectionChannel> toRemove = new List<ConnectionChannel>();

                    //Loop and ensure the channels are still connected
                    connectedChannels.ForEach(channel => {

                            if (channel.getSocket().Poll(1000, SelectMode.SelectRead)) {
                               channel.getPipeline().getHandler().connectionTerminated(channel);
                               toRemove.Add(channel);
                            }
                        
                    });

                    //Remove the disconnected channels
                    connectedChannels = connectedChannels.Except(toRemove).ToList();

                    //Accept connections
                    execute();

                    //Sleep for 50ms
                    Thread.Sleep(50);

                }
            } catch (Exception e) {
                Logger.Log(Level.SEVERE, "Error in bootstrap worker", e);
            }

        }

        /// <summary>
        /// The execution method, called at the first pulse
        /// </summary>
        private void execute() {
            this.socket.BeginAccept(new AsyncCallback(connectCallback), socket);
        }

        private void connectCallback(IAsyncResult ar) {

            //The listener socket
            Socket listener = (Socket) ar.AsyncState;

            //The connected socket
            Socket socket = listener.EndAccept(ar);

            //Set the socket options
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);

            //The channel instance
            ConnectionChannel channel = new ConnectionChannel(socket, pipelineFactory.getPipeline());

            //Add the channel
            connectedChannels.Add(channel);

            //An empty packet instance
            Packet packet = new Packet();

            //Begin receiving to the packet payload
            channel.getSocket().BeginReceive(packet.getPayload(), 0, packet.getPayload().Length, 0, new AsyncCallback(readCallback), new object[] {channel, packet});

            // Start accepting another connection
            listener.BeginAccept(new AsyncCallback(connectCallback), listener);
        }

        private void readCallback(IAsyncResult ar) {

                //The callback messages
                object[] messages = (object[])ar.AsyncState;

                //The channel
                ConnectionChannel channel = (ConnectionChannel)messages[0];

                //The packet
                Packet packet = (Packet) messages[1];

            try {

                //If we are unable to poll the channel's socket, it must be terminated
                if (channel.getSocket().Poll(1000, SelectMode.SelectRead)) {
                    channel.getPipeline().getHandler().connectionTerminated(channel);
                    connectedChannels.Remove(channel);
                    return;
                }

                //Send the packet to the channel's decoder
                channel.getPipeline().getHandler().messageReceived(channel, channel.getPipeline().getDecoder().decode(channel, packet));

                //Await another packet
                channel.getSocket().BeginReceive(packet.getPayload(), 0, packet.getPayload().Length, 0, new AsyncCallback(readCallback), new object[] { channel, packet });
            
            } catch (Exception e) {

                //Send the exception to the channel's handler
                channel.getPipeline().getHandler().exceptionCaught(channel, e);
            }
        }

        /// <summary>
        /// Checks if the worker is currently running
        /// </summary>
        /// 
        /// <returns>
        /// True if the thread is running, else false
        /// </returns>
        private bool isRunning() {
            return running;
        }
    }
}
