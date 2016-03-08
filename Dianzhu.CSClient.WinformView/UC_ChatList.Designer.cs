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
            this.SuspendLayout();
            // 
            // pnlChatList
            // 
            this.pnlChatList.AutoScroll = true;
            this.pnlChatList.AutoSize = true;
            this.pnlChatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChatList.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.pnlChatList.Location = new System.Drawing.Point(0, 0);
            this.pnlChatList.Name = "pnlChatList";
            this.pnlChatList.Size = new System.Drawing.Size(259, 225);
            this.pnlChatList.TabIndex = 0;
            // 
            // UC_ChatList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlChatList);
            this.Name = "UC_ChatList";
            this.Size = new System.Drawing.Size(259, 225);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlChatList;
    }
}
