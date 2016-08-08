using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using clsMCIPlay;
using System.Diagnostics;
using System.Threading;

namespace Music
{
    public partial class Music : Form
    {
        clsMCI mc = new clsMCI();
        Dianzhu.ApplicationService.McisendstringAPI mc1 = new Dianzhu.ApplicationService.McisendstringAPI();
        int t = 1;
        int s = 0;
        int sum = 0;//歌曲数目
        int nowID = 0;
        int  PlayModel = 0;
        int oldvoices = 0;
        bool end = false;
        Music mainH;
        List<string> listName = new List<string>();
        Thread th;
        Thread MonitorTh;
        public Music()
        {
            InitializeComponent();
            
            Init();
            th = new Thread(new ThreadStart(Mathod));
            MonitorTh = new Thread(new ThreadStart(MonTh));
            MonitorTh.Start();
            sum =listBox1.Items.Count;
            CheckForIllegalCrossThreadCalls = false;
            
        }

        /// <summary>
        /// 播放器初始化函数
        /// </summary>
        private void Init()
        {
            if (!File.Exists("F:\\2345下载\\code\\Music\\Resource.txt"))//如果日志文件不存在
            {
                File.CreateText("F:\\2345下载\\code\\Music\\Resource.txt");//创建文本文件
            }
            StreamReader SR = new StreamReader("F:\\2345下载\\code\\Music\\Resource.txt");
             while (SR.Peek()>0)
             {
               string str = SR.ReadLine();//读取信息
               listName.Add(str);//读取信息
               string[] s = str.Split(new char[] { '\\' });//获取歌曲名
               listBox1.Items.Add(s[s.Length - 1]);
             }
            SR.Close();
            oldvoices = trackBar1.Value;
            label1.Parent = this;
            label6.Text = "顺序";
        }

        /// <summary>
        /// 音乐播放函数
        /// </summary>
        /// <param name="name"></param>
        private void PlayStart(string name,IntPtr Handle)
        {
            try
            {
                //mc.CloseMusic();
                //bool OK = mc.OpenMusic(name, Handle);
                //if (!OK)
                //{
                //    mc.CloseMusic();
                //    MessageBox.Show("Sorry~该歌曲无法播放");
                //    return;
                //}

                //mc.play();
                //mc.SetVolumeTo(trackBar1.Value);

                //int id = listBox1.SelectedIndex;
                //label1.Text = listBox1.Items[id].ToString();
                //t = mc.GetMusicLength();
                //label3.Text = IntToTimeS(t);
                //s = 0;
                //label2.Text = IntToTimeS(s);

                //progressBar1.Value = 0;
                //progressBar1.Maximum = t;
                //progressBar1.Minimum = s;

                //************************************************
                mc1.CloseMusic();
                bool OK = mc1.OpenMusic(name, Handle);
                if (!OK)
                {
                    mc1.CloseMusic();
                    MessageBox.Show("Sorry~该歌曲无法播放");
                    return;
                }

                mc1.PlayMusic();

                int id = listBox1.SelectedIndex;
                label1.Text = listBox1.Items[id].ToString();
                t = mc1.GetMusicLength();
                label3.Text = IntToTimeS(t);
                s = 0;
                label2.Text = IntToTimeS(s);

                progressBar1.Value = 0;
                progressBar1.Maximum = t;
                progressBar1.Minimum = s;


            }
            catch
            {
                Error();
                MessageBox.Show("歌曲位置改变或已删除");    
            }
        }

        /// <summary>
        /// 获取歌曲名
        /// </summary>
        /// <returns></returns>
        private string MusicName()
        {
            nowID = listBox1.SelectedIndex;
            if (nowID < 0)
            {
                return null;
            }
            return listName[nowID];
        }

