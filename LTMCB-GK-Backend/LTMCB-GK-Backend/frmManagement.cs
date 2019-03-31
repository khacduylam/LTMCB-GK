using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTMCB_GK_Backend {
    public partial class frmManagement : Form {
        static String MongodbUrl = "mongodb://localhost:27017";
        static String DatabaseName = "LTMCB_DB";
        private Database Database;
        private ServiceModel service;
        public frmManagement() {
            InitializeComponent();
        }

        private void frmManagement_Load(object sender, EventArgs e) {
            CheckForIllegalCrossThreadCalls = false;

            this.Database = new Database(MongodbUrl, DatabaseName);           

            txtIP.Text = "127.0.0.1";
            txtPort.Text = "13000";
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e) {
            int n;
            String ip = txtIP.Text;
            String port = txtPort.Text;
            if(ip.Length == 0 ||
               port.Length == 0 ||
               !int.TryParse(port, out n)) {

                this.service = new ServiceModel();
            } else {

                this.service = new ServiceModel(ip, n);
            }

            this.service.StartServer();
            Thread t = new Thread(this.service.ServeMultiClient);
            t.Start(lstConnectionManagement);

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            txtIP.Enabled = false;
            txtPort.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e) {
            this.service.StopServer();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            txtIP.Enabled = true;
            txtPort.Enabled = true;
        }
    }
}
