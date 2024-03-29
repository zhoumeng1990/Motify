﻿using ModifyPackage.entify;
using ModifyPackage.interfaces;
using MotifyPackage.utils;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ModifyPackage.utils
{
    class XmlUtil
    {
        private readonly IXmlCallback xmlCallback;
        private readonly MainEntity mainEntity;

        public XmlUtil(IXmlCallback xmlCallback, MainEntity mainEntity)
        {
            this.xmlCallback = xmlCallback;
            this.mainEntity = mainEntity;
        }

        //拿到manifest
        public void AnalysisXML(string filePath)
        {
            //bool notExecute = false;
            if (!Directory.Exists(filePath))
            {
                return;
            }
            DirectoryInfo theFolder = new DirectoryInfo(filePath);
            /*foreach (FileInfo file in theFolder.GetFiles())
            {
                if (file.Name.Equals("AndroidManifest.xml"))
                {
                    AnalysisManifestXML(file.FullName);
                    notExecute = true;
                }

                if (file.Name.Equals("apktool.yml"))
                {
                    YMLUtil yMLUtil = new YMLUtil(file.FullName);
                    string targetSdkVersion = yMLUtil.read("targetSdkVersion");
                    if (targetSdkVersion.Contains("\'"))
                    {
                        targetSdkVersion = targetSdkVersion.Replace("\'", "");
                    }
                    Console.WriteLine("targetSdkVersion:" + targetSdkVersion);
                    notExecute = true;
                }
            }*/
            if (!XMLHelper(theFolder))
            {
                foreach (DirectoryInfo directoryInfo in theFolder.GetDirectories())
                {
                    DirectoryInfo directoryInfos = new DirectoryInfo(directoryInfo.FullName);
                    XMLHelper(theFolder);
                    /*foreach (FileInfo file in directoryInfos.GetFiles())
                    {
                        if (file.Name.Equals("AndroidManifest.xml"))
                        {
                            AnalysisManifestXML(file.FullName);
                            return;
                        }
                    }*/
                }
            }
        }

        private bool XMLHelper(DirectoryInfo theFolder)
        {
            bool notExecute = false;
            string manifestPath = null;
            foreach (FileInfo file in theFolder.GetFiles())
            {
                if (file.Name.Equals("AndroidManifest.xml"))
                {
                    manifestPath = file.FullName;
                    notExecute = true;
                }

                if (file.Name.Equals("apktool.yml"))
                {
                    YMLUtil yMLUtil = new YMLUtil(file.FullName);
                    string targetSdkVersion = yMLUtil.Read("targetSdkVersion");
                    if (targetSdkVersion.Contains("\'"))
                    {
                        targetSdkVersion = targetSdkVersion.Replace("\'", "");
                    }
                    if (int.Parse(targetSdkVersion) < 23)
                    {
                        MessageBox.Show("小于23");
                    }else if (int.Parse(targetSdkVersion) < 26)
                    {
                        yMLUtil.Modify("targetSdkVersion", "\'26\'");
                        yMLUtil.Save();
                    }
                    Console.WriteLine("targetSdkVersion:" + targetSdkVersion);
                }
            }
            if (!CommonUtil.IsEmpty(manifestPath))
            {
                notExecute = true;
                AnalysisManifestXML(manifestPath);
            }
            return notExecute;
        }

        private void AnalysisManifestXML(string manifestPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(manifestPath);
            XmlElement xmlNode = (XmlElement)doc.SelectSingleNode("manifest");
            string package = xmlNode.GetAttribute("package");
            Console.WriteLine("value is {0}", package);
            if (mainEntity.PackageNameList != null && mainEntity.PackageNameList.Count > 0)
            {
                mainEntity.PackageName = mainEntity.PackageNameList[0];
                mainEntity.PackageNameList.RemoveAt(0);
            }
            if (!CommonUtil.IsEmpty(mainEntity.PackageName)) {
                xmlNode.Attributes["package"].Value = mainEntity.PackageName;
            }

            XmlNodeList xmlNodeList = doc.GetElementsByTagName("application");
            XmlNode applicationNode = xmlNodeList.Item(0);
            if (!CommonUtil.IsEmpty(mainEntity.IconPath))
            {
                //更换图标的操作
                if (File.Exists(mainEntity.IconPath))
                {
                    string iconName = applicationNode.Attributes["android:icon"].Value;
                    iconName = iconName.Substring(iconName.IndexOf("/")+1);
                    xmlCallback.ModifyIcon(iconName);
                    /*string destDirectoryName = mainEntity.DirectoryName + "\\res\\drawable-xhdpi\\";
                    if (Directory.Exists(destDirectoryName))
                    {
                        File.Copy(mainEntity.IconPath, destDirectoryName + Path.GetFileName(mainEntity.IconPath), true);
                        applicationNode.Attributes["android:icon"].Value = "@drawable/" + Path.GetFileNameWithoutExtension(mainEntity.IconPath);
                    }*/
                }
                if (applicationNode.Attributes["android:roundIcon"] != null)
                {
                    //删除android:roundIcon节点
                    XmlElement xmlElement = (XmlElement)applicationNode;
                    xmlElement.RemoveAttribute("android:roundIcon");
                }
            }

            if (!CommonUtil.IsEmpty(mainEntity.PackageName))
            {
                //更换应用名称的操作
                applicationNode.Attributes["android:label"].Value = mainEntity.AppName;
            }

            doc.Save(manifestPath);
            xmlCallback.ModifyManifestEnd();
        }
    }
}
