namespace Dianzhu.CSClient.WinformView
{
    partial class FormMain
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
            this.scmain = new System.Windows.Forms.SplitContainer();
            this.pnlCustomerList = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnlOrder = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxServiceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxServiceBusinessName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxServiceDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxServiceUnitPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxServiceUrl = new System.Windows.Forms.TextBox();
            this.btnPushExternalService = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.pnlChat = new System.Windows.Forms.FlowLayoutPanel();
            this.tbxChatLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbxChatMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlResultService = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnKeywords = new System.Windows.Forms.Button();
            this.btnMap = new System.Windows.Forms.Button();
            this.btnBusiness = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnSearchOut = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbxKeywords = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.scmain)).BeginInit();
            this.scmain.Panel1.SuspendLayout();
            this.scmain.Panel2.SuspendLayout();
            this.scmain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pnlOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scmain
            // 
            this.scmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scmain.Location = new System.Drawing.Point(0, 0);
            this.scmain.Name = "scmain";
            this.scmain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scmain.Panel1
            // 
            this.scmain.Panel1.AutoScroll = true;
            this.scmain.Panel1.Controls.Add(this.pnlCustomerList);
            // 
            // scmain.Panel2
            // 
            this.scmain.Panel2.Controls.Add(this.splitContainer2);
            this.scmain.Size = new System.Drawing.Size(841, 639);
            this.scmain.SplitterDistance = 114;
            this.scmain.TabIndex = 0;
            // 
            // pnlCustomerList
            // 
            this.pnlCustomerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomerList.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomerList.Name = "pnlCustomerList";
            this.pnlCustomerList.Size = new System.Drawing.Size(841, 114);
            this.pnlCustomerList.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pnlOrder);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(841, 521);
            this.splitContainer2.SplitterDistance = 247;
            this.splitContainer2.TabIndex = 0;
            // 
            // pnlOrder
            // 
            this.pnlOrder.AutoScroll = true;
            this.pnlOrder.Controls.Add(this.label1);
            this.pnlOrder.Controls.Add(this.tbxServiceName);
            this.pnlOrder.Controls.Add(this.label2);
            this.pnlOrder.Controls.Add(this.tbxServiceBusinessName);
            this.pnlOrder.Controls.Add(this.label3);
            this.pnlOrder.Controls.Add(this.tbxServiceDescription);
            this.pnlOrder.Controls.Add(this.label4);
            this.pnlOrder.Controls.Add(this.tbxServiceUnitPrice);
            this.pnlOrder.Controls.Add(this.label5);
            this.pnlOrder.Controls.Add(this.tbxServiceUrl);
            this.pnlOrder.Controls.Add(this.btnPushExternalService);
            this.pnlOrder.Controls.Add(this.label6);
            this.pnlOrder.Controls.Add(this.textBox1);
            this.pnlOrder.Controls.Add(this.label7);
            this.pnlOrder.Controls.Add(this.textBox2);
            this.pnlOrder.Controls.Add(this.label8);
            this.pnlOrder.Controls.Add(this.textBox3);
            this.pnlOrder.Controls.Add(this.btnCreateOrder);
            this.pnlOrder.Controls.Add(this.button2);
            this.pnlOrder.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlOrder.Location = new System.Drawing.Point(3, 3);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new System.Drawing.Size(280, 473);
            this.pnlOrder.TabIndex = 2;
            this.pnlOrder.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务名称";
            // 
            // tbxServiceName
            // 
            this.tbxServiceName.Location = new System.Drawing.Point(3, 15);
            this.tbxServiceName.Name = "tbxServiceName";
            this.tbxServiceName.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "商家名称";
            // 
            // tbxServiceBusinessName
            // 
            this.tbxServiceBusinessName.Location = new System.Drawing.Point(3, 54);
            this.tbxServiceBusinessName.Name = "tbxServiceBusinessName";
            this.tbxServiceBusinessName.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceBusinessName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "服务描述";
            // 
            // tbxServiceDescription
            // 
            this.tbxServiceDescription.Location = new System.Drawing.Point(3, 93);
            this.tbxServiceDescription.Multiline = true;
            this.tbxServiceDescription.Name = "tbxServiceDescription";
            this.tbxServiceDescription.Size = new System.Drawing.Size(262, 36);
            this.tbxServiceDescription.TabIndex = 0;
            this.tbxServiceDescription.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "价格";
            // 
            // tbxServiceUnitPrice
            // 
            this.tbxServiceUnitPrice.Location = new System.Drawing.Point(3, 147);
            this.tbxServiceUnitPrice.Name = "tbxServiceUnitPrice";
            this.tbxServiceUnitPrice.Size = new System.Drawing.Size(100, 21);
            this.tbxServiceUnitPrice.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "服务网址";
            // 
            // tbxServiceUrl
            // 
            this.tbxServiceUrl.Location = new System.Drawing.Point(3, 186);
            this.tbxServiceUrl.Name = "tbxServiceUrl";
            this.tbxServiceUrl.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceUrl.TabIndex = 0;
            // 
            // btnPushExternalService
            // 
            this.btnPushExternalService.Location = new System.Drawing.Point(3, 213);
            this.btnPushExternalService.Name = "btnPushExternalService";
            this.btnPushExternalService.Size = new System.Drawing.Size(75, 23);
            this.btnPushExternalService.TabIndex = 2;
            this.btnPushExternalService.Text = "推送";
            this.btnPushExternalService.UseVisualStyleBackColor = true;
            this.btnPushExternalService.Click += new System.EventHandler(this.btnPushExternalService_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 239);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "服务时间";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 254);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(262, 21);
            this.textBox1.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 278);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "总价";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 293);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(262, 21);
            this.textBox2.TabIndex = 5;
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Location = new System.Drawing.Point(3, 397);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(75, 23);
            this.btnCreateOrder.TabIndex = 7;
            this.btnCreateOrder.Text = "创建订单";
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 426);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "发送支付链接";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.pnlChat);
            this.splitContainer3.Panel1.Controls.Add(this.tbxChatLog);
            this.splitContainer3.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.pnlResultService);
            this.splitContainer3.Panel2.Controls.Add(this.panel2);
            this.splitContainer3.Size = new System.Drawing.Size(590, 521);
            this.splitContainer3.SplitterDistance = 291;
            this.splitContainer3.TabIndex = 0;
            // 
            // pnlChat
            // 
            this.pnlChat.AutoScroll = true;
            this.pnlChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChat.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlChat.ImeMode = System.Windows.Forms.ImeMode.On;
            this.pnlChat.Location = new System.Drawing.Point(0, 0);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(289, 496);
            this.pnlChat.TabIndex = 3;
            this.pnlChat.WrapContents = false;
            // 
            // tbxChatLog
            // 
            this.tbxChatLog.Location = new System.Drawing.Point(0, 0);
            this.tbxChatLog.Name = "tbxChatLog";
            this.tbxChatLog.Size = new System.Drawing.Size(211, 116);
            this.tbxChatLog.TabIndex = 2;
            this.tbxChatLog.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbxChatMsg);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 496);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 23);
            this.panel1.TabIndex = 1;
            // 
            // tbxChatMsg
            // 
            this.tbxChatMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxChatMsg.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbxChatMsg.Location = new System.Drawing.Point(0, 0);
            this.tbxChatMsg.Name = "tbxChatMsg";
            this.tbxChatMsg.Size = new System.Drawing.Size(214, 21);
            this.tbxChatMsg.TabIndex = 0;
            this.tbxChatMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxChatMsg_KeyPress);
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Location = new System.Drawing.Point(214, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pnlResultService
            // 
            this.pnlResultService.AutoScroll = true;
            this.pnlResultService.Location = new System.Drawing.Point(4, 367);
            this.pnlResultService.Name = "pnlResultService";
            this.pnlResultService.Size = new System.Drawing.Size(340, 150);
            this.pnlResultService.TabIndex = 1;
            this.pnlResultService.WrapContents = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnKeywords);
            this.panel2.Controls.Add(this.btnMap);
            this.panel2.Controls.Add(this.btnBusiness);
            this.panel2.Controls.Add(this.btnManual);
            this.panel2.Controls.Add(this.btnSearchOut);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.tbxKeywords);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(293, 104);
            this.panel2.TabIndex = 0;
            // 
            // btnKeywords
            // 
            this.btnKeywords.Location = new System.Drawing.Point(85, 69);
            this.btnKeywords.Name = "btnKeywords";
            this.btnKeywords.Size = new System.Drawing.Size(75, 23);
            this.btnKeywords.TabIndex = 2;
            this.btnKeywords.Text = "关键字";
            this.btnKeywords.UseVisualStyleBackColor = true;
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(247, 69);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(75, 23);
            this.btnMap.TabIndex = 2;
            this.btnMap.Text = "地图";
            this.btnMap.UseVisualStyleBackColor = true;
            // 
            // btnBusiness
            // 
            this.btnBusiness.Location = new System.Drawing.Point(166, 69);
            this.btnBusiness.Name = "btnBusiness";
            this.btnBusiness.Size = new System.Drawing.Size(75, 23);
            this.btnBusiness.TabIndex = 2;
            this.btnBusiness.Text = "服务商";
            this.btnBusiness.UseVisualStyleBackColor = true;
            // 
            // btnManual
            // 
            this.btnManual.Location = new System.Drawing.Point(4, 69);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(75, 23);
            this.btnManual.TabIndex = 2;
            this.btnManual.Text = "操作手册";
            this.btnManual.UseVisualStyleBackColor = true;
            // 
            // btnSearchOut
            // 
            this.btnSearchOut.Location = new System.Drawing.Point(247, 32);
            this.btnSearchOut.Name = "btnSearchOut";
            this.btnSearchOut.Size = new System.Drawing.Size(75, 23);
            this.btnSearchOut.TabIndex = 2;
            this.btnSearchOut.Text = "外部搜索";
            this.btnSearchOut.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(247, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbxKeywords
            // 
            this.tbxKeywords.Font = new System.Drawing.Font("宋体", 20.5F);
            this.tbxKeywords.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbxKeywords.Location = new System.Drawing.Point(19, 16);
            this.tbxKeywords.Name = "tbxKeywords";
            this.tbxKeywords.Size = new System.Drawing.Size(222, 39);
            this.tbxKeywords.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 317);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "备注";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(3, 332);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(262, 59);
            this.textBox3.TabIndex = 9;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 639);
            this.Controls.Add(this.scmain);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.ResizeEnd += new System.EventHandler(this.FormMain_ResizeEnd);
            this.scmain.Panel1.ResumeLayout(false);
            this.scmain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scmain)).EndInit();
            this.scmain.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pnlOrder.ResumeLayout(false);
            this.pnlOrder.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scmain;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbxChatMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.FlowLayoutPanel pnlCustomerList;
        private System.Windows.Forms.RichTextBox tbxChatLog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSearchOut;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbxKeywords;
        private System.Windows.Forms.Button btnKeywords;
        private System.Windows.Forms.Button btnMap;
        private System.Windows.Forms.Button btnBusiness;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.FlowLayoutPanel pnlResultService;
        private System.Windows.Forms.FlowLayoutPanel pnlChat;
        private System.Windows.Forms.FlowLayoutPanel pnlOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxServiceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxServiceBusinessName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxServiceDescription;
        private System.Windows.Forms.TextBox tbxServiceUnitPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxServiceUrl;
        private System.Windows.Forms.Button btnPushExternalService;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox3;

    }
}