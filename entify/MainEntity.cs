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

        public string ApkPath { get => apkPath; set => apkPath = value; }
        public string PackageName { get => packageName; set => packageName = value; }
        public string LoadingPaht { get => loadingPaht; set => loadingPaht = value; }
        public string IconPath { get => iconPath; set => iconPath = value; }
        public string AppName { get => appName; set => appName = value; }
        public string DirectoryName { get => directoryName; set => directoryName = value; }
    }
}
