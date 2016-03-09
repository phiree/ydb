namespace Dianzhu.CSClient.WinformView
{
    public partial class UC_ChatList:System.Windows.Forms.UserControl,Dianzhu.CSClient.IView.IViewChatList
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlChatList = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.btnSendText = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSendMessageAudio = new System.Windows.Forms.Button();
            this.btnMessageSendImage = new System.Windows.Forms.Button();
            this.btnMessageCapture = new System.Windows.Forms.Button();
            this.btnSendMessageVideo = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlChatList
            // 
            this.pnlChatList.AutoScroll = true;
            this.pnlChatList.AutoSize = true;
            this.pnlChatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChatList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlChatList.Location = new System.Drawing.Point(0, 0);
            this.pnlChatList.Name = "pnlChatList";
            this.pnlChatList.Size = new System.Drawing.Size(631, 411);
            this.pnlChatList.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbxMessage);
            this.panel1.Controls.Add(this.btnSendText);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 411);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 65);
            this.panel1.TabIndex = 1;
            // 
            // tbxMessage
            // 
            this.tbxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxMessage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxMessage.Location = new System.Drawing.Point(0, 0);
            this.tbxMessage.Multiline = true;
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.Size = new System.Drawing.Size(556, 25);
            this.tbxMessage.TabIndex = 0;
            this.tbxMessage.Text = "输入";
            // 
            // btnSendText
            // 
            this.btnSendText.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendText.Location = new System.Drawing.Point(556, 0);
            this.btnSendText.Name = "btnSendText";
            this.btnSendText.Size = new System.Drawing.Size(75, 25);
            this.btnSendText.TabIndex = 1;
            this.btnSendText.Text = "发送";
            this.btnSendText.UseVisualStyleBackColor = true;
            this.btnSendText.Click += new System.EventHandler(this.btnSendText_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(631, 40);
            this.panel2.TabIndex = 2;
            // 
            // btnSendMessageAudio
            // 
            this.btnSendMessageAudio.Location = new System.Drawing.Point(3, 9);
            this.btnSendMessageAudio.Name = "btnSendMessageAudio";
            this.btnSendMessageAudio.Size = new System.Drawing.Size(75, 23);
            this.btnSendMessageAudio.TabIndex = 0;
            this.btnSendMessageAudio.Text = "发送音频";
            this.btnSendMessageAudio.UseVisualStyleBackColor = true;
            // 
            // btnMessageSendImage
            // 
            this.btnMessageSendImage.Location = new System.Drawing.Point(84, 9);
            this.btnMessageSendImage.Name = "btnMessageSendImage";
            this.btnMessageSendImage.Size = new System.Drawing.Size(75, 23);
            this.btnMessageSendImage.TabIndex = 0;
            this.btnMessageSendImage.Text = "发送图片";
            this.btnMessageSendImage.UseVisualStyleBackColor = true;
            // 
            // btnMessageCapture
            // 
            this.btnMessageCapture.Location = new System.Drawing.Point(3, 9);
            this.btnMessageCapture.Name = "btnMessageCapture";
            this.btnMessageCapture.Size = new System.Drawing.Size(75, 23);
            this.btnMessageCapture.TabIndex = 0;
            this.btnMessageCapture.Text = "截图";
            this.btnMessageCapture.UseVisualStyleBackColor = true;
            // 
            // btnSendMessageVideo
            // 
            this.btnSendMessageVideo.Location = new System.Drawing.Point(84, 9);
            this.btnSendMessageVideo.Name = "btnSendMessageVideo";
            this.btnSendMessageVideo.Size = new System.Drawing.Size(75, 23);
            this.btnSendMessageVideo.TabIndex = 0;
            this.btnSendMessageVideo.Text = "发送视频";
            this.btnSendMessageVideo.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSendMessageAudio);
            this.panel3.Controls.Add(this.btnSendMessageVideo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(165, 40);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnMessageCapture);
            this.panel4.Controls.Add(this.btnMessageSendImage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(468, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(163, 40);
            this.panel4.TabIndex = 2;
            // 
            // UC_ChatList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlChatList);
            this.Controls.Add(this.panel1);
            this.Name = "UC_ChatList";
            this.Size = new System.Drawing.Size(631, 476);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlChatList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSendText;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMessageCapture;
        private System.Windows.Forms.Button btnMessageSendImage;
        private System.Windows.Forms.Button btnSendMessageVideo;
        private System.Windows.Forms.Button btnSendMessageAudio;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
    }
}
