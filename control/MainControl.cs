using ModifyPackage.entify;
using ModifyPackage.interfaces;
using ModifyPackage.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ModifyPackage.control
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
            GetChannelList();
            if (CommonUtil.IsEmpty(mainEntity.ApktoolPath))
            {
                DecodeEnd();
            }
            else
            {
                if (Directory.Exists(mainEntity.ApkPath))
                {
                    string[] apkPathFiles = Directory.GetFiles(mainEntity.ApkPath);
                    mainEntity.ApkPathList = new List<string>();
                    foreach (var apkPathFile in apkPathFiles)
                    {
                        if (File.Exists(apkPathFile)&& Path.GetExtension(apkPathFile).Equals(".apk"))
                        {
                            mainEntity.ApkPathList.Add(apkPathFile);
                        }
                    }
                    if (mainEntity.ApkPathList.Count > 0)
                    {
                        mainEntity.ApkPath = mainEntity.ApkPathList[0];
                    }
                }
                processUtil.ExecuteDecodeCMD();
            }
        }

        public void DecodeEnd()
        {
            mainEntity.DirectoryName = Path.GetDirectoryName(mainEntity.ApkPath) + "\\" + Path.GetFileNameWithoutExtension(mainEntity.ApkPath);
            xmlUtil = new XmlUtil(this, mainEntity);
            xmlUtil.AnalysisXML(mainEntity.DirectoryName);
        }

        public void BuildEnd()
        {
            if (fileUtil.Enciphered(mainEntity.DirectoryName))
            {
                GoOnDecode();
            }
            else
            {
                processUtil.ExecuteSignerCMD();
            }
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

            else
            {
                GoOnDecode();
            }
        }

        private void GoOnDecode()
        {
            if (mainEntity.ApkPathList != null && mainEntity.ApkPathList.Count > 0)
            {
                mainEntity.ApkPathList.RemoveAt(0);
                if (mainEntity.ApkPathList.Count > 0)
                {
                    GetChannelList();
                    mainEntity.ApkPath = mainEntity.ApkPathList[0];
                    processUtil.ExecuteDecodeCMD();
                }
            }
            else
            {
                MessageBox.Show("执行完毕");
            }
        }

        private void GetChannelList()
        {
            if (!CommonUtil.IsEmpty(mainEntity.ChannePath))
            {
                mainEntity.ChanneList = fileUtil.GetChannelList(mainEntity.ChannePath);
            }
            else
            {
                mainEntity.ChanneList = new List<string>
                {
                    "0"
                };
            }
        }

        private void ExecuteThread()
        {
            Thread.Sleep(5000);
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
            GoOnDecode();
            //System.Environment.Exit(0);
            //Console.ReadLine();

        }

        public void ModifyManifestEnd()
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
            if (CommonUtil.IsEmpty(mainEntity.ApktoolPath))
            {
                BuildEnd();
            }
            else
            {
                processUtil.ExecuteBuildCMD();
            }
        }
    }
}
