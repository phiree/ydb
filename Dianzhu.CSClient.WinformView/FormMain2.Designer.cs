namespace Dianzhu.CSClient.WinformView
{
    partial class FormMain2
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
            this.uC_CustomerList1 = new Dianzhu.CSClient.WinformView.UC_CustomerList();
            this.SuspendLayout();
            // 
            // uC_CustomerList1
            // 
            this.uC_CustomerList1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_CustomerList1.Location = new System.Drawing.Point(0, 0);
            this.uC_CustomerList1.Name = "uC_CustomerList1";
            this.uC_CustomerList1.Size = new System.Drawing.Size(582, 82);
            this.uC_CustomerList1.TabIndex = 0;
            // 
            // FormMain2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 364);
            this.Controls.Add(this.uC_CustomerList1);
            this.Name = "FormMain2";
            this.Text = "FormMain2";
            this.ResumeLayout(false);

        }

        #endregion

        private UC_CustomerList uC_CustomerList1;
    }
}