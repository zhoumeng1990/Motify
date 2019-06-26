using ModifyPackage.entify;
using ModifyPackage.interfaces;
using System;
using System.IO;
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

        public void AnalysisXML(string filePath)
        {
            DirectoryInfo theFolder = new DirectoryInfo(filePath);
            foreach (FileInfo file in theFolder.GetFiles())
            {
                Console.WriteLine("file.Name:{0}", file.Name);
                if (file.Name.Equals("AndroidManifest.xml"))
                {
                    AnalysisManifestXML(file.FullName);
                    return;
                }
            }

            foreach (DirectoryInfo directoryInfo in theFolder.GetDirectories())
            {
                DirectoryInfo directoryInfos = new DirectoryInfo(directoryInfo.FullName);
                foreach (FileInfo file in directoryInfos.GetFiles())
                {
                    if (file.Name.Equals("AndroidManifest.xml"))
                    {
                        AnalysisManifestXML(file.FullName);
                        return;
                    }
                }
            }
        }

        private void AnalysisManifestXML(string manifestPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(manifestPath);
            XmlElement xmlNode = (XmlElement)doc.SelectSingleNode("manifest");
            string package = xmlNode.GetAttribute("package");
            Console.WriteLine("value is {0}", package);
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
