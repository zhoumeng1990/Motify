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

        public void ExecuteDecodeCMD(string fileName)
        {

            Process process = new Process();  //创建进程对象
            InitProcess(process);

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

        private void InitProcess(Process process)
        {
            process.StartInfo.FileName = "cmd.exe";  //要执行的程序名

            process.StartInfo.UseShellExecute = false;  ////不使用系统外壳程序启动进程
            process.StartInfo.CreateNoWindow = true;  //不显示dos程序窗口

            //重新定向标准输入，输入，错误输出
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();  //进程开始
        }

        public void ExecuteBuildCMD(string fileName)
        {
            Process process = new Process();  //创建进程对象
            InitProcess(process);

            //输入dos命令
            process.StandardInput.WriteLine("cd {0}", Path.GetDirectoryName(fileName));
            process.StandardInput.WriteLine("apktool b {0}", Path.GetFileNameWithoutExtension(fileName));
            process.StandardInput.WriteLine("exit");

            string strRst = process.StandardOutput.ReadToEnd(); //获取结果 
            Console.WriteLine("已执行了：{0}", strRst);

            process.WaitForExit();  //等待命令结束
            process.Close();  //进程结束

            iProcess.BuildEnd();
        }

        private string getAlisa(Process process, MainEntity mainEntity)
        {
            process.StandardInput.WriteLine("keytool -list  -v -keystore " + mainEntity.SignerPath + " -storepass " + mainEntity.SignerPassword);
            string strRst = process.StandardOutput.ReadToEnd(); //获取结果 
            return strRst;
        }

        public void ExecuteSignerCMD(MainEntity mainEntity)
        {
            Process process = new Process();  //创建进程对象
            InitProcess(process);
            //getAlisa(process, mainEntity);
            //输入dos命令

            //process.StandardInput.WriteLine("keytool -list  -v -keystore " + mainEntity.SignerPath + " -storepass " + mainEntity.SignerPassword);
            //Console.WriteLine("keytool -list  -v -keystore C:\\Users\\game\\Desktop\\zm.jks -storepass 123456");
            //string strsRst = process.StandardOutput.ReadToEnd(); //获取结果 
           // Console.WriteLine(strsRst);

            process.StandardInput.WriteLine("jarsigner -verbose -keystore C:\\Users\\game\\Desktop\\zm.jks -signedjar D:\\ok\\motify\\game_base_588\\dist\\game_base_588_signer.apk D:\\ok\\motify\\game_base_588\\dist\\game_base_588.apk zero");
            process.StandardInput.WriteLine("123456");
            process.StandardInput.WriteLine("exit");

            string strRst = process.StandardOutput.ReadToEnd(); //获取结果 

            process.WaitForExit();  //等待命令结束
            process.Close();  //进程结束
            Console.WriteLine(strRst);
        }
    }
}
