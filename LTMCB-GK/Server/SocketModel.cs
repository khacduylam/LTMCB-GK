using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    class SocketModel
    {
        Socket socket;

        public SocketModel(Socket s)
        {
            socket = s;
        }

        public int sendData(string str)
        {
            try
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] byteSend = asen.GetBytes(str);
                socket.Send(byteSend);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sending Error.." + e.StackTrace);
                return -1;
            }
            return 1;
        }

        public string receiveData()
        {
            string str;
            try
            {
                byte[] tmp = new byte[100];
                socket.Receive(tmp);
                str = System.Text.Encoding.ASCII.GetString(tmp);
            }
            catch
            {
                return null;
            }
            return str;
        }

        override public string ToString()
        {
            string str = null;
            try
            {
                str = Convert.ToString(socket.RemoteEndPoint);
            }
            catch
            {
                return null;
            }
            return str;
        }   

        public void Close()
        {
            socket.Close();
        }
    }
}
