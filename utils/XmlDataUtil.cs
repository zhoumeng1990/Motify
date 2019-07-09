using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MotifyPackage.utils
{
    class XmlDataUtil
    {
        //private static readonly string xmlPath = Environment.CurrentDirectory + "configSetting.xml";
        private static readonly string xmlPath = "D://configSetting.xml";

        public static void CreateXMLForData(Dictionary<String, String> keyValuePairs)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlNode = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDocument.AppendChild(xmlNode);
            //创建根节点
            XmlNode rootNode = xmlDocument.CreateElement("Settings");
            xmlDocument.AppendChild(rootNode);
            CreateNode(xmlDocument, rootNode, keyValuePairs);
            xmlDocument.Save(xmlPath);
        }

        //创建子节点存储数据
        private static void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, Dictionary<String, String> keyValuePair)
        {
            foreach (KeyValuePair<String, String> keyValue in keyValuePair)
            {
                XmlNode node = xmlDoc.CreateElement(keyValue.Key, xmlDoc.DocumentElement.NamespaceURI);
                Console.WriteLine("keyValue.Key" + keyValue.Key);
                Console.WriteLine("keyValue.Value" + keyValue.Value);
                node.InnerText = keyValue.Value;
                parentNode.AppendChild(node);
            }
        }

        //获取xml里面存储的数据
        public static Dictionary<string, string> GetManifestXML()
        {
            if (!File.Exists(xmlPath))
            {
                return null;
            }

            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlElement xmlNode = (XmlElement)doc.SelectSingleNode("Settings");

            keyValues.Add("apktoolPath", xmlNode.SelectSingleNode("apktoolPath").InnerText);
            keyValues.Add("signerPath", xmlNode.SelectSingleNode("signerPath").InnerText);
            keyValues.Add("signerPassword", xmlNode.SelectSingleNode("signerPassword").InnerText);
            keyValues.Add("alias", xmlNode.SelectSingleNode("alias").InnerText);

            return keyValues;
        }
    }
}
