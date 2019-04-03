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
    public partial class frm_SigninForm : Form
    {
        private TcpClientModel tcp;
        public frm_SigninForm(TcpClientModel tcp)
        {
            this.tcp = tcp;
            InitializeComponent();
        }

        private void btn_signin_Click(object sender, EventArgs e)
        {
            string usr = txt_Username.Text;
            string pw = txt_Password.Text;
            string sendData = "Signin:" + usr + ";" + pw;
            int iSuccess = tcp.sendData(sendData);
            if(iSuccess == -1)
            {
                MessageBox.Show("Connection Error!");
                return;
            }
            string receiveData = null;
            iSuccess = tcp.receiveData(ref receiveData);
            if (iSuccess == -1)
            {
                MessageBox.Show("Connection Error..");
                return;
            }
            string[] dataformat = receiveData.Split(
                new char[] { ':', ';' });
            if(dataformat[0]!="Success")
            {
                MessageBox.Show("Invalid username/password");
                return;
            }
            if(dataformat.Length !=4)
            {
                MessageBox.Show("Receiving Error..");
                return;
            }
            Info i = new Info();
            i.username = dataformat[1];
            i.money = dataformat[2];
            i.MpR = dataformat[3];

            this.Hide();
            frm_MainForm frm = new frm_MainForm(tcp, i);
            frm.ShowDialog();
            this.Show();
        }

        private void lbl_Username_Click(object sender, EventArgs e)
        {

        }
    }
}
