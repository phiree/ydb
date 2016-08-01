namespace Music
{
    partial class Music
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Music));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonprev = new System.Windows.Forms.Button();
            this.button2s = new System.Windows.Forms.Button();
            this.buttonlater = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonadd = new System.Windows.Forms.Button();
            this.buttondele = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button4 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // buttonprev
            // 
            this.buttonprev.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonprev.Location = new System.Drawing.Point(21, 70);
            this.buttonprev.Name = "buttonprev";
            this.buttonprev.Size = new System.Drawing.Size(52, 22);
            this.buttonprev.TabIndex = 2;
            this.buttonprev.Text = "上一首";
            this.buttonprev.UseVisualStyleBackColor = false;
            this.buttonprev.Click += new System.EventHandler(this.buttonprev_Click);
            // 
            // button2s
            // 
            this.button2s.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button2s.Location = new System.Drawing.Point(77, 70);
            this.button2s.Name = "button2s";
            this.button2s.Size = new System.Drawing.Size(40, 23);
            this.button2s.TabIndex = 2;
            this.button2s.Text = "开始";
            this.button2s.UseVisualStyleBackColor = false;
            this.button2s.Click += new System.EventHandler(this.button2s_Click);
            // 
            // buttonlater
            // 
            this.buttonlater.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonlater.Location = new System.Drawing.Point(122, 70);
            this.buttonlater.Name = "buttonlater";
            this.buttonlater.Size = new System.Drawing.Size(49, 23);
            this.buttonlater.TabIndex = 2;
            this.buttonlater.Text = "下一首";
            this.buttonlater.UseVisualStyleBackColor = false;
            this.buttonlater.Click += new System.EventHandler(this.buttonlater_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.listBox1.Font = new System.Drawing.Font("SimSun", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.ForeColor = System.Drawing.Color.DarkGreen;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(22, 113);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(206, 334);
            this.listBox1.TabIndex = 3;
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Lime;
            this.progressBar1.Location = new System.Drawing.Point(14, 35);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(231, 10);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "00:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(212, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "00:00";
            // 
            // buttonadd
            // 
            this.buttonadd.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonadd.Location = new System.Drawing.Point(237, 119);
            this.buttonadd.Name = "buttonadd";
            this.buttonadd.Size = new System.Drawing.Size(61, 23);
            this.buttonadd.TabIndex = 5;
            this.buttonadd.Text = "添加";
            this.buttonadd.UseVisualStyleBackColor = false;
            this.buttonadd.Click += new System.EventHandler(this.buttonadd_Click);
            // 
            // buttondele
            // 
            this.buttondele.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttondele.Location = new System.Drawing.Point(237, 177);
            this.buttondele.Name = "buttondele";
            this.buttondele.Size = new System.Drawing.Size(61, 23);
            this.buttondele.TabIndex = 6;
            this.buttondele.Text = "删除";
            this.buttondele.UseVisualStyleBackColor = false;
            this.buttondele.Click += new System.EventHandler(this.buttondele_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.Location = new System.Drawing.Point(237, 298);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "顺序";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button2.Location = new System.Drawing.Point(237, 359);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "单曲";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button3.Location = new System.Drawing.Point(237, 418);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(61, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "随机";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(178, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "声音";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(267, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "50%";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(248, 33);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "静音";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.trackBar1.Location = new System.Drawing.Point(171, 83);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(120, 20);
            this.trackBar1.TabIndex = 14;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 50;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button4.Location = new System.Drawing.Point(237, 239);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(61, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "清空";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(114, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "label6";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.ForeColor = System.Drawing.Color.Transparent;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.linkLabel1.Location = new System.Drawing.Point(260, 451);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 12);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "关于";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Lime;
            this.label7.Location = new System.Drawing.Point(114, 451);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "倾听雨的声音";
            // 
            // Music
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Music.Properties.Resources._8879_12010121463710;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(304, 468);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttondele);
            this.Controls.Add(this.buttonadd);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonlater);
            this.Controls.Add(this.button2s);
            this.Controls.Add(this.buttonprev);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Music";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "雨の音";
            this.Load += new System.EventHandler(this.Music_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Music_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonprev;
        private System.Windows.Forms.Button button2s;
        private System.Windows.Forms.Button buttonlater;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonadd;
        private System.Windows.Forms.Button buttondele;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label7;
    }
}

