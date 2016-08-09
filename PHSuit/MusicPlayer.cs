using System.Runtime.InteropServices;
using System.Text;
using System;
using System.Windows.Forms;
using System.IO;
namespace PHSuit
{
    public class Media
    {
        public const int MM_MCINOTIFY = 0x3B9;

        private string fileName;
        private bool isOpen = false;
        private int handle;
        private string mediaName = "media";

        //mciSendString 
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        private static extern int mciSendString(
            string command,
            StringBuilder returnValue,
            int returnLength,
            int winHandle);

        private void ClosePlayer()
        {
            if (isOpen)
            {
                String playCommand = "Close " + mediaName;
                mciSendString(playCommand, null, 0,0);
                isOpen = false;
            }
        }


        private void OpenMediaFile()
        {
            ClosePlayer();
            string playCommand = "Open \"" + fileName + "\" type mpegvideo alias " + mediaName;
            mciSendString(playCommand, null, 0, 0);
            isOpen = true;
        }


        private void PlayMediaFile()
        {
            if (isOpen)
            {
                string playCommand = "Play " + mediaName + " notify";
                mciSendString(playCommand, null, 0, handle);
            }
        }


        public void Play(string fileName, int formPro)
        {
            this.fileName = fileName;
            this.handle = formPro;
            OpenMediaFile();
            PlayMediaFile();
        }

        public void Stop()
        {
            ClosePlayer();
        }


        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);


        //[DllImport("winmm.dll")]
        //public static extern int mciSendString(string m_strCmd, StringBuilder m_strReceive, int m_v1, int m_v2);



        public string getasfTime(string filePath)
        {
            //long l1 = mciSendString("close " + filePath, null, 0, 0);
            string MciCommand = "open " + filePath + " alias media1 ";
            MciCommand = "open G:\\file\\2.mp3 alias media ";
            long l2 = mciSendString(MciCommand, null, 0, 0);
            long l3 = mciSendString("play " + filePath, null, 0, 0);// + filePath


            //StringBuilder shortpath = new StringBuilder(80);
            //GetShortPathName(filePath, shortpath, shortpath.Capacity);
            //string name = shortpath.ToString();
            StringBuilder buf = new StringBuilder(80);
            //mciSendString("close all", buf, buf.Capacity, 0);
            //mciSendString("open " + name + " ", buf, buf.Capacity, 0);//alias  type mpegvideo alias media
            mciSendString("status media length", buf, buf.Capacity,0);//media
            string ss = buf.ToString().Trim();
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)Convert.ToDouble(buf.ToString().Trim()));
            return ts.Seconds.ToString();
        }

    }

}