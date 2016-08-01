using System;

using System.Runtime.InteropServices;

using System.Text;

using System.IO;
using System.Windows.Forms;



namespace clsMCIPlay
{

    /// 

    /// clsMci 的摘要说明。

    /// 

    public class clsMCI
    {
        public clsMCI()
        {

            //

            // TODO: 在此处添加构造函数逻辑

            //

        }



        //定义API函数使用的字符串变量
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        private string ShortPathName = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        private string durLength = "";
        [MarshalAs(UnmanagedType.LPTStr, SizeConst = 128)]
        private string TemStr = "";
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]

        public static extern int GetShortPathName(
         string lpszLongPath,
         string shortFile,
         int cchBuffer
        );
        int ilong;
        //定义播放状态枚举变量
        public enum State
        {
            mPlaying = 1,
            mPuase = 2,
            mStop = 3
        };
        //结构变量
        public struct structMCI
        {
            public bool bMut;
            public int iDur;
            public int iPos;
            public int iVol;
            public int iBal;
            public string iName;
            public State state;
        };
        public structMCI mc = new structMCI();

        /// <summary>
        /// 当前播放状态的属性
        /// </summary>
        public int GetState
        {
            get
            {
                if (State.mPlaying == mc.state)
                {
                    return 1;
                }
                else if (State.mPuase == mc.state)
                {
                    return 2;
                }
                return 3;
            }
            set
            {
                if (1 == value)
                {
                    mc.state = State.mPlaying;
                }
                else if (2 == value)
                {
                    mc.state = State.mPuase;
                }
                else if (3 == value)
                {
                    mc.state = State.mStop;
                }
            }
        }

        #region 打开MCI设备

