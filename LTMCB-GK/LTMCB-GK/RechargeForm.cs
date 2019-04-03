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
    public partial class frm_RechargeForm : Form
    {
        private TcpClientModel tcp;
        public frm_RechargeForm(TcpClientModel tcp)
        {
            this.tcp = tcp;
            InitializeComponent();
        }

        private void btn_recharge_Click(object sender, EventArgs e)
        {
            string money = txt_Money.Text;
            double iMoney;
            bool bSuccess = Double.TryParse(money, out iMoney);
            if(!bSuccess || iMoney <= 0)
            {
                MessageBox.Show("Invalid Data..");
                return;
            }
            string data = "Recharge:" + money + ";" + txt_Password.Text;
            int iSuccess = tcp.sendData(data);
            if(iSuccess == -1)
            {
                MessageBox.Show("Sending Error..");
                return;
            }
            iSuccess = tcp.receiveData(ref data);
            if(iSuccess == -1)
            {
                MessageBox.Show("Receiving Error..");
                return;
            }
            string[] dataformat = data.Split(':');
            if(dataformat[0] != "Success")
            {
                MessageBox.Show("Recharging Error..");
                return;
            }
            MessageBox.Show("Recharging Success");
            this.Close();
        }
    }
}
