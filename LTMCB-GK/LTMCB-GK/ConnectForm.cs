using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTMCB_GK
{
    public partial class ConnectForm : Form
    {
        public ConnectForm()
        {
            InitializeComponent();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            string ip = txt_IP.Text;
            string port = txt_Port.Text;
            int p;
            bool bSuccess = Int32.TryParse(port, out p);
            if (!bSuccess)
            {
                MessageBox.Show("Invalid Port!");
                return;
            }
            TcpClientModel tcp = new TcpClientModel(ip, p);
            //int iSuccess = -1;
            int iSuccess = tcp.connectToServer();
            if (iSuccess == -1)
            {
                MessageBox.Show("Cannot connect to server!");
                return;
            }
            else
            {
                NavigationForm frm = new NavigationForm(tcp);
                this.Hide();
                frm.ShowDialog();
                tcp.sendData("Disconnected");
                tcp.closeConnection();
                this.Show();
            }
        }
    }
}
