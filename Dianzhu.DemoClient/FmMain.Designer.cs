namespace Dianzhu.DemoClient
{
    partial class FmMain
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
            this.tbxUserName = new System.Windows.Forms.TextBox();
            this.tbxPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxOrderId = new System.Windows.Forms.TextBox();
            this.btnAdvList = new System.Windows.Forms.Button();
            this.btnOnlineUsers = new System.Windows.Forms.Button();
            this.lblAssignedCS = new System.Windows.Forms.Label();
            this.tbxManualAssignedCS = new System.Windows.Forms.TextBox();
            this.lblLoginStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.btnAudio = new System.Windows.Forms.Button();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbxLog = new System.Windows.Forms.RichTextBox();
            this.dlgSelectPic = new System.Windows.Forms.OpenFileDialog();
            this.pnlChat = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // tbxUserName
            // 
            this.tbxUserName.Location = new System.Drawing.Point(59, 12);
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(128, 21);
            this.tbxUserName.TabIndex = 1;
            this.tbxUserName.Text = "f@f.f";
            // 
            // tbxPwd
            // 
            this.tbxPwd.Location = new System.Drawing.Point(240, 14);
            this.tbxPwd.Name = "tbxPwd";
            this.tbxPwd.Size = new System.Drawing.Size(68, 21);
            this.tbxPwd.TabIndex = 3;
            this.tbxPwd.Text = "123456";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(314, 12);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tbxOrderId);
            this.panel1.Controls.Add(this.btnAdvList);
            this.panel1.Controls.Add(this.btnOnlineUsers);
            this.panel1.Controls.Add(this.lblAssignedCS);
            this.panel1.Controls.Add(this.tbxManualAssignedCS);
            this.panel1.Controls.Add(this.lblLoginStatus);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbxPwd);
            this.panel1.Controls.Add(this.tbxUserName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 81);
            this.panel1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "订单ID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "手动指定客服:";
            // 
            // tbxOrderId
            // 
            this.tbxOrderId.Location = new System.Drawing.Point(397, 47);
            this.tbxOrderId.Name = "tbxOrderId";
            this.tbxOrderId.Size = new System.Drawing.Size(229, 21);
            this.tbxOrderId.TabIndex = 11;
            // 
            // btnAdvList
            // 
            this.btnAdvList.Location = new System.Drawing.Point(686, 9);
            this.btnAdvList.Name = "btnAdvList";
            this.btnAdvList.Size = new System.Drawing.Size(75, 23);
            this.btnAdvList.TabIndex = 8;
            this.btnAdvList.Text = "AdvertisementList";
            this.btnAdvList.UseVisualStyleBackColor = true;
            this.btnAdvList.Click += new System.EventHandler(this.btnAdvList_Click);
            // 
            // btnOnlineUsers
            // 
            this.btnOnlineUsers.Location = new System.Drawing.Point(605, 9);
            this.btnOnlineUsers.Name = "btnOnlineUsers";
            this.btnOnlineUsers.Size = new System.Drawing.Size(75, 23);
            this.btnOnlineUsers.TabIndex = 8;
            this.btnOnlineUsers.Text = "在线用户";
            this.btnOnlineUsers.UseVisualStyleBackColor = true;
            this.btnOnlineUsers.Click += new System.EventHandler(this.btnOnlineUsers_Click);
            // 
            // lblAssignedCS
            // 
            this.lblAssignedCS.AutoSize = true;
            this.lblAssignedCS.Location = new System.Drawing.Point(510, 18);
            this.lblAssignedCS.Name = "lblAssignedCS";
            this.lblAssignedCS.Size = new System.Drawing.Size(17, 12);
            this.lblAssignedCS.TabIndex = 7;
            this.lblAssignedCS.Text = "--";
            // 
            // tbxManualAssignedCS
            // 
            this.tbxManualAssignedCS.Location = new System.Drawing.Point(101, 47);
            this.tbxManualAssignedCS.Name = "tbxManualAssignedCS";
            this.tbxManualAssignedCS.Size = new System.Drawing.Size(232, 21);
            this.tbxManualAssignedCS.TabIndex = 11;
            // 
            // lblLoginStatus
            // 
            this.lblLoginStatus.AutoSize = true;
            this.lblLoginStatus.ForeColor = System.Drawing.Color.Chocolate;
            this.lblLoginStatus.Location = new System.Drawing.Point(395, 17);
            this.lblLoginStatus.Name = "lblLoginStatus";
            this.lblLoginStatus.Size = new System.Drawing.Size(17, 12);
            this.lblLoginStatus.TabIndex = 5;
            this.lblLoginStatus.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "当前客服";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbxMessage);
            this.panel2.Controls.Add(this.btnAudio);
            this.panel2.Controls.Add(this.btnSelectImage);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 252);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(873, 23);
            this.panel2.TabIndex = 6;
            // 
            // tbxMessage
            // 
            this.tbxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxMessage.Location = new System.Drawing.Point(0, 0);
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(648, 21);
            this.tbxMessage.TabIndex = 1;
            this.tbxMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxMessage_KeyPress);
            // 
            // btnAudio
            // 
            this.btnAudio.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAudio.Location = new System.Drawing.Point(648, 0);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(75, 23);
            this.btnAudio.TabIndex = 6;
            this.btnAudio.Text = "音频";
            this.btnAudio.UseVisualStyleBackColor = true;
            this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectImage.Location = new System.Drawing.Point(723, 0);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(75, 23);
            this.btnSelectImage.TabIndex = 5;
            this.btnSelectImage.Text = "图片";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Location = new System.Drawing.Point(798, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "文本";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 12);
            this.label4.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tbxLog);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 275);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(873, 179);
            this.panel3.TabIndex = 0;
            // 
            // tbxLog
            // 
            this.tbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxLog.Location = new System.Drawing.Point(0, 0);
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.Size = new System.Drawing.Size(873, 179);
            this.tbxLog.TabIndex = 0;
            this.tbxLog.Text = "";
            // 
            // dlgSelectPic
            // 
            this.dlgSelectPic.FileName = "openFileDialog1";
            // 
            // pnlChat
            // 
            this.pnlChat.AutoScroll = true;
            this.pnlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChat.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlChat.Location = new System.Drawing.Point(0, 81);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(873, 171);
            this.pnlChat.TabIndex = 8;
            this.pnlChat.WrapContents = false;
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 454);
            this.Controls.Add(this.pnlChat);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "FmMain";
            this.Text = "DemoCustomer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FmMain_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxUserName;
        private System.Windows.Forms.TextBox tbxPwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Label lblLoginStatus;
        private System.Windows.Forms.OpenFileDialog dlgSelectPic;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.FlowLayoutPanel pnlChat;
        private System.Windows.Forms.Label lblAssignedCS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAudio;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxOrderId;
        private System.Windows.Forms.Button btnOnlineUsers;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxManualAssignedCS;
        private System.Windows.Forms.RichTextBox tbxLog;
        private System.Windows.Forms.Button btnAdvList;
    }
}