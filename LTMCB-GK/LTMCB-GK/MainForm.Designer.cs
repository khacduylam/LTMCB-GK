namespace LTMCB_GK
{
    partial class frm_MainForm
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
            this.txt_Input = new System.Windows.Forms.TextBox();
            this.txt_Result = new System.Windows.Forms.TextBox();
            this.txt_Time = new System.Windows.Forms.TextBox();
            this.lbl_Number = new System.Windows.Forms.Label();
            this.lbl_Result = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.btn_Recharge = new System.Windows.Forms.Button();
            this.btn_Iteration = new System.Windows.Forms.Button();
            this.btn_Recursion = new System.Windows.Forms.Button();
            this.lbl_MoneyPerReq = new System.Windows.Forms.Label();
            this.lbl_Money = new System.Windows.Forms.Label();
            this.txt_Money = new System.Windows.Forms.TextBox();
            this.txt_MpR = new System.Windows.Forms.TextBox();
            this.rtb_Status = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txt_Input
            // 
            this.txt_Input.Location = new System.Drawing.Point(140, 42);
            this.txt_Input.Name = "txt_Input";
            this.txt_Input.Size = new System.Drawing.Size(306, 20);
            this.txt_Input.TabIndex = 2;
            // 
            // txt_Result
            // 
            this.txt_Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Result.Location = new System.Drawing.Point(140, 172);
            this.txt_Result.Name = "txt_Result";
            this.txt_Result.ReadOnly = true;
            this.txt_Result.Size = new System.Drawing.Size(181, 23);
            this.txt_Result.TabIndex = 3;
            // 
            // txt_Time
            // 
            this.txt_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Time.Location = new System.Drawing.Point(342, 172);
            this.txt_Time.Name = "txt_Time";
            this.txt_Time.ReadOnly = true;
            this.txt_Time.Size = new System.Drawing.Size(104, 23);
            this.txt_Time.TabIndex = 4;
            // 
            // lbl_Number
            // 
            this.lbl_Number.AutoSize = true;
            this.lbl_Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Number.Location = new System.Drawing.Point(106, 36);
            this.lbl_Number.Name = "lbl_Number";
            this.lbl_Number.Size = new System.Drawing.Size(28, 26);
            this.lbl_Number.TabIndex = 8;
            this.lbl_Number.Text = "N";
            // 
            // lbl_Result
            // 
            this.lbl_Result.AutoSize = true;
            this.lbl_Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Result.Location = new System.Drawing.Point(208, 148);
            this.lbl_Result.Name = "lbl_Result";
            this.lbl_Result.Size = new System.Drawing.Size(48, 17);
            this.lbl_Result.TabIndex = 9;
            this.lbl_Result.Text = "Result";
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Time.Location = new System.Drawing.Point(375, 148);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(39, 17);
            this.lbl_Time.TabIndex = 10;
            this.lbl_Time.Text = "Time";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(284, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(50, 17);
            this.lbl_Title.TabIndex = 11;
            this.lbl_Title.Text = "USER";
            // 
            // btn_Recharge
            // 
            this.btn_Recharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Recharge.Location = new System.Drawing.Point(562, 114);
            this.btn_Recharge.Name = "btn_Recharge";
            this.btn_Recharge.Size = new System.Drawing.Size(100, 47);
            this.btn_Recharge.TabIndex = 13;
            this.btn_Recharge.Text = "Recharge";
            this.btn_Recharge.UseVisualStyleBackColor = true;
            this.btn_Recharge.Click += new System.EventHandler(this.btn_Recharge_Click);
            // 
            // btn_Iteration
            // 
            this.btn_Iteration.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Iteration.Location = new System.Drawing.Point(140, 89);
            this.btn_Iteration.Name = "btn_Iteration";
            this.btn_Iteration.Size = new System.Drawing.Size(104, 38);
            this.btn_Iteration.TabIndex = 15;
            this.btn_Iteration.Text = "Iteration";
            this.btn_Iteration.UseVisualStyleBackColor = true;
            this.btn_Iteration.Click += new System.EventHandler(this.btn_Iteration_Click);
            // 
            // btn_Recursion
            // 
            this.btn_Recursion.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Recursion.Location = new System.Drawing.Point(342, 89);
            this.btn_Recursion.Name = "btn_Recursion";
            this.btn_Recursion.Size = new System.Drawing.Size(104, 38);
            this.btn_Recursion.TabIndex = 16;
            this.btn_Recursion.Text = "Recursion";
            this.btn_Recursion.UseVisualStyleBackColor = true;
            this.btn_Recursion.Click += new System.EventHandler(this.btn_Recursion_Click);
            // 
            // lbl_MoneyPerReq
            // 
            this.lbl_MoneyPerReq.AutoSize = true;
            this.lbl_MoneyPerReq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MoneyPerReq.Location = new System.Drawing.Point(464, 68);
            this.lbl_MoneyPerReq.Name = "lbl_MoneyPerReq";
            this.lbl_MoneyPerReq.Size = new System.Drawing.Size(93, 15);
            this.lbl_MoneyPerReq.TabIndex = 19;
            this.lbl_MoneyPerReq.Text = "Money/Request";
            // 
            // lbl_Money
            // 
            this.lbl_Money.AutoSize = true;
            this.lbl_Money.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Money.Location = new System.Drawing.Point(484, 28);
            this.lbl_Money.Name = "lbl_Money";
            this.lbl_Money.Size = new System.Drawing.Size(72, 15);
            this.lbl_Money.TabIndex = 18;
            this.lbl_Money.Text = "Your Money";
            // 
            // txt_Money
            // 
            this.txt_Money.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Money.Location = new System.Drawing.Point(562, 27);
            this.txt_Money.Name = "txt_Money";
            this.txt_Money.ReadOnly = true;
            this.txt_Money.Size = new System.Drawing.Size(100, 21);
            this.txt_Money.TabIndex = 20;
            // 
            // txt_MpR
            // 
            this.txt_MpR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MpR.Location = new System.Drawing.Point(562, 67);
            this.txt_MpR.Name = "txt_MpR";
            this.txt_MpR.ReadOnly = true;
            this.txt_MpR.Size = new System.Drawing.Size(100, 21);
            this.txt_MpR.TabIndex = 21;
            // 
            // rtb_Status
            // 
            this.rtb_Status.Location = new System.Drawing.Point(96, 228);
            this.rtb_Status.Name = "rtb_Status";
            this.rtb_Status.ReadOnly = true;
            this.rtb_Status.Size = new System.Drawing.Size(389, 127);
            this.rtb_Status.TabIndex = 17;
            this.rtb_Status.Text = "";
            // 
            // frm_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 367);
            this.Controls.Add(this.txt_MpR);
            this.Controls.Add(this.txt_Money);
            this.Controls.Add(this.lbl_MoneyPerReq);
            this.Controls.Add(this.lbl_Money);
            this.Controls.Add(this.rtb_Status);
            this.Controls.Add(this.btn_Recursion);
            this.Controls.Add(this.btn_Iteration);
            this.Controls.Add(this.btn_Recharge);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.lbl_Time);
            this.Controls.Add(this.lbl_Result);
            this.Controls.Add(this.lbl_Number);
            this.Controls.Add(this.txt_Time);
            this.Controls.Add(this.txt_Result);
            this.Controls.Add(this.txt_Input);
            this.Name = "frm_MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.frm_MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Input;
        private System.Windows.Forms.TextBox txt_Result;
        private System.Windows.Forms.TextBox txt_Time;
        private System.Windows.Forms.Label lbl_Number;
        private System.Windows.Forms.Label lbl_Result;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Button btn_Recharge;
        private System.Windows.Forms.Button btn_Iteration;
        private System.Windows.Forms.Button btn_Recursion;
        private System.Windows.Forms.Label lbl_MoneyPerReq;
        private System.Windows.Forms.Label lbl_Money;
        private System.Windows.Forms.TextBox txt_Money;
        private System.Windows.Forms.TextBox txt_MpR;
        private System.Windows.Forms.RichTextBox rtb_Status;
    }
}

