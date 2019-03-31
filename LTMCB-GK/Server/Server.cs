using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public partial class Server : Form
    {
        TcpServerModel tcp;
        SocketModel[] sockets;
        const int MAX_SOCKET = 10;
        int top;
        Database db;
        public Server()
        {
            InitializeComponent();
            sockets = new SocketModel[MAX_SOCKET];
            top = 0;
            for (int i = 0; i < MAX_SOCKET; i++)
            {
                sockets[i] = null;
            }
            db = new Database("D:\\1_UNIVERSITY\\" +
                "4_Lập trình Mạng\\" +
                "Giữa kì\\db.db");
        }


        private void btn_Start_Click(object sender, EventArgs e)
        {
            string ip = txt_IP.Text;
            string port = txt_Port.Text;
            int p;
            bool iSuccess = Int32.TryParse(port, out p);
            tcp = new TcpServerModel(ip, p);
            tcp.Start();
            Thread t = new Thread(WaitForClient);
            t.Start();
        }

        void WaitForClient()
        {
            while (true)
            {
                Socket s = tcp.Accept();
                SocketModel sm = new 
                    SocketModel(s);
                int index = getFreeSocketIndex();
                if (index == -1)
                {
                    rtb_Client.Text += 
                        "Cannot reach to " + 
                        sm.ToString() + "\n";
                    continue;
                }
                rtb_Client.Text +=
                        sm.ToString() + "\n";
                sockets[index] = new SocketModel(s);
                Thread t = new Thread(ServeClient);
                t.Start(index);
            }
        }

        int SignUpEvent(int index, string parameters)
        {
            SocketModel soc = sockets[index];
            string[] parameter = parameters.Split(';');

            if (parameter.Length != 2)
            {
                soc.sendData("Failure:");
            }

            Record tRec = new Record(parameter[0],
                parameter[1], 0);
            int iSuccess = db.addRecord(tRec);
            if (iSuccess == -1)
            {

            }
            return 0;
        }

        void ServeClient(Object obj)
        {
            int index = (Int32)obj;
            SocketModel soc = sockets[index];
            while (true)
            {
                
                string data = soc.receiveData();
                if (data == null)
                    break;
                rtb_Procedure.Text += "From " + ":" +
                    data + "\n";

                string[] req = data.Split(':');
                if (req.Length != 2)
                {
                    int i = soc.sendData("Failure:Something");
                    if (i == -1)
                        break;
                    continue;
                }

                switch (req[0])
                {
                    case "Signup":
                        SignUpEvent(index, req[1]);
                        break;
                }

                soc.sendData("Success:Something");
            }
            rtb_Client.Text += "Disconnect from "
                + ":" +
                    soc.ToString() + "\n";
        }

        int getFreeSocketIndex()
        {
            int limit_times = 100;
            while (true)
            {
                if (sockets[top] == null)
                    return top;
                top = (top + 1) % MAX_SOCKET;
                limit_times--;
                if (limit_times < 0)
                    break;
            }
            return -1;
        }

        void CloseSocket(int index)
        {
            sockets[index].Close();
            sockets[index] = null;
        }

        private void Server_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }
    }
}
