using ModifyPackage.control;
using ModifyPackage.entify;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ModifyPackage.utils
{
    class ProcessUtil
    {
        private readonly IProcess iProcess;
        private MainEntity mainEntity;
        public ProcessUtil(IProcess iProcess)
        {
            this.iProcess = iProcess;
        }

        public void SetMainEntity(MainEntity mainEntity)
        {
            this.mainEntity = mainEntity;
        }

        //获取别名
        public void GetAlisa()
        {
            /*if (CommonUtil.IsEmpty(mainEntity.SignerPath))
            {
                MessageBox.Show("请输入签名文件");
                return;
            }

            if (!File.Exists(mainEntity.SignerPath))
            {
                MessageBox.Show("请输入签名文件密码");
                return;
            }*/

            Process process = InitProcess();
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

        //抽取并初始化进程
        private Process InitProcess()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";  //要执行的程序名
            process.StartInfo.UseShellExecute = false;  //不使用系统外壳程序启动进程
            process.StartInfo.CreateNoWindow = true;  //不显示dos程序窗口

            //重新定向标准输入，输入，错误输出
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = false;

            process.Start();  //进程开始
            return process;
        }

        //执行反编译
        public void ExecuteDecodeCMD()
        {
            string fileName = mainEntity.ApkPath;
            ThreadPool.QueueUserWorkItem(h =>
            {
                Process process = InitProcess();

                //输入dos命令
                process.StandardInput.WriteLine(Path.GetPathRoot(fileName).Substring(0,2));
                process.StandardInput.WriteLine("cd {0}", Path.GetDirectoryName(fileName));
                process.StandardInput.WriteLine("{0} d {1}", mainEntity.ApktoolPath, fileName.IndexOf(" ") > 0 ? "\"" + fileName + "\"" : fileName);
                process.StandardInput.WriteLine("exit");

                string strRst = process.StandardOutput.ReadToEnd(); //获取结果 
                Console.WriteLine("已执行了：{0}", strRst);
                //MessageBox.Show(strRst);

                process.WaitForExit();  //等待命令结束
                process.Close();  //进程结束

                iProcess.DecodeEnd();
            });
        }

        //构建
        public void ExecuteBuildCMD()
        {
            string fileName = mainEntity.ApkPath;
            Process process = InitProcess();

            string buildName = Path.GetFileNameWithoutExtension(fileName).IndexOf(" ") > 0 ? "\"" + Path.GetFileNameWithoutExtension(fileName) + "\"" : Path.GetFileNameWithoutExtension(fileName);

            //输入dos命令
            process.StandardInput.WriteLine(Path.GetPathRoot(fileName).Substring(0, 2));
            process.StandardInput.WriteLine("cd {0}", Path.GetDirectoryName(fileName));
            process.StandardInput.WriteLine("{0} b {1}", mainEntity.ApktoolPath, buildName);
            process.StandardInput.WriteLine("exit");

            string strRst = process.StandardOutput.ReadToEnd(); //获取结果 
            Console.WriteLine("已执行了：{0}", strRst);

            process.WaitForExit();  //等待命令结束
            process.Close();  //进程结束

            iProcess.BuildEnd();
        }

        //签名
        public void ExecuteSignerCMD()
        {
            if (CommonUtil.IsEmpty(mainEntity.SignerPath)|| CommonUtil.IsEmpty(mainEntity.SignerPassword))
            {
                iProcess.GoOnDecode();
                return;
            }
            Process process = InitProcess();
            // 改名方法
            string directorySigner = mainEntity.DirectoryName + "\\dist\\" + Path.GetFileNameWithoutExtension(mainEntity.ApkPath) + 
                (mainEntity.ChanneList[0].Equals("0") ? "" : mainEntity.ChanneList[0]) + "_signer";

            FileInfo fileInfo = new FileInfo(mainEntity.DirectoryName + "\\dist\\" + Path.GetFileName(mainEntity.ApkPath));
            if (File.Exists(directorySigner + ".apk"))
            {
                File.Delete(directorySigner + ".apk");
            }

            if (!Directory.Exists(mainEntity.DirectoryName + "\\dist"))
            {
                mainEntity.ChanneList.Clear();
                iProcess.SignerEnd();
                return;
            }

            fileInfo.MoveTo(directorySigner + ".apk");
            process.OutputDataReceived += new DataReceivedEventHandler(OnDataReceived);
            string outputSignerName = directorySigner +"_signer.apk";
            outputSignerName = outputSignerName.IndexOf(" ") > 0 ? "\"" + outputSignerName + "\"" : outputSignerName;
            directorySigner = directorySigner.IndexOf(" ") > 0 ? "\"" + directorySigner + ".apk\"" : directorySigner + ".apk";
            string cmdStr = "jarsigner -verbose -keystore " + mainEntity.SignerPath + " -signedjar " + outputSignerName + " "+ directorySigner + " "+ mainEntity.Alias;
            process.StandardInput.WriteLine(cmdStr);
            process.StandardInput.WriteLine(mainEntity.SignerPassword);
            process.StandardInput.WriteLine("exit");
            process.BeginOutputReadLine();

            iProcess.SignerEnd();
            
            process.WaitForExit();  //等待命令结束
            process.Close();  //进程结束
        }

        //异步打印
        private static void OnDataReceived(object Sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Console.WriteLine("e.Data:" + e.Data);
            }
        }
    }
}
