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
    public partial class frm_MainForm : Form
    {
        private Info i;
        private TcpClientModel tcp;
        public frm_MainForm(TcpClientModel tcp, Info i)
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
            string sendData = "Request:" + req +";" + n;
            int iSuccess = tcp.sendData(sendData);
            rtb_Status.Text += "Sending: " + sendData + "\r\n";
            if (iSuccess == -1)
            {
                MessageBox.Show("Connecting Error..");
                return;
            }
            string receiveData = null;
            iSuccess = tcp.receiveData(ref receiveData);
            rtb_Status.Text += "Received: " + receiveData + "\r\n";
            if (iSuccess == -1)
            {
                MessageBox.Show("Connecting Error..");
                return;
            }
            string[] dataformat = null;
            dataformat = receiveData.Split(new char[] { ':', ';' });
            if (dataformat.Length != 6
                || dataformat[0] != "Result")
            {
                MessageBox.Show("Data Error...");
                return;
            }

            txt_Result.Text = dataformat[1];
            txt_Time.Text = dataformat[2];
            lbl_Title.Text = dataformat[3];
            
            double dmoney = Math.Round(Double.Parse(dataformat[4]), 1);
            txt_Money.Text = dmoney + "";
            txt_MpR.Text = dataformat[5];
        }

        private void btn_Recharge_Click(object sender, EventArgs e)
        {
            frm_RechargeForm frm = new frm_RechargeForm(tcp);
            frm.ShowDialog();
        }

        private void frm_MainForm_Load(object sender, EventArgs e)
        {

        }

       
    }
}
