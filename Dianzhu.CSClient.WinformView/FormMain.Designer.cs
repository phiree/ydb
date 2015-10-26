﻿namespace Dianzhu.CSClient.WinformView
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
            this.label14 = new System.Windows.Forms.Label();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblOrderStatus = new System.Windows.Forms.Label();
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCreateNewDraft = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.pnlChat = new System.Windows.Forms.FlowLayoutPanel();
            this.tbxChatLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnScreenshot = new System.Windows.Forms.Button();
            this.btnSendAudio = new System.Windows.Forms.Button();
            this.btnSendImage = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbxChatMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlResultService = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxRawXml = new System.Windows.Forms.TextBox();
            this.btnSendRawXml = new System.Windows.Forms.Button();
            this.btnNoticeSystem = new System.Windows.Forms.Button();
            this.btnNoticeCustomerService = new System.Windows.Forms.Button();
            this.btnNoticeOrder = new System.Windows.Forms.Button();
            this.btnNoticePromote = new System.Windows.Forms.Button();
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
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlResultService.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.scmain.Size = new System.Drawing.Size(1161, 647);
            this.scmain.SplitterDistance = 115;
            this.scmain.TabIndex = 0;
            // 
            // pnlCustomerList
            // 
            this.pnlCustomerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCustomerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomerList.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomerList.Name = "pnlCustomerList";
            this.pnlCustomerList.Size = new System.Drawing.Size(1161, 115);
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
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1161, 528);
            this.splitContainer2.SplitterDistance = 342;
            this.splitContainer2.TabIndex = 0;
            // 
            // pnlOrder
            // 
            this.pnlOrder.AutoScroll = true;
            this.pnlOrder.Controls.Add(this.label14);
            this.pnlOrder.Controls.Add(this.lblOrderNumber);
            this.pnlOrder.Controls.Add(this.label15);
            this.pnlOrder.Controls.Add(this.lblOrderStatus);
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
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrder.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlOrder.Location = new System.Drawing.Point(0, 0);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Size = new System.Drawing.Size(340, 430);
            this.pnlOrder.TabIndex = 2;
            this.pnlOrder.WrapContents = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 16;
            this.label14.Text = "订单编号";
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Location = new System.Drawing.Point(3, 12);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(0, 12);
            this.lblOrderNumber.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 16;
            this.label15.Text = "订单状态";
            // 
            // lblOrderStatus
            // 
            this.lblOrderStatus.AutoSize = true;
            this.lblOrderStatus.Location = new System.Drawing.Point(3, 36);
            this.lblOrderStatus.Name = "lblOrderStatus";
            this.lblOrderStatus.Size = new System.Drawing.Size(0, 12);
            this.lblOrderStatus.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "服务名称";
            // 
            // tbxServiceName
            // 
            this.tbxServiceName.Location = new System.Drawing.Point(3, 63);
            this.tbxServiceName.Name = "tbxServiceName";
            this.tbxServiceName.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceName.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 87);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "商家名称";
            // 
            // tbxServiceBusinessName
            // 
            this.tbxServiceBusinessName.Location = new System.Drawing.Point(3, 102);
            this.tbxServiceBusinessName.Name = "tbxServiceBusinessName";
            this.tbxServiceBusinessName.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceBusinessName.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 126);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 16;
            this.label12.Text = "服务介绍";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // tbxServiceDescription
            // 
            this.tbxServiceDescription.Location = new System.Drawing.Point(3, 141);
            this.tbxServiceDescription.Name = "tbxServiceDescription";
            this.tbxServiceDescription.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceDescription.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 165);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 16;
            this.label13.Text = "单价";
            this.label13.Click += new System.EventHandler(this.label12_Click);
            // 
            // tbxServiceUnitPrice
            // 
            this.tbxServiceUnitPrice.Location = new System.Drawing.Point(3, 180);
            this.tbxServiceUnitPrice.Name = "tbxServiceUnitPrice";
            this.tbxServiceUnitPrice.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceUnitPrice.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "服务时间";
            // 
            // tbxServiceTime
            // 
            this.tbxServiceTime.Location = new System.Drawing.Point(3, 219);
            this.tbxServiceTime.Name = "tbxServiceTime";
            this.tbxServiceTime.Size = new System.Drawing.Size(262, 21);
            this.tbxServiceTime.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 243);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "服务地点";
            // 
            // tbxTargetAddress
            // 
            this.tbxTargetAddress.Location = new System.Drawing.Point(3, 258);
            this.tbxTargetAddress.Name = "tbxTargetAddress";
            this.tbxTargetAddress.Size = new System.Drawing.Size(262, 21);
            this.tbxTargetAddress.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "总价";
            // 
            // tbxAmount
            // 
            this.tbxAmount.Location = new System.Drawing.Point(3, 297);
            this.tbxAmount.Name = "tbxAmount";
            this.tbxAmount.Size = new System.Drawing.Size(262, 21);
            this.tbxAmount.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 321);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "备注";
            // 
            // tbxMemo
            // 
            this.tbxMemo.Location = new System.Drawing.Point(3, 336);
            this.tbxMemo.Multiline = true;
            this.tbxMemo.Name = "tbxMemo";
            this.tbxMemo.Size = new System.Drawing.Size(262, 59);
            this.tbxMemo.TabIndex = 9;
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Location = new System.Drawing.Point(3, 401);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(155, 23);
            this.btnCreateOrder.TabIndex = 7;
            this.btnCreateOrder.Text = "生成订单";
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCreateNewDraft);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 430);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(340, 96);
            this.panel3.TabIndex = 3;
            // 
            // btnCreateNewDraft
            // 
            this.btnCreateNewDraft.Enabled = false;
            this.btnCreateNewDraft.Location = new System.Drawing.Point(5, 6);
            this.btnCreateNewDraft.Name = "btnCreateNewDraft";
            this.btnCreateNewDraft.Size = new System.Drawing.Size(155, 23);
            this.btnCreateNewDraft.TabIndex = 18;
            this.btnCreateNewDraft.Text = "增加子订单";
            this.btnCreateNewDraft.UseVisualStyleBackColor = true;
            this.btnCreateNewDraft.Click += new System.EventHandler(this.btnCreateNewDraft_Click);
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
            this.splitContainer3.Size = new System.Drawing.Size(815, 528);
            this.splitContainer3.SplitterDistance = 407;
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
            this.pnlChat.Size = new System.Drawing.Size(405, 461);
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
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 461);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 65);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnScreenshot);
            this.panel4.Controls.Add(this.btnSendAudio);
            this.panel4.Controls.Add(this.btnSendImage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(405, 36);
            this.panel4.TabIndex = 0;
            // 
            // btnScreenshot
            // 
            this.btnScreenshot.Location = new System.Drawing.Point(192, 9);
            this.btnScreenshot.Name = "btnScreenshot";
            this.btnScreenshot.Size = new System.Drawing.Size(75, 23);
            this.btnScreenshot.TabIndex = 4;
            this.btnScreenshot.Text = "截图";
            this.btnScreenshot.UseVisualStyleBackColor = true;
            this.btnScreenshot.Click += new System.EventHandler(this.btnScreenshot_Click);
            // 
            // btnSendAudio
            // 
            this.btnSendAudio.Location = new System.Drawing.Point(99, 9);
            this.btnSendAudio.Name = "btnSendAudio";
            this.btnSendAudio.Size = new System.Drawing.Size(75, 23);
            this.btnSendAudio.TabIndex = 3;
            this.btnSendAudio.Text = "语音";
            this.btnSendAudio.UseVisualStyleBackColor = true;
            this.btnSendAudio.Click += new System.EventHandler(this.btnSendAudio_Click);
            // 
            // btnSendImage
            // 
            this.btnSendImage.Location = new System.Drawing.Point(0, 9);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(75, 23);
            this.btnSendImage.TabIndex = 2;
            this.btnSendImage.Text = "图片";
            this.btnSendImage.UseVisualStyleBackColor = true;
            this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tbxChatMsg);
            this.panel5.Controls.Add(this.btnSend);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(405, 29);
            this.panel5.TabIndex = 2;
            // 
            // tbxChatMsg
            // 
            this.tbxChatMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxChatMsg.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbxChatMsg.Location = new System.Drawing.Point(0, 0);
            this.tbxChatMsg.Name = "tbxChatMsg";
            this.tbxChatMsg.Size = new System.Drawing.Size(330, 21);
            this.tbxChatMsg.TabIndex = 0;
            this.tbxChatMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxChatMsg_KeyPress);
            // 
            // btnSend
            // 
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Location = new System.Drawing.Point(330, 0);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 29);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pnlResultService
            // 
            this.pnlResultService.AutoScroll = true;
            this.pnlResultService.Controls.Add(this.groupBox1);
            this.pnlResultService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResultService.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlResultService.Location = new System.Drawing.Point(0, 104);
            this.pnlResultService.Name = "pnlResultService";
            this.pnlResultService.Size = new System.Drawing.Size(402, 422);
            this.pnlResultService.TabIndex = 1;
            this.pnlResultService.WrapContents = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxRawXml);
            this.groupBox1.Controls.Add(this.btnSendRawXml);
            this.groupBox1.Controls.Add(this.btnNoticeSystem);
            this.groupBox1.Controls.Add(this.btnNoticeCustomerService);
            this.groupBox1.Controls.Add(this.btnNoticeOrder);
            this.groupBox1.Controls.Add(this.btnNoticePromote);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 342);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试按钮";
            // 
            // tbxRawXml
            // 
            this.tbxRawXml.Location = new System.Drawing.Point(16, 189);
            this.tbxRawXml.Multiline = true;
            this.tbxRawXml.Name = "tbxRawXml";
            this.tbxRawXml.Size = new System.Drawing.Size(303, 114);
            this.tbxRawXml.TabIndex = 20;
            this.tbxRawXml.Visible = false;
            // 
            // btnSendRawXml
            // 
            this.btnSendRawXml.Location = new System.Drawing.Point(16, 164);
            this.btnSendRawXml.Name = "btnSendRawXml";
            this.btnSendRawXml.Size = new System.Drawing.Size(166, 23);
            this.btnSendRawXml.TabIndex = 19;
            this.btnSendRawXml.Text = "SendRawXML";
            this.btnSendRawXml.UseVisualStyleBackColor = true;
            this.btnSendRawXml.Visible = false;
            this.btnSendRawXml.Click += new System.EventHandler(this.btnSendRawXml_Click);
            // 
            // btnNoticeSystem
            // 
            this.btnNoticeSystem.Location = new System.Drawing.Point(16, 47);
            this.btnNoticeSystem.Name = "btnNoticeSystem";
            this.btnNoticeSystem.Size = new System.Drawing.Size(166, 23);
            this.btnNoticeSystem.TabIndex = 18;
            this.btnNoticeSystem.Text = "系统通知";
            this.btnNoticeSystem.UseVisualStyleBackColor = true;
            this.btnNoticeSystem.Click += new System.EventHandler(this.btnNoticeSystem_Click);
            // 
            // btnNoticeCustomerService
            // 
            this.btnNoticeCustomerService.Location = new System.Drawing.Point(16, 134);
            this.btnNoticeCustomerService.Name = "btnNoticeCustomerService";
            this.btnNoticeCustomerService.Size = new System.Drawing.Size(166, 23);
            this.btnNoticeCustomerService.TabIndex = 18;
            this.btnNoticeCustomerService.Text = "客服通知";
            this.btnNoticeCustomerService.UseVisualStyleBackColor = true;
            this.btnNoticeCustomerService.Click += new System.EventHandler(this.btnNoticeCustomerService_Click);
            // 
            // btnNoticeOrder
            // 
            this.btnNoticeOrder.Location = new System.Drawing.Point(16, 76);
            this.btnNoticeOrder.Name = "btnNoticeOrder";
            this.btnNoticeOrder.Size = new System.Drawing.Size(166, 23);
            this.btnNoticeOrder.TabIndex = 18;
            this.btnNoticeOrder.Text = "订单通知";
            this.btnNoticeOrder.UseVisualStyleBackColor = true;
            this.btnNoticeOrder.Click += new System.EventHandler(this.btnNoticeOrder_Click);
            // 
            // btnNoticePromote
            // 
            this.btnNoticePromote.Location = new System.Drawing.Point(16, 105);
            this.btnNoticePromote.Name = "btnNoticePromote";
            this.btnNoticePromote.Size = new System.Drawing.Size(166, 23);
            this.btnNoticePromote.TabIndex = 18;
            this.btnNoticePromote.Text = "推广通知";
            this.btnNoticePromote.UseVisualStyleBackColor = true;
            this.btnNoticePromote.Click += new System.EventHandler(this.btnNoticePromote_Click);
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
            this.panel2.Size = new System.Drawing.Size(402, 104);
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
            this.ClientSize = new System.Drawing.Size(1161, 647);
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
            this.panel3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlResultService.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Button btnSendImage;
        private System.Windows.Forms.OpenFileDialog dlgSelectPic;
        private System.Windows.Forms.Button btnSendAudio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblOrderStatus;
        private System.Windows.Forms.Label lblOrderNumber;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCreateNewDraft;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnScreenshot;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNoticeSystem;
        private System.Windows.Forms.Button btnNoticeCustomerService;
        private System.Windows.Forms.Button btnNoticeOrder;
        private System.Windows.Forms.Button btnNoticePromote;
        private System.Windows.Forms.TextBox tbxRawXml;
        private System.Windows.Forms.Button btnSendRawXml;
    }
}