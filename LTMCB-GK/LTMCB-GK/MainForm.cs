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
    public partial class MainForm : Form
    {
        private Info i;
        private TcpClientModel tcp;
        public MainForm(TcpClientModel tcp, Info i)
        {
            this.tcp = tcp;
            this.i = i;
            InitializeComponent();
            lbl_Title.Text = i.username;
            txt_Money.Text = i.money;
            txt_MpR.Text = i.MpR;
        }


        private void btn_Iteration_Click(object sender, EventArgs e)
        {
            request("Iteration");
        }

        private void btn_Recursion_Click(object sender, EventArgs e)
        {
            request("Recursion");
        }

        void request(string req)
        {
            string n = txt_Input.Text;
            int temp;
            bool bSuccess = Int32.TryParse(n, out temp);
            if (!bSuccess)
            {
                MessageBox.Show("Invalid data!");
                return;
            }
            string data = "Request:" + req +";" + n;
            int iSuccess = tcp.sendData(data);
            rtb_Status.Text += "Sending: " + data + "\r\n";
            if (iSuccess == -1)
            {
                MessageBox.Show("Connecting Error..");
                return;
            }
            iSuccess = tcp.receiveData(ref data);
            rtb_Status.Text += "Received: " + data + "\r\n";
            if (iSuccess == -1)
            {
                MessageBox.Show("Connecting Error..");
                return;
            }
            string[] dataformat = data.Split(new char[] { ':', ';' });
            if (dataformat.Length != 6
                || dataformat[0] != "Result")
            {
                MessageBox.Show("Data Error...");
                return;
            }

            txt_Result.Text = dataformat[1];
            txt_Time.Text = dataformat[2];
            lbl_Title.Text = dataformat[3];
            txt_Money.Text = dataformat[4];
            txt_MpR.Text = dataformat[5];
        }

        private void btn_Recharge_Click(object sender, EventArgs e)
        {
            RechargeForm frm = new RechargeForm(tcp);
            frm.ShowDialog();
        }

        
    }
}
