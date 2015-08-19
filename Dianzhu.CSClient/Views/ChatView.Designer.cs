namespace Dianzhu.CSClient.Views
{
    partial class ChatView
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbxMsgList = new System.Windows.Forms.TextBox();
            this.tbxMsg = new System.Windows.Forms.TextBox();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxMsgList
            // 
            this.tbxMsgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxMsgList.Location = new System.Drawing.Point(0, 0);
            this.tbxMsgList.Multiline = true;
            this.tbxMsgList.Name = "tbxMsgList";
            this.tbxMsgList.Size = new System.Drawing.Size(308, 350);
            this.tbxMsgList.TabIndex = 0;
            // 
            // tbxMsg
            // 
            this.tbxMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxMsg.Location = new System.Drawing.Point(0, 0);
            this.tbxMsg.Multiline = true;
            this.tbxMsg.Name = "tbxMsg";
            this.tbxMsg.Size = new System.Drawing.Size(233, 24);
            this.tbxMsg.TabIndex = 0;
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendMsg.Location = new System.Drawing.Point(233, 0);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(75, 24);
            this.btnSendMsg.TabIndex = 1;
            this.btnSendMsg.Text = "发送";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbxMsg);
            this.panel1.Controls.Add(this.btnSendMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 350);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 24);
            this.panel1.TabIndex = 2;
            // 
            // ChatView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbxMsgList);
            this.Controls.Add(this.panel1);
            this.Name = "ChatView";
            this.Size = new System.Drawing.Size(308, 374);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxMsgList;
        private System.Windows.Forms.TextBox tbxMsg;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.Panel panel1;
    }
}
