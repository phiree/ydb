namespace Dianzhu.CSClient.WinformView
{
    partial class UC_SearchResult
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
            this.pnlSearchResult = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pnlSearchResult
            // 
            this.pnlSearchResult.AutoScroll = true;
            this.pnlSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchResult.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlSearchResult.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchResult.Name = "pnlSearchResult";
            this.pnlSearchResult.Size = new System.Drawing.Size(244, 417);
            this.pnlSearchResult.TabIndex = 0;
            // 
            // UC_SearchResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSearchResult);
            this.Name = "UC_SearchResult";
            this.Size = new System.Drawing.Size(244, 417);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlSearchResult;
    }
}
