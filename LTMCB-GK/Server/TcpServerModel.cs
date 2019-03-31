using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Server
{
    class TcpServerModel
    {
        private IPAddress IP;
        private int port;
        private Socket[] Sockets;
        int top;
        private TcpListener listener;
        int MAX_BUFFER = 100;

        public TcpServerModel(string ip, int p)
        {
            IP = IPAddress.Parse(ip);
            port = p;
            listener = new TcpListener(IP, port);
            Sockets = new Socket[MAX_BUFFER];
            top = 0;
            for (int i = 0; i < MAX_BUFFER; i++)
            {
                Sockets[i] = null;
            }
        }

        public int Start()
        {
            try
            {
                listener.Start();
            }
            catch
            {
                return -1;
            }
            return 1;
        }

        public void Stop()
        {
            listener.Stop();
        }

        public Socket Accept()
        {
            return null;
            return listener.AcceptSocket();
        }
    }
}
