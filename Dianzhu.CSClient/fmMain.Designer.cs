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
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbxChatLog = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbxMsg = new System.Windows.Forms.TextBox();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.gbCustomerList = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(652, 353);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.splitContainer1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(652, 353);
            this.panel3.TabIndex = 1;
            // 
            // tbxChatLog
            // 
            this.tbxChatLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxChatLog.Location = new System.Drawing.Point(0, 0);
            this.tbxChatLog.Name = "tbxChatLog";
            this.tbxChatLog.Size = new System.Drawing.Size(278, 326);
            this.tbxChatLog.TabIndex = 4;
            this.tbxChatLog.Text = "";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tbxMsg);
            this.panel5.Controls.Add(this.btnSendMsg);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 326);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(278, 25);
            this.panel5.TabIndex = 0;
            // 
            // tbxMsg
            // 
            this.tbxMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxMsg.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbxMsg.Location = new System.Drawing.Point(0, 0);
            this.tbxMsg.Name = "tbxMsg";
            this.tbxMsg.Size = new System.Drawing.Size(201, 21);
            this.tbxMsg.TabIndex = 3;
            this.tbxMsg.TextChanged += new System.EventHandler(this.tbxMsg_TextChanged);
            this.tbxMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxMsg_KeyPress);
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendMsg.Location = new System.Drawing.Point(201, 0);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(75, 23);
            this.btnSendMsg.TabIndex = 2;
            this.btnSendMsg.Text = "发送";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // gbCustomerList
            // 
            this.gbCustomerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gbCustomerList.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCustomerList.Location = new System.Drawing.Point(0, 0);
            this.gbCustomerList.Name = "gbCustomerList";
            this.gbCustomerList.Size = new System.Drawing.Size(652, 100);
            this.gbCustomerList.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(650, 351);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tbxChatLog);
            this.splitContainer2.Panel1.Controls.Add(this.panel5);
            this.splitContainer2.Size = new System.Drawing.Size(464, 351);
            this.splitContainer2.SplitterDistance = 278;
            this.splitContainer2.TabIndex = 0;
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
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel gbCustomerList;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tbxMsg;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.RichTextBox tbxChatLog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}