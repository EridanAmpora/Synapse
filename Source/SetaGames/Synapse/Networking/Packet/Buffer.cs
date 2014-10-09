using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetaGames.Synapse.Networking.Packet {

    /// <summary>
    /// Represents a buffer of bytes, received from
    /// a socket connection
    /// </summary>
    public class Buffer {

        /// <summary>
        /// The initial payload of the buffer
        /// </summary>
        private byte[] payload;

        /// <summary>
        /// The current reader offset
        /// </summary>
        private int currentOffset = 0;

        /// <summary>
        /// Creates an uninitialized buffer instance
        /// </summary>
        public Buffer() {
            this.payload = new byte[4096];
        }

        /// <summary>
        /// Creates a buffer instance from a sequence
        /// of received bytes
        /// </summary>
        /// 
        /// <param name="payload">
        /// The bytes to wrap
        /// </param>
        public Buffer(byte[] payload) {
            this.payload = payload;
        }

        /// <summary>
        /// Gets the payload of the buffer
        /// </summary>
        /// 
        /// <returns>
        /// The entire payload
        /// </returns>
        public byte[] getPayload() {
            return payload;
        }

        /// <summary>
        /// Gets the amount of bytes left available
        /// to read
        /// </summary>
        /// 
        /// <returns>
        /// The amount of bytes left that are readable
        /// </returns>
        public int remainingBytes() {
            if (payload.Length - currentOffset < 0) {
                return 0;
            }
            return payload.Length - currentOffset;
        }

        /// <summary>
        /// Reads a single byte from the buffer payload
        /// </summary>
        /// 
        /// <returns>
        /// A single byte
        /// </returns>
        public byte readByte() {
            return payload[currentOffset++];
        }

        /// <summary>
        /// Reads a single short from the buffer payload
        /// </summary>
        /// 
        /// <returns>
        /// A single short
        /// </returns>
        public int readShort() {
            currentOffset += 2;
            int value = ((payload[currentOffset - 2] & 0xFF) << 8) + (payload[currentOffset - 1] & 0xFF);
            if (value > 32767) {
                value -= 0x10000;
            }
            return value;
        }

        /// <summary>
        /// Reads a single integer from the buffer payload
        /// </summary>
        /// 
        /// <returns>
        /// A single integer
        /// </returns>
        public int readInt() {
            currentOffset += 4;
            return ((payload[currentOffset - 4] & 0xFF) << 24) + ((payload[currentOffset - 3] & 0xFF) << 16) + ((payload[currentOffset - 2] & 0xFF) << 8) + (payload[currentOffset - 1] & 0xFF);
        }

        /// <summary>
        /// Reads a single long from the buffer payload
        /// </summary>
        /// 
        /// <returns>
        /// A single long
        /// </returns>
        public long readLong() {
            long first = (long)readInt() & 0xFFFFFFFFL;
            long second = (long)readInt() & 0xFFFFFFFFL;
            return (first << 32) + second;
        }

        /// <summary>
        /// Reads a three-byte integer from the buffer payload
        /// </summary>
        /// 
        /// <returns>
        /// A tri-byte integer
        /// </returns>
        public int readTriByte() {
            return ((readByte() & 0xFF) << 16) + ((readByte() & 0xFF) << 8) + (readByte() & 0xFF);
        }
    }
}
