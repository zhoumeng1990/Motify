using MotifyPackage.entify;
using MotifyPackage.interfaces;
using MotifyPackage.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MotifyPackage.control
{
    class MainControl : IProcess, IXmlCallback
    {
        private MainEntity mainEntity;
        private XmlUtil xmlUtil;
        private ProcessUtil processUtil;
        private FileUtil fileUtil;
        private readonly IMain iMain;

        public MainControl(IMain iMain)
        {
            this.iMain = iMain;
        }

        /**
         * 反编译
         */
        public void ExecuteProcess(MainEntity mainEntity)
        {
            this.mainEntity = mainEntity;
            processUtil = new ProcessUtil(this);
            processUtil.SetMainEntity(mainEntity);

            if (CommonUtil.IsEmpty(mainEntity.SignerPath))
            {
                GetAliasEnd(null);
            }
            else
            {
                if (CommonUtil.IsEmpty(mainEntity.SignerPassword))
                {
                    MessageBox.Show("签名文件密码为空");
                }
                else
                {
                    processUtil.GetAlisa();
                }
            }
        }

        public void GetAliasEnd(string alias)
        {
            iMain.AliasValue(alias ?? "");
            fileUtil = new FileUtil();
            mainEntity.ChanneList = fileUtil.GetChannelList(mainEntity.ChannePath);
            processUtil.ExecuteDecodeCMD();
        }

        public void DosEnd()
        {
            mainEntity.DirectoryName = Path.GetDirectoryName(mainEntity.ApkPath) + "\\" + Path.GetFileNameWithoutExtension(mainEntity.ApkPath);
            xmlUtil = new XmlUtil(this, mainEntity);
            xmlUtil.AnalysisXML(mainEntity.DirectoryName);
        }

        public void BuildEnd()
        {
            processUtil.ExecuteSignerCMD();
        }

        public void SignerEnd()
        {
            if (mainEntity.ChanneList != null && mainEntity.ChanneList.Count > 0)
            {
                mainEntity.ChanneList.RemoveAt(0);
                if (mainEntity.ChanneList.Count > 0)
                {
                    fileUtil.ModifyChannel(mainEntity.DirectoryName, mainEntity.ChanneList[0]);
                    processUtil.ExecuteBuildCMD();
                }
                else
                {
                    Thread thread = new Thread(new ThreadStart(ExecuteThread));
                    thread.Start();
                }
            }
        }

        private void ExecuteThread()
        {
            Thread.Sleep(10000);
            string[] files = Directory.GetFiles(mainEntity.DirectoryName + "\\dist");
            foreach (string file in files)
            {
                if (!Path.GetFileNameWithoutExtension(file).EndsWith("signer") && !file.Contains("temp"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch { }
                }
            }
            //System.Environment.Exit(0);
            //Console.ReadLine();
        }

        public void ModifyPackageNameEnd()
        {
            if (!CommonUtil.IsEmpty(mainEntity.LoadingPath))
            {
                fileUtil.ModifyLoading(mainEntity.DirectoryName, mainEntity.LoadingPath);
            }
            MotifyLoadingEnd();
        }

        public void ModifyIcon(string iconName)
        {
            fileUtil.ModifyLoading(mainEntity.DirectoryName, mainEntity.IconPath, iconName);
        }

        public void ModifyAppNameEnd()
        {
            throw new NotImplementedException();
        }

        public void MotifyLoadingEnd()
        {
            if (!CommonUtil.IsEmpty(mainEntity.ChannePath))
            {
                if (mainEntity.ChanneList == null || mainEntity.ChanneList.Count < 1)
                {
                    mainEntity.ChanneList = new List<string>
                {
                    "0"
                };
                }
                fileUtil.ModifyChannel(mainEntity.DirectoryName, mainEntity.ChanneList[0]);
            }
            processUtil.ExecuteBuildCMD();
        }
    }
}
