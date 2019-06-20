using MotifyPackage.entify;
using MotifyPackage.interfaces;
using MotifyPackage.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MotifyPackage.control
{
    class MainControl : IMain, IProcess, IXmlCallback
    {

        private MainEntity mainEntity;
        private XmlUtil xmlUtil;
        private ProcessUtil processUtil;

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
            mainEntity.ChanneList = new FileUtil().GetChannelList(mainEntity.ChannePath);
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

        public void ModifyPackageNameEnd()
        {
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
