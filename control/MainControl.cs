﻿using MotifyPackage.entify;
using MotifyPackage.interfaces;
using MotifyPackage.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace MotifyPackage.control
{
    class MainControl : IMain, IProcess, IXmlCallback
    {

        private MainEntity mainEntity;
        private XmlUtil xmlUtil;
        private ProcessUtil processUtil;
        private FileUtil fileUtil;

        /**
         * 反编译
         */
        public void ExecuteProcess(MainEntity mainEntity)
        {
            this.mainEntity = mainEntity;
            if (File.Exists(mainEntity.ApkPath))
            {
                if (Path.GetExtension(mainEntity.ApkPath).Equals(".apk"))
                {
                    processUtil = new ProcessUtil(this);
                    //processUtil.ExecuteDecodeCMD(pathName);
                    processUtil.GetAlisa(mainEntity);
                }
                else
                {
                    MessageBox.Show("文件错误");
                }
            }
            else
            {
                MessageBox.Show("路径错误");
            }
        }

        public void GetAliasEnd(string alias)
        {
            mainEntity.Alias = alias;
            fileUtil = new FileUtil();
            mainEntity.ChanneList = fileUtil.GetChannelList(mainEntity.ChannePath);
            processUtil.ExecuteDecodeCMD(mainEntity.ApkPath);
        }

        public void DosEnd()
        {
            mainEntity.DirectoryName = Path.GetDirectoryName(mainEntity.ApkPath) + "\\" + Path.GetFileNameWithoutExtension(mainEntity.ApkPath);
            xmlUtil = new XmlUtil(this, mainEntity);
            xmlUtil.AnalysisXML(mainEntity.DirectoryName);
        }

        public void BuildEnd()
        {
            processUtil.ExecuteSignerCMD(mainEntity);
        }

        public void SignerEnd()
        {
            if (mainEntity.ChanneList == null || mainEntity.ChanneList.Count < 1)
            {
                return;
            }
            else
            {
                mainEntity.ChanneList.RemoveAt(0);
                if (mainEntity.ChanneList.Count > 0)
                {
                    fileUtil.ModifyChannel(mainEntity.DirectoryName, mainEntity.ChanneList[0]);
                    processUtil.ExecuteBuildCMD(mainEntity.ApkPath);
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
                if (!file.Contains("sign") && !file.Contains("temp"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch { }
                }
            }
            //System.Environment.Exit(0);
        }

        public void ModifyPackageNameEnd()
        {
            if (mainEntity.ChanneList==null || mainEntity.ChanneList.Count<1)
            {
                mainEntity.ChanneList.Add("0");
            }
            fileUtil.ModifyChannel(mainEntity.DirectoryName, mainEntity.ChanneList[0]);
            processUtil.ExecuteBuildCMD(mainEntity.ApkPath);
        }

        public void ModifyIconEnd()
        {
            throw new NotImplementedException();
        }

        public void ModifyAppNameEnd()
        {
            throw new NotImplementedException();
        }

        public void MotifyLoadingEnd()
        {
            throw new NotImplementedException();
        }
    }
}
