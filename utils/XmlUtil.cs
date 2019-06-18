using MotifyPackage.entify;
using MotifyPackage.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MotifyPackage.utils
{
    class XmlUtil
    {
        private IXmlCallback xmlCallback;
        private MainEntity mainEntity;

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
                        continue;
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
            xmlNode.Attributes["package"].Value = mainEntity.PackageName;
            doc.Save(manifestPath);
            xmlCallback.MotifyPackageNameEnd();
        }
    }
}