        private void StartTH()
        {
            if (th.ThreadState.ToString() != "Unstarted")
            {
                if (th.ThreadState.ToString() == "Suspended")      //先检查线程的状态 
                {
                    th.Resume();                            // 唤起 
                    th.Abort();                               //杀死线程 
                    th.Join();                                  //这里是保证彻底的杀死线程
                }
                else
                {
                    th.Abort();
                    th.Join();
                }
                th = new Thread(new ThreadStart(Mathod));//重新建立一个线程
            }
            th.Start();
        }
        /// <summary>
        /// 上一首
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonprev_Click(object sender, EventArgs e)
        {
            int id = listBox1.SelectedIndex;
            if (id < 0)
            {
                return;
            }
            if (id-1>=0)//如果前面还有
            {
                id--;
            }
            else
            {
                id = sum-1;
            }
            listBox1.SelectedIndex = id;
            StartTH();
        }
        

        private void button2s_Click(object sender, EventArgs e)
        {  
            int id = listBox1.SelectedIndex;
            if (id < 0)
            {
                return;
            }
            if (mc.GetState == 3)//如果没有歌曲播放
            {
                string musicname = MusicName();
                StartTH();
                button2s.Text = "暂停";
            }
            else if (mc.GetState == 2)//如果歌曲状态是暂停
            {
                mc.ResumeMusic();
                th.Resume();
                button2s.Text = "暂停";
            }
            else if (mc.GetState == 1)//如果歌曲状态正在播放
            {
                mc.PauseMusic();
                th.Suspend();
                button2s.Text = "开始";
            }
        }
        private void MonTh()
        {         
            while (true)
            {
                if (end)
                {
                    StartTH();
                    end = false;
                }
            }
        }
        /// <summary>
        /// 线程事件
        /// </summary>
        private void Mathod()
        {
           string musicname = MusicName();
            this.Invoke(new MethodInvoker(delegate
                {
                    PlayStart(musicname, mainH.Handle);
                }));
           
            while (s < t)
            {   
                Thread.Sleep(1000);
                s++;
                progressBar1.Value = s;
                label2.Text = IntToTimeS(s);
            }
            if (s >= t)
            {
                end = true;
                s = 0;
                t = 1;
                int id = 0;
                switch (PlayModel)
                {
                    case 0://循环播放
                        {
                            id = listBox1.SelectedIndex;
                            if (id < sum - 1)//如果后面还有
                            {
                                id++;
                            }
                            else
                            {
                                id = 0;
                            }
                            listBox1.SelectedIndex = id;
                        }; break;
                    case 1://单曲循环
                        {
                            id = listBox1.SelectedIndex;
                        }; break;
                    case 2://随机播放
                        {
                            Random rm = new Random();
                            id = rm.Next(0,sum-1);
                            listBox1.SelectedIndex = id;
                        }; break;
                    default: MessageBox.Show("出错了！"); break;
                }
            }
        }

        /// <summary>
        /// 下一首
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonlater_Click(object sender, EventArgs e)
        {
            int id = listBox1.SelectedIndex;
            if (id < 0)
            {
                return;
            }
            if (id < sum - 1)//如果后面还有
            {
                id++;
            }
            else
            {
                id = 0;
            }
            listBox1.SelectedIndex = id;
            string musicname = MusicName();
            StartTH();
        }

        /// <summary>
        /// 添加歌曲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonadd_Click(object sender, EventArgs e)
        {
            //文件对话框的创建
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = "D:\\";//注意这里写路径时要用c:\\而不是c:\
            file.Filter = "MP3(*.mp3),WMA(*.wma)|*.mp3;*.wma|所有文件|*.*";
            file.RestoreDirectory = true;
            file.FilterIndex = 1;
            string fName = "";
            //文本文件操作
            StreamWriter SW;
            
            if (file.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists("F:\\2345下载\\code\\Music\\Resource.txt"))//如果日志文件不存在
                {
                    SW = File.CreateText("F:\\2345下载\\code\\Music\\Resource.txt");//创建文本文件
                }
                fName = file.SafeFileName;
                listBox1.Items.Add(fName);
                sum++;
                SW = File.AppendText("F:\\2345下载\\code\\Music\\Resource.txt");
                SW.WriteLine(file.FileName);//将信息存储到日志文件中
                listName.Add(file.FileName);
                SW.Close();
            }
            
        }

