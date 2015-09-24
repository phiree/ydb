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
            this.btnOrderList = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxServiceName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxServiceBusinessName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxServiceDescription = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbxServiceUnitPrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxServiceTime = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxTargetAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxAmount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxMemo = new System.Windows.Forms.TextBox();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.pnlChat = new System.Windows.Forms.FlowLayoutPanel();
            this.tbxChatLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSendImage = new System.Windows.Forms.Button();
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
            this.tbxServiceUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnPushExternalService = new System.Windows.Forms.Button();
            this.pnlExternService = new System.Windows.Forms.FlowLayoutPanel();
            this.dlgSelectPic = new System.Windows.Forms.OpenFileDialog();
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
            this.pnlExternService.SuspendLayout();
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
            this.scmain.Size = new System.Drawing.Size(893, 639);
            this.scmain.SplitterDistance = 114;
            this.scmain.TabIndex = 0;
            // 
            // pnlCustomerList
            // 
            this.pnlCustomerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomerList.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomerList.Name = "pnlCustomerList";
            this.pnlCustomerList.Size = new System.Drawing.Size(893, 114);
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
            this.splitContainer2.Panel1.Controls.Add(this.btnLogout);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(893, 521);
            this.splitContainer2.SplitterDistance = 292;
            this.splitContainer2.TabIndex = 0;
            // 
            // pnlOrder
            // 
            this.pnlOrder.AutoScroll = true;
            this.pnlOrder.Controls.Add(this.btnOrderList);
            this.pnlOrder.Controls.Add(this.label10);
            this.pnlOrder.Controls.Add(this.tbxServiceName);
            this.pnlOrder.Controls.Add(this.label11);
            this.pnlOrder.Controls.Add(this.tbxServiceBusinessName);
            this.pnlOrder.Controls.Add(this.label12);
            this.pnlOrder.Controls.Add(this.tbxServiceDescription);
            this.pnlOrder.Controls.Add(this.label13);
            this.pnlOrder.Controls.Add(this.tbxServiceUnitPrice);
            this.pnlOrder.Controls.Add(this.label6);
            this.pnlOrder.Controls.Add(this.tbxServiceTime);
            this.pnlOrder.Controls.Add(this.label9);
            this.pnlOrder.Controls.Add(this.tbxTargetAddress);
            this.pnlOrder.Controls.Add(this.label7);
            this.pnlOrder.Controls.Add(this.tbxAmount);
            this.pnlOrder.Controls.Add(this.label8);
            this.pnlOrder.Controls.Add(this.tbxMemo);
            this.pnlOrder.Controls.Add(this.btnCreateOrder);
            this.pnlOrder.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlOrder.Location = new System.Drawing.Point(3, 13);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new System.Drawing.Size(280, 429);
            this.pnlOrder.TabIndex = 2;
            this.pnlOrder.WrapContents = false;
            // 
            // btnOrderList
            // 
            this.btnOrderList.Location = new System.Drawing.Point(3, 3);
            this.btnOrderList.Name = "btnOrderList";
            this.btnOrderList.Size = new System.Drawing.Size(100, 23);
            this.btnOrderList.TabIndex = 8;
            this.btnOrderList.Text = "查看订单列表";
            this.btnOrderList.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "服务名称";
            // 
            // tbxServiceName
            // 
            this.tbxServiceName.Location = new System.Drawing.Point(3, 44);
            this.tbxServiceName.Name = "tbxServiceName";
            this.tbxServiceName.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceName.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "商家名称";
            // 
            // tbxServiceBusinessName
            // 
            this.tbxServiceBusinessName.Location = new System.Drawing.Point(3, 83);
            this.tbxServiceBusinessName.Name = "tbxServiceBusinessName";
            this.tbxServiceBusinessName.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceBusinessName.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 107);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 16;
            this.label12.Text = "服务介绍";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // tbxServiceDescription
            // 
            this.tbxServiceDescription.Location = new System.Drawing.Point(3, 122);
            this.tbxServiceDescription.Name = "tbxServiceDescription";
            this.tbxServiceDescription.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceDescription.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 146);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 16;
            this.label13.Text = "单价";
            this.label13.Click += new System.EventHandler(this.label12_Click);
            // 
            // tbxServiceUnitPrice
            // 
            this.tbxServiceUnitPrice.Location = new System.Drawing.Point(3, 161);
            this.tbxServiceUnitPrice.Name = "tbxServiceUnitPrice";
            this.tbxServiceUnitPrice.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceUnitPrice.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "服务时间";
            // 
            // tbxServiceTime
            // 
            this.tbxServiceTime.Location = new System.Drawing.Point(3, 200);
            this.tbxServiceTime.Name = "tbxServiceTime";
            this.tbxServiceTime.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceTime.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "服务地点";
            // 
            // tbxTargetAddress
            // 
            this.tbxTargetAddress.Location = new System.Drawing.Point(3, 239);
            this.tbxTargetAddress.Name = "tbxTargetAddress";
            this.tbxTargetAddress.Size = new System.Drawing.Size(262, 21);
            this.tbxTargetAddress.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "总价";
            // 
            // tbxAmount
            // 
            this.tbxAmount.Location = new System.Drawing.Point(3, 278);
            this.tbxAmount.Name = "tbxAmount";
            this.tbxAmount.Size = new System.Drawing.Size(262, 21);
            this.tbxAmount.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 302);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "备注";
            // 
            // tbxMemo
            // 
            this.tbxMemo.Location = new System.Drawing.Point(3, 317);
            this.tbxMemo.Multiline = true;
            this.tbxMemo.Name = "tbxMemo";
            this.tbxMemo.Size = new System.Drawing.Size(262, 59);
            this.tbxMemo.TabIndex = 9;
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Location = new System.Drawing.Point(3, 382);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(155, 23);
            this.btnCreateOrder.TabIndex = 7;
            this.btnCreateOrder.Text = "创建订单,发送支付链接";
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(-1, 494);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(155, 23);
            this.btnLogout.TabIndex = 17;
            this.btnLogout.Text = "退出";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
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
            this.splitContainer3.Size = new System.Drawing.Size(597, 521);
            this.splitContainer3.SplitterDistance = 294;
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
            this.pnlChat.Size = new System.Drawing.Size(292, 496);
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
            this.panel1.Controls.Add(this.btnSendImage);
            this.panel1.Controls.Add(this.tbxChatMsg);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 496);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 23);
            this.panel1.TabIndex = 1;
            // 
            // btnSendImage
            // 
            this.btnSendImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendImage.Location = new System.Drawing.Point(142, 0);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(75, 23);
            this.btnSendImage.TabIndex = 2;
            this.btnSendImage.Text = "图片";
            this.btnSendImage.UseVisualStyleBackColor = true;
            this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
            // 
            // tbxChatMsg
            // 
            this.tbxChatMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxChatMsg.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbxChatMsg.Location = new System.Drawing.Point(0, 0);
            this.tbxChatMsg.Name = "tbxChatMsg";
            this.tbxChatMsg.Size = new System.Drawing.Size(217, 21);
            this.tbxChatMsg.TabIndex = 0;
            this.tbxChatMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxChatMsg_KeyPress);
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Location = new System.Drawing.Point(217, 0);
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
            this.panel2.Size = new System.Drawing.Size(297, 104);
            this.panel2.TabIndex = 0;
            // 
            // btnKeywords
            // 
            this.btnKeywords.Enabled = false;
            this.btnKeywords.Location = new System.Drawing.Point(85, 69);
            this.btnKeywords.Name = "btnKeywords";
            this.btnKeywords.Size = new System.Drawing.Size(75, 23);
            this.btnKeywords.TabIndex = 2;
            this.btnKeywords.Text = "关键字";
            this.btnKeywords.UseVisualStyleBackColor = true;
            // 
            // btnMap
            // 
            this.btnMap.Enabled = false;
            this.btnMap.Location = new System.Drawing.Point(247, 69);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(75, 23);
            this.btnMap.TabIndex = 2;
            this.btnMap.Text = "地图";
            this.btnMap.UseVisualStyleBackColor = true;
            // 
            // btnBusiness
            // 
            this.btnBusiness.Enabled = false;
            this.btnBusiness.Location = new System.Drawing.Point(166, 69);
            this.btnBusiness.Name = "btnBusiness";
            this.btnBusiness.Size = new System.Drawing.Size(75, 23);
            this.btnBusiness.TabIndex = 2;
            this.btnBusiness.Text = "服务商";
            this.btnBusiness.UseVisualStyleBackColor = true;
            // 
            // btnManual
            // 
            this.btnManual.Enabled = false;
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
            this.btnSearchOut.Click += new System.EventHandler(this.btnSearchOut_Click);
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
            this.tbxKeywords.Font = new System.Drawing.Font("SimSun", 20.5F);
            this.tbxKeywords.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbxKeywords.Location = new System.Drawing.Point(19, 16);
            this.tbxKeywords.Name = "tbxKeywords";
            this.tbxKeywords.Size = new System.Drawing.Size(222, 39);
            this.tbxKeywords.TabIndex = 0;
            // 
            // tbxServiceUrl
            // 
            this.tbxServiceUrl.Location = new System.Drawing.Point(3, 63);
            this.tbxServiceUrl.Name = "tbxServiceUrl";
            this.tbxServiceUrl.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceUrl.TabIndex = 0;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "商家名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "服务描述";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "价格";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "服务网址";
            // 
            // btnPushExternalService
            // 
            this.btnPushExternalService.Location = new System.Drawing.Point(3, 90);
            this.btnPushExternalService.Name = "btnPushExternalService";
            this.btnPushExternalService.Size = new System.Drawing.Size(75, 23);
            this.btnPushExternalService.TabIndex = 2;
            this.btnPushExternalService.Text = "推送";
            this.btnPushExternalService.UseVisualStyleBackColor = true;
            this.btnPushExternalService.Visible = false;
            this.btnPushExternalService.Click += new System.EventHandler(this.btnPushExternalService_Click);
            // 
            // pnlExternService
            // 
            this.pnlExternService.AutoScroll = true;
            this.pnlExternService.Controls.Add(this.label1);
            this.pnlExternService.Controls.Add(this.label2);
            this.pnlExternService.Controls.Add(this.label3);
            this.pnlExternService.Controls.Add(this.label4);
            this.pnlExternService.Controls.Add(this.label5);
            this.pnlExternService.Controls.Add(this.tbxServiceUrl);
            this.pnlExternService.Controls.Add(this.btnPushExternalService);
            this.pnlExternService.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlExternService.Location = new System.Drawing.Point(4, 110);
            this.pnlExternService.Name = "pnlExternService";
            this.pnlExternService.Size = new System.Drawing.Size(333, 242);
            this.pnlExternService.TabIndex = 2;
            this.pnlExternService.WrapContents = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 639);
            this.Controls.Add(this.scmain);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
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
            this.pnlExternService.ResumeLayout(false);
            this.pnlExternService.PerformLayout();
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
        private System.Windows.Forms.FlowLayoutPanel pnlExternService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxServiceUrl;
        private System.Windows.Forms.Button btnPushExternalService;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxServiceTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxAmount;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Button btnOrderList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxMemo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxTargetAddress;
        private System.Windows.Forms.TextBox tbxServiceName;
        private System.Windows.Forms.TextBox tbxServiceBusinessName;
        private System.Windows.Forms.TextBox tbxServiceDescription;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxServiceUnitPrice;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnSendImage;
        private System.Windows.Forms.OpenFileDialog dlgSelectPic;
    }
}