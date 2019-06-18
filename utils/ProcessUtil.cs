using MotifyPackage.control;
using MotifyPackage.entify;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MotifyPackage.utils
{
    class ProcessUtil
    {
        private IProcess iProcess;
        public ProcessUtil(IProcess iProcess)
        {
            this.iProcess = iProcess;
        }

        public void ExecuteCMD(string fileName)
        {

            Process process = new Process();  //创建进程对象
            process.StartInfo.FileName = "cmd.exe";  //要执行的程序名

            process.StartInfo.UseShellExecute = false;  ////不使用系统外壳程序启动进程
            process.StartInfo.CreateNoWindow = true;  //不显示dos程序窗口

            //重新定向标准输入，输入，错误输出
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();  //进程开始

            //输入dos命令
            process.StandardInput.WriteLine("cd {0}", Path.GetDirectoryName(fileName));
            process.StandardInput.WriteLine("apktool d {0}", fileName);
            process.StandardInput.WriteLine("exit");

            string strRst = process.StandardOutput.ReadToEnd(); //获取结果 
            Console.WriteLine("已执行了：{0}", strRst);

            process.WaitForExit();  //等待命令结束
            process.Close();  //进程结束

            iProcess.DosEnd();
        }
    }
}