        /// <summary>
        /// 双击播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            StartTH();
            button2s.Text = "暂停";
            
        }

        /// <summary>
        /// 删除歌曲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttondele_Click(object sender, EventArgs e)
        {
            List<string> Dlist = new List<string>();
            int id = listBox1.SelectedIndex;//获取选中歌曲列表序号
            listBox1.Items.RemoveAt(id);
            if (id == nowID)//如果删除的是正在播放的歌曲
            {
                if (sum != 1)
                {
                    if (id < sum - 1)//如果后面还有
                    {
                    }
                    else
                    {
                        id = 0;
                    }
                    listBox1.SelectedIndex = id;
                    string musicname = MusicName();
                    StartTH();
                    listBox1.SelectedIndex = nowID;
                }
                else
                {
                    Error();
                }
            }
            else
            {
                if (id < nowID)
                {
                    nowID--;
                }
                listBox1.SelectedIndex = nowID;
            }
            StreamReader SR = new StreamReader("F:\\2345下载\\code\\Music\\Resource.txt");
            while (SR.Peek() > 0)
            {
                Dlist.Add(SR.ReadLine());//读取信息
            }
            SR.Close();
            Dlist.RemoveAt(id);
            listName.RemoveAt(id);
            StreamWriter SW = new StreamWriter("F:\\2345下载\\code\\Music\\Resource.txt");
            for (int i = 0; i < Dlist.Count; i++)
            {
                SW.WriteLine(Dlist[i]);
            }
            SW.Close();
            sum--;
        }

        /// <summary>
        /// 转换时间格式函数
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private string IntToTimeS(int t)
        {
            string Str = "";
            int s = t % 60;//秒数
            int m = t / 60;//分钟
            if (m< 10)
            {
                Str = "0" + m+":";
                if (s < 10)
                {
                    Str = Str + "0" + s;
                }
                else
                {
                    Str = Str + s;
                }
            }
            else
            {
                Str = "" + m+":";
                if (s< 10)
                {
                    Str = Str + "0" + s;
                }
                else
                {
                    Str = Str + s;
                }
            }
            return Str;
        }

        /// <summary>
        /// 单曲循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)//单曲循环
        {
            PlayModel = 1;
            label6.Text = "单曲";
        }

        /// <summary>
        /// 多曲循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)//多曲循环
        {
            PlayModel = 0;
            label6.Text = "顺序";
        }

        /// <summary>
        /// 程序关闭，线程销毁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Music_FormClosing(object sender, FormClosingEventArgs e)
        {
          System.Environment.Exit(0);
        }

        /// <summary>
        /// 随机播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            PlayModel = 2;
            label6.Text = "随机";
        }

        /// <summary>
        /// 静音操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)//如果选中
            {
                mc.SetVolumeTo(0);
                trackBar1.Value = 0;
                label5.Text = "0%";
                checkBox1.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                trackBar1.Value = oldvoices;
                label5.Text = oldvoices + "%";
                mc.SetVolumeTo(oldvoices);
                checkBox1.ForeColor = System.Drawing.Color.Black;
            }
        }

        /// <summary>
        /// 声音操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            oldvoices = trackBar1.Value;
            label5.Text = oldvoices + "%";
            mc.SetVolumeTo(oldvoices);
            if (oldvoices == 0)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

        }
        /// <summary>
        /// 获取句柄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Music_Load(object sender, EventArgs e)
        {
            mainH = this;
        }
        /// <summary>
        /// 清空列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            File.WriteAllText("F:\\2345下载\\code\\Music\\Resource.txt", string.Empty);
            sum=0;
            Error();
            
        }
        private void Error()
        {
            mc.CloseMusic();
            if (th.ThreadState.ToString() != "Unstarted")
            {
                if (th.ThreadState.ToString() == "Suspended")      //先检查线程的状态 
                {
                    th.Resume();                            // 唤起 
                    th.Abort();                               //杀死线程 
                    th.Join();                                  //这里是保证彻底的杀死线程
                }
                else
                {
                    th.Abort();
                    th.Join();
                }
            }
            s = 0;
            t = 0;
            label3.Text = IntToTimeS(t);
            label2.Text = IntToTimeS(s);
            label1.Text = "";
            progressBar1.Value = 0;
            button2s.Text = "开始";
        }
        /// <summary>
        /// 作品关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            about formabout = new about();
            formabout.Show();
        }
    }
}
