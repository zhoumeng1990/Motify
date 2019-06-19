using MotifyPackage.entify;
using MotifyPackage.interfaces;
using MotifyPackage.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MotifyPackage.control
{
    class MainControl : IMain, IProcess, IXmlCallback
    {

        private MainEntity mainEntity;
        private XmlUtil xmlUtil;

        public MainControl()
        {
            
        }

        /**
         * 反编译
         */
        public void ExecuteProcess(MainEntity mainEntity)
        {
            this.mainEntity = mainEntity;
            string pathName = mainEntity.ApkPath;
            if (File.Exists(pathName))
            {
                if (Path.GetExtension(pathName).Equals(".apk"))
                {
                    ProcessUtil processUtil = new ProcessUtil(this);
                    processUtil.ExecuteCMD(pathName);
                }
            }
            else
            {
                Console.WriteLine("路径错误");
            }
        }

        public void DosEnd()
        {
            if (CommonUtil.IsEmpty(mainEntity.PackageName)){
                MotifyPackageNameEnd();
            }
            else
            {
                mainEntity.DirectoryName = Path.GetDirectoryName(mainEntity.ApkPath) + "\\" + Path.GetFileNameWithoutExtension(mainEntity.ApkPath);
                xmlUtil = new XmlUtil(this, mainEntity);
                xmlUtil.AnalysisXML(mainEntity.DirectoryName);
            }
        }

        public void MotifyPackageNameEnd()
        {
            if (CommonUtil.IsEmpty(mainEntity.IconPath))
            {
                MotifyAppNameEnd();
            }
            else
            {
                MotifyIconEnd();
            }
        }

        public void MotifyIconEnd()
        {
            throw new NotImplementedException();
        }

        public void MotifyAppNameEnd()
        {
            throw new NotImplementedException();
        }

        public void MotifyLoadingEnd()
        {
            throw new NotImplementedException();
        }
    }
}
