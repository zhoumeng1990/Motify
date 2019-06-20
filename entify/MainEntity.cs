using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotifyPackage.entify
{
    class MainEntity
    {
        private string apkPath;
        private string packageName;
        private string loadingPaht;
        private string iconPath;
        private string appName;
        private string directoryName;
        private string signerPath;
        private string signerPassword;
        private string alias;
        private string channePath;
        private List<string> channeList;

        public string ApkPath { get => apkPath; set => apkPath = value; }
        public string PackageName { get => packageName; set => packageName = value; }
        public string LoadingPaht { get => loadingPaht; set => loadingPaht = value; }
        public string IconPath { get => iconPath; set => iconPath = value; }
        public string AppName { get => appName; set => appName = value; }
        public string DirectoryName { get => directoryName; set => directoryName = value; }
        public string SignerPath { get => signerPath; set => signerPath = value; }
        public string SignerPassword { get => signerPassword; set => signerPassword = value; }
        public string Alias { get => alias; set => alias = value; }
        public string ChannePath { get => channePath; set => channePath = value; }
        public List<string> ChanneList { get => channeList; set => channeList = value; }
    }
}
