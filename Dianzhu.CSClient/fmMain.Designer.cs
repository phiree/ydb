namespace Dianzhu.CSClient
{
    partial class fmMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDemoAddCustomer = new System.Windows.Forms.Button();
            this.gbCustomerList = new System.Windows.Forms.FlowLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlChatHistory = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.tbxMsg = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbCustomerList.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(652, 353);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(203, 353);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pnlChatHistory);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(203, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(266, 353);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(469, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(183, 353);
            this.panel4.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnDemoAddCustomer
            // 
            this.btnDemoAddCustomer.Location = new System.Drawing.Point(84, 3);
            this.btnDemoAddCustomer.Name = "btnDemoAddCustomer";
            this.btnDemoAddCustomer.Size = new System.Drawing.Size(116, 23);
            this.btnDemoAddCustomer.TabIndex = 0;
            this.btnDemoAddCustomer.Text = "(DEMO)增加客户";
            this.btnDemoAddCustomer.UseVisualStyleBackColor = true;
            this.btnDemoAddCustomer.Click += new System.EventHandler(this.btnDemoAddCustomer_Click);
            // 
            // gbCustomerList
            // 
            this.gbCustomerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gbCustomerList.Controls.Add(this.button1);
            this.gbCustomerList.Controls.Add(this.btnDemoAddCustomer);
            this.gbCustomerList.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCustomerList.Location = new System.Drawing.Point(0, 0);
            this.gbCustomerList.Name = "gbCustomerList";
            this.gbCustomerList.Size = new System.Drawing.Size(652, 100);
            this.gbCustomerList.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tbxMsg);
            this.panel5.Controls.Add(this.btnSendMsg);
            this.panel5.Location = new System.Drawing.Point(6, 281);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(254, 60);
            this.panel5.TabIndex = 0;
            // 
            // pnlChatHistory
            // 
            this.pnlChatHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChatHistory.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.pnlChatHistory.Location = new System.Drawing.Point(6, 6);
            this.pnlChatHistory.Name = "pnlChatHistory";
            this.pnlChatHistory.Size = new System.Drawing.Size(251, 249);
            this.pnlChatHistory.TabIndex = 1;
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Location = new System.Drawing.Point(176, 23);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(75, 23);
            this.btnSendMsg.TabIndex = 2;
            this.btnSendMsg.Text = "发送";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // tbxMsg
            // 
            this.tbxMsg.Location = new System.Drawing.Point(4, 23);
            this.tbxMsg.Name = "tbxMsg";
            this.tbxMsg.Size = new System.Drawing.Size(166, 21);
            this.tbxMsg.TabIndex = 3;
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 453);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbCustomerList);
            this.Name = "fmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.gbCustomerList.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDemoAddCustomer;
        private System.Windows.Forms.FlowLayoutPanel gbCustomerList;
        private System.Windows.Forms.FlowLayoutPanel pnlChatHistory;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tbxMsg;
        private System.Windows.Forms.Button btnSendMsg;
    }
}