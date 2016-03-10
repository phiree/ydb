using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Dianzhu.Model;

using System.Text.RegularExpressions;

namespace Dianzhu.CSClient.WinformView
{
    public partial class FormLogin : Form, IView.ILoginForm
    {


        public FormLogin()
        {


            InitializeComponent();
            btnLogin.Click += new EventHandler(btnLogin_Click2);

        }


        private void btnLogin_Click2(object sender, EventArgs e)
        {
            bgw.RunWorkerAsync();
         
        }




        //这是一个异步的进程.

        public bool IsLoginSuccess
        {
            set
            {
                this.DialogResult = value ? System.Windows.Forms.DialogResult.OK : System.Windows.Forms.DialogResult.Abort;
                //return;
                //Action lambda = () =>this.DialogResult=value? System.Windows.Forms.DialogResult.OK: System.Windows.Forms.DialogResult.Abort;
                //if (InvokeRequired)
                //    Invoke(lambda);
                //else
                //    lambda();

            }
        }



        public event IView.ViewLogin ViewLogin;
        public string UserName
        {
            get { return tbxUserName.Text; }

        }
        public string Password
        {
            get { return tbxPassword.Text; }
        }
        public string LoginMessage
        {
            set
            {
                Action lambda = () =>
                {
                    lblResult.Text = value;
                };
                if (InvokeRequired) Invoke(lambda);
                else lambda();
            }
        }
        public string ErrorMessage { get; set; }
        public void ShowError()
        {

            Action lambda = () =>
            {
                MessageBox.Show(ErrorMessage);
            };
            if (InvokeRequired) Invoke(lambda);
            else lambda();
        }
        public string LoginButtonText { set {
                Action lambda = () =>
                {
                    btnLogin.Text = value;
                };
                if (InvokeRequired) Invoke(lambda);
                else lambda();
               }
        }
        public bool LoginButtonEnabled
        {
            set
            {

                Action lambda = () =>
                {
                    btnLogin.Enabled = value;
                };
                if (InvokeRequired) Invoke(lambda);
                else lambda();
            }
        }

        public string FormText
        {
            get
            {
                return this.Text;
            }

            set
            {
                this.Text = value;
            }
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            ViewLogin();
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Teboscreen.Form1 form1 = new Teboscreen.Form1();
            form1.InstanceRef = this;
            form1.Show();

        }
    }
}
