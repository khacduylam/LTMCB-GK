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
    public partial class frm_SignupForm : Form
    {
        private TcpClientModel tcp;
        public frm_SignupForm(TcpClientModel tcp)
        {
            this.tcp = tcp;
            InitializeComponent();
        }

        private void btn_signup_Click(object sender, EventArgs e)
        {
            string usr = txt_Username.Text;
            string pw = txt_Password.Text;
            string repw = txt_retypePw.Text;

            if(pw != repw)
            {
                MessageBox.Show("Mismatch Password!");
                return;
            }

            string senddata = "Signup:" + usr + ";" + pw;
            int iSuccess = tcp.sendData(senddata);
            if (iSuccess == -1)
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
            string[] dataformat = receiveData.Split(':');
            MessageBox.Show(receiveData);
            if (dataformat[0] != "Success")
            {
                return;
            }
            this.Close();
        }
    }
}
