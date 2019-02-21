using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MultiChatClient
{
    class StateObject
    {
        public int bufferSize;
        public Socket socket;
        public byte[] dataBuffer;
        public bool firstTime;
        public string userName;
        public string id;
        public TcpClient client;
        public StringBuilder sb = new StringBuilder();

        public StateObject()
        {
            socket = null;
            bufferSize = 1024;
            dataBuffer = new byte[bufferSize];
            sb = new StringBuilder();
        }
    }
}
