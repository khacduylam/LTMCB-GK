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
    public partial class frm_NavigationForm : Form
    {
        private TcpClientModel tcp;
        public frm_NavigationForm(TcpClientModel tcp)
        {
            this.tcp = tcp;
            InitializeComponent();
        }

        private void btn_signin_Click(object sender, EventArgs e)
        {
            frm_SigninForm frm = new frm_SigninForm(tcp);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btn_signup_Click(object sender, EventArgs e)
        {
            frm_SignupForm frm = new frm_SignupForm(tcp);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}