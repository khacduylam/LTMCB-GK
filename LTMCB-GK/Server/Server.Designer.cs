namespace Server
{
    partial class Server
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.txt_Port = new System.Windows.Forms.TextBox();
            this.rtb_Client = new System.Windows.Forms.RichTextBox();
            this.rtb_Procedure = new System.Windows.Forms.RichTextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // txt_IP
            // 
            this.txt_IP.Location = new System.Drawing.Point(106, 50);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(159, 20);
            this.txt_IP.TabIndex = 2;
            this.txt_IP.Text = "127.0.0.1";
            // 
            // txt_Port
            // 
            this.txt_Port.Location = new System.Drawing.Point(106, 92);
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.Size = new System.Drawing.Size(159, 20);
            this.txt_Port.TabIndex = 3;
            this.txt_Port.Text = "12345";
            // 
            // rtb_Client
            // 
            this.rtb_Client.Location = new System.Drawing.Point(59, 151);
            this.rtb_Client.Name = "rtb_Client";
            this.rtb_Client.Size = new System.Drawing.Size(265, 80);
            this.rtb_Client.TabIndex = 4;
            this.rtb_Client.Text = "";
            // 
            // rtb_Procedure
            // 
            this.rtb_Procedure.Location = new System.Drawing.Point(59, 266);
            this.rtb_Procedure.Name = "rtb_Procedure";
            this.rtb_Procedure.Size = new System.Drawing.Size(265, 77);
            this.rtb_Procedure.TabIndex = 5;
            this.rtb_Procedure.Text = "";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(397, 50);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(113, 72);
            this.btn_Start.TabIndex = 6;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 356);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.rtb_Procedure);
            this.Controls.Add(this.rtb_Client);
            this.Controls.Add(this.txt_Port);
            this.Controls.Add(this.txt_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Server";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.TextBox txt_Port;
        private System.Windows.Forms.RichTextBox rtb_Client;
        private System.Windows.Forms.RichTextBox rtb_Procedure;
        private System.Windows.Forms.Button btn_Start;
    }
}