        public bool OpenMusic(string FileName, IntPtr Handle)
        {
            bool result = false;
            string MciCommand;
            int RefInt;
            ShortPathName = "";
            ShortPathName = ShortPathName.PadLeft(260, Convert.ToChar(" "));
            RefInt = GetShortPathName(FileName, ShortPathName, ShortPathName.Length);
            ShortPathName = GetCurrPath(ShortPathName);
            MciCommand = string.Format("open {0} alias media ", ShortPathName);//"open " & RefShortName & " type " & DriverID & " alias NOWMUSIC"   
                //if (Handle != IntPtr.Zero)
                //{
                //    MciCommand = MciCommand + string.Format(" parent {0} style child ", Handle);// " parent " & Hwnd & " style child"   
                //}
                //else
                //{
                //    MciCommand = MciCommand + " style overlapped ";
                //}

            TemStr = "";
            TemStr = TemStr.PadLeft(128, Convert.ToChar(" "));
            RefInt=APIClass.mciSendString(MciCommand, null, 0, 0);
            RefInt=APIClass.mciSendString("set media time format milliseconds", null, 0, 0);
            if (RefInt == 0)
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 关闭媒体

        public bool CloseMusic()
        {
            int RefInt = APIClass.mciSendString("close media", null, 0, 0);
            mc.state = State.mStop;
            if (RefInt == 0)
                return true;

            return false;
        }

        #endregion
        /// <summary>
        /// 返回播放总时间
        /// </summary>
        /// <returns></returns>
        public string PlayerAllTime()
        {
            int total = Duration;
            return total.ToString();
        }

        /// <summary>
        /// 当前音量属性
        /// </summary>
        public int Volume
        {
            get
            {
                return mc.iVol;
            }
            set
            {
                SetVolumeTo(value);
            }
        }

        /// <summary>
        /// 播放位置的属性
        /// </summary>
        public int Position
        {
            get
            {
                return mc.iPos;
            }
            set
            {
                StepTo(value);
            }
        }

        /// <summary>
        /// 播放
        /// </summary>
        public int play()
        {
            TemStr = "";
            TemStr = TemStr.PadLeft(127, Convert.ToChar(" "));

            ilong = APIClass.mciSendString("play media notify", TemStr, TemStr.Length, 0);
            SetVolumeTo(200);
            mc.state = State.mPlaying;
            return ilong;

        }

        #region 暂停播放

        public bool PauseMusic()
        {
            int RefInt = APIClass.mciSendString("pause media", null, 0, 0);
            mc.state = State.mPuase;
            if (RefInt == 0)
                return true;

            return false;
        }

        #endregion

        #region 继续播放

        public bool ResumeMusic()
        {
            int RefInt = APIClass.mciSendString("resume media", null, 0, 0);
            mc.state = State.mPlaying;
            if (RefInt == 0)
                return true;

            return false;
        }

        #endregion

        /// <summary>
        /// 快进\快退
        /// </summary>
        public void StepTo(int steps)
        {
            if (steps < 0)
            {
                TemStr = "";
                steps = -steps;
                TemStr = TemStr.PadLeft(128, Convert.ToChar(" "));
                ilong = APIClass.mciSendString("step media by reverse", TemStr, steps, 0);
                mc.state = State.mPlaying;
            }
            else
            {
                TemStr = "";
                TemStr = TemStr.PadLeft(128, Convert.ToChar(" "));
                ilong = APIClass.mciSendString("step media by", TemStr, steps, 0);
                mc.state = State.mPlaying;
            }
        }

        #region 获取媒体的长度

        public int GetMusicLength()
        {
            durLength = "";
            durLength = durLength.PadLeft(128, Convert.ToChar(" "));
            ilong =APIClass.mciSendString("status media length", durLength, durLength.Length, 0);
           
            if (ilong != 0)
            {
                return 0;
            }
            durLength = durLength.Trim();
            if (string.IsNullOrEmpty(durLength))
                return 0;
            else
                return Convert.ToInt32(durLength)/1000;
        }

        #endregion
        ///////////////////////////////////////////
        //音量控制
        /// <summary>
        /// 获取当前音量
        /// </summary>
        /// <returns></returns>
        public int GetNowVolumn()
        {
            int volumn = 0;
            TemStr = "";
            TemStr = TemStr.PadLeft(128, Convert.ToChar(" "));
            ilong = APIClass.mciSendString("status media volumn", TemStr, volumn, 0);
            //mc.state = State.mPlaying;
            return volumn;
        }

        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="volume">设置的值</param>
        public void SetVolumeTo(int volume)
        {
            TemStr = "";
            volume *= 10;
            TemStr = TemStr.PadLeft(128, Convert.ToChar(" "));
            ilong = APIClass.mciSendString("setaudio media volume to " + volume.ToString(), TemStr, 128, 0);
            mc.iVol = volume;
           // mc.state = State.mPlaying;
        }

        /// <summary>
        /// 得到文件路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetCurrPath(string name)
        {
            if (name.Length < 1)
            {
                return "";
            }
            name = name.Trim();
            name = name.Substring(0, name.Length - 1);
            return name;
        }

        //总时间
        public int Duration
        {
            get
            {
                durLength = "";
                durLength = durLength.PadLeft(128, Convert.ToChar(" "));
                int ilog = APIClass.mciSendString("status media length", durLength, durLength.Length, 0);
                durLength = durLength.Trim();
                if (durLength == "")
                {
                    return 0;
                }
                return (int)(Convert.ToDouble(Convert.ToInt32(durLength)) / 1000f);
            }
        }

        //当前时间
        public int CurrentPosition
        {
            get
            {
                durLength = "";
                durLength = durLength.PadLeft(128, Convert.ToChar(" "));
                int ilog = APIClass.mciSendString("status media position", durLength, durLength.Length, 0);
                durLength = durLength.Trim();
                mc.iPos = (int)(Convert.ToDouble(Convert.ToInt32(durLength)) / 1000f);
                return mc.iPos;
            }
        }

        public class APIClass
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetShortPathName(string lpszLongPath, string shortFile, int cchBuffer);

            [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
            public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        }
    }
}

