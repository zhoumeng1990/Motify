using ModifyPackage.entify;
using ModifyPackage.interfaces;
using ModifyPackage.utils;
using MotifyPackage.utils;
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

        //获取别名后的操作
        public void GetAliasEnd(string alias)
        {
            iMain.AliasValue(alias ?? "");

            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "apktoolPath", mainEntity.ApktoolPath },
                { "signerPath", mainEntity.SignerPath },
                { "signerPassword", mainEntity.SignerPassword },
                { "alias", mainEntity.Alias }
            };
            XmlDataUtil.CreateXMLForData(keyValues);

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
                    mainEntity.PackageNameList = new List<string>();
                    int i = 0;
                    foreach (var apkPathFile in apkPathFiles)
                    {
                        if (File.Exists(apkPathFile)&& Path.GetExtension(apkPathFile).Equals(".apk"))
                        {
                            ++i;
                            mainEntity.ApkPathList.Add(apkPathFile);
                            mainEntity.PackageNameList.Add(mainEntity.PackageName + i);
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

        //反编译之后的操作
        public void DecodeEnd()
        {
            mainEntity.DirectoryName = Path.GetDirectoryName(mainEntity.ApkPath) + "\\" + Path.GetFileNameWithoutExtension(mainEntity.ApkPath);
            xmlUtil = new XmlUtil(this, mainEntity);
            xmlUtil.AnalysisXML(mainEntity.DirectoryName);
        }

        //构建之后的操作
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

        //签名结束后的操作
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

        //继续反编译
        public void GoOnDecode()
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
                else
                {
                    MessageBox.Show("执行完毕");
                }
            }
        }

        //获取渠道列表
        private void GetChannelList()
        {
            if (!CommonUtil.IsEmpty(mainEntity.ChannePath))
            {
                mainEntity.ChanneList = fileUtil.GetTxtDataList(mainEntity.ChannePath);
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
        }

        //修改manifest之后的操作
        public void ModifyManifestEnd()
        {
            if (!CommonUtil.IsEmpty(mainEntity.LoadingPath))
            {
                fileUtil.ModifyLoading(mainEntity.DirectoryName, mainEntity.LoadingPath);
            }
            MotifyLoadingEnd();
        }

        //修改icon之后的操作
        public void ModifyIcon(string iconName)
        {
            fileUtil.ModifyLoading(mainEntity.DirectoryName, mainEntity.IconPath, iconName);
        }

        public void ModifyAppNameEnd()
        {
            throw new NotImplementedException();
        }

        //修改loading页之后的操作
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
