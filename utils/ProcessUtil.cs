using MotifyPackage.control;
using MotifyPackage.entify;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MotifyPackage.utils
{
    class ProcessUtil
    {
        private IProcess iProcess;
        public ProcessUtil(IProcess iProcess)
        {
            this.iProcess = iProcess;
        }

        //获取别名
        public void GetAlisa(MainEntity mainEntity)
        {
            if (CommonUtil.IsEmpty(mainEntity.SignerPath))
            {
                MessageBox.Show("请输入签名文件");
                return;
            }

            if (!File.Exists(mainEntity.SignerPath))
            {
                MessageBox.Show("请输入签名文件密码");
                return;
            }
            Process process = new Process();  //创建进程对象
            InitProcess(process);
            process.StandardInput.WriteLine("keytool -list  -v -keystore " + mainEntity.SignerPath + " -storepass " + mainEntity.SignerPassword);
            process.StandardInput.WriteLine("exit");

            string strRst = process.StandardOutput.ReadToEnd(); //获取结果 

            process.WaitForExit();  //等待命令结束
            process.Close();  //进程结束
            string alias = "";
            int startIndex;
            int length;
            if (strRst.Contains("Alias name"))
            {
                startIndex = strRst.IndexOf("Alias name");
                length = strRst.IndexOf("Creation date");
            }
            else if (strRst.Contains("别名"))
            {
                startIndex = strRst.IndexOf("别名");
                length = strRst.IndexOf("创建日期");
            }
            else
            {
                MessageBox.Show("签名密码错误");
                return;
            }

            String[] strs = strRst.Substring(startIndex, length).Split(':');
            if (strs != null && strs.Length > 0)
            {
                alias = strs[1];
                //斩头去尾留中间
                alias = alias.Substring(0, alias.IndexOf("\n")).TrimStart().TrimEnd();

                Console.WriteLine("alias is:\n" + alias + alias.Length);
            }
            else
            {
                MessageBox.Show("签名密码错误");
            }
            iProcess.GetAliasEnd(alias);
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

        public void ExecuteDecodeCMD(string fileName)
        {

            Process process = new Process();  //创建进程对象
            InitProcess(process);

            //输入dos命令
            process.StandardInput.WriteLine("cd {0}", Path.GetDirectoryName(fileName));
            process.StandardInput.WriteLine("apktool d {0}", fileName);
            process.StandardInput.WriteLine("exit");

            string strRst = process.StandardOutput.ReadToEnd(); //获取结果 

            process.WaitForExit();  //等待命令结束
            process.Close();  //进程结束

            iProcess.DosEnd();
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

        public void ExecuteSignerCMD(MainEntity mainEntity)
        {
            Process process = new Process();  //创建进程对象
            InitProcess(process);

            string outputSignerName = mainEntity.DirectoryName + "\\dist\\" + Path.GetFileNameWithoutExtension(mainEntity.ApkPath);
            process.StandardInput.WriteLine("jarsigner -verbose -keystore ｛0｝ -signedjar D:\\ok\\motify\\game_base_588\\dist\\game_base_588_signer.apk D:\\ok\\motify\\game_base_588\\dist\\game_base_588.apk zero",mainEntity.SignerPath,mainEntity.DirectoryName);
            process.StandardInput.WriteLine("123456");
            process.StandardInput.WriteLine("exit");

            string strRst = process.StandardOutput.ReadToEnd(); //获取结果 

            process.WaitForExit();  //等待命令结束
            process.Close();  //进程结束
        }
    }
}
