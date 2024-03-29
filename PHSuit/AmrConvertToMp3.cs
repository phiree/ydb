﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PHSuit
{
    public class MediaConvert
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.PHSuit.MediaConvert");
        /// <summary>
        /// 将Wav音频转成Amr手机音频
        /// </summary>
        /// <param name="applicationPath">ffmeg.exe文件路径</param>
        /// <param name="fileName">WAV文件的路径(带文件名)</param>
        /// <param name="targetFilName">生成目前amr文件路径（带文件名）</param>
        public void ConvertToAmr(string applicationPath, string fileName, string targetFilName)
        {
            string c = applicationPath + @"\ffmpeg.exe -y -i " + fileName + " -ar 8000 -ab 12.2k -ac 1 " + targetFilName;
            Cmd(c);
        }

        /// <summary>
        /// 将音频转成mp3
        /// </summary>
        /// <param name="applicationPath">ffmeg.exe文件路径</param>
        /// <param name="fileName">文件的路径(带文件名)</param>
        /// <param name="targetFilName">生成目前amr文件路径（带文件名）</param>
        public void ConvertToMp3(string applicationPath, string fileName, string targetFilName)
        {
            string c = applicationPath + @"ffmpeg.exe -i " + fileName + " " + targetFilName;
            Cmd(c);
        }

        /// <summary>
        /// 执行Cmd命令
        /// </summary>
        private void Cmd(string c)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";//C:\\inetpub\\dianzhu\\cmd.exe
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.Arguments = " \"cmd /C " + c + "\"";// C表示执行完此次操作后关闭
                process.Start();
                process.StandardInput.WriteLine(c);
                process.StandardInput.AutoFlush = true;
                //process.StandardInput.WriteLine("exit");
                log.Debug("开始返回");
                StreamReader reader = process.StandardOutput;//截取输出流 
                string result = reader.ReadToEnd();

                StreamReader reader1 = process.StandardError;//截取输出流
                string result1 = reader1.ReadToEnd();

                process.WaitForExit();
                log.Debug("成功返回");
            }
            catch(Exception e)
            {
                PHSuit.ExceptionLoger.ExceptionLog(log, e);
            }
        }
        /// <summary>
        /// 获取文件的byte[]
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileByte(string fileName)
        {
            FileStream pFileStream = null;
            byte[] pReadByte = new byte[0];
            try
            {
                pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(pFileStream);
                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
                pReadByte = r.ReadBytes((int)r.BaseStream.Length);
                return pReadByte;
            }
            catch
            {
                return pReadByte;
            }
            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }
        }
        /// <summary>
        /// 将文件的byte[]生成文件
        /// </summary>
        /// <param name="pReadByte"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool writeFile(byte[] pReadByte, string fileName)
        {
            FileStream pFileStream = null;
            try
            {
                pFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                pFileStream.Write(pReadByte, 0, pReadByte.Length);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (pFileStream != null)
                    pFileStream.Close();
            }
            return true;
        }
    }
}
