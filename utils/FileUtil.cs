﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ModifyPackage.utils
{
    class FileUtil
    {
        public List<string> GetChannelList(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (Path.GetExtension(filePath).Equals(".txt"))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    using(StreamReader streamReader = new StreamReader(filePath))
                    {
                        string line;
                        while((line = streamReader.ReadLine()) != null)
                        {
                            stringBuilder.Append(line+",");
                        }
                    }
                    return stringBuilder.ToString().TrimEnd(',').Split(',').ToList<string>();
                }
            }
            return null;
        }

        public void ModifyChannel(string directoryPath,string info)
        {
            if(Directory.Exists(directoryPath + "\\assets"))
            {
                string fileName = directoryPath + "\\assets\\channel.ini";
                FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.SetLength(0);
                fileStream.Close();

                //true 若要将数据追加到该文件; false 覆盖该文件。 如果指定的文件不存在，该参数无效，且构造函数将创建一个新文件。
                StreamWriter sw = new StreamWriter(fileName, false);
                sw.WriteLine(info);
                sw.Close();
            }
        }

        public void ModifyLoading(string directoryPath,string loadingPath)
        {
            ModifyLoading(directoryPath, loadingPath, null);
        }

        public void ModifyLoading(string directoryPath, string loadingPath,string modifyName)
        {

            if (Directory.Exists(directoryPath + "\\res"))
            {
                List<string> drawableFilePaths = new List<string>();
                string loadingName = Path.GetFileNameWithoutExtension(loadingPath);
                string[] directorys = Directory.GetDirectories(directoryPath + "\\res");
                foreach (string directoryName in directorys)
                {
                    if (directoryName.Contains("drawable") || directoryName.Contains("mipmap"))
                    {
                        string[] files = Directory.GetFiles(directoryName);
                        foreach (string fileName in files)
                        {
                            //三目运算转 COALESCE 表达式
                            if (Path.GetFileNameWithoutExtension(fileName).Equals(modifyName ?? loadingName))
                            {
                                drawableFilePaths.Add(fileName);
                            }
                        }
                    }
                }
                drawableFilePaths.ForEach(drawable => {
                    File.Delete(drawable);
                    drawable = Path.GetDirectoryName(drawable) + "\\" + Path.GetFileNameWithoutExtension(drawable) + Path.GetExtension(loadingPath);
                    File.Copy(loadingPath, drawable);
                    //File.Move(loadingPath, drawable);
                });
            }
        }

        private static StringBuilder stringBuilder;

        /**
         * 是否加固
         */
        public Boolean Enciphered(string filePath)
        {
            Boolean isEnciphered = false;
            DirectoryInfo theFolder = new DirectoryInfo(filePath);
            if (theFolder != null)
            {
                DirectoryInfo[] directoryInfos = theFolder.GetDirectories();
                foreach (DirectoryInfo directoryInfo in directoryInfos)
                {
                    if (directoryInfo.FullName.EndsWith("smali"))
                    {
                        if (directoryInfo.GetDirectories().Length < 2)
                        {
                            DirectoryInfo[] comDirectoryInfos = directoryInfo.GetDirectories();
                            foreach (DirectoryInfo comDirectoryInfo in comDirectoryInfos)
                            {
                                if (comDirectoryInfo.Name.Equals("com"))
                                {
                                    DirectoryInfo[] smaliDirectoryInfos = comDirectoryInfo.GetDirectories();
                                    /*foreach (DirectoryInfo smaliDirectoryInfo in smaliDirectoryInfos)
                                    {
                                        if (stringBuilder == null)
                                        {
                                            stringBuilder = new StringBuilder();
                                        }
                                        if (smaliDirectoryInfo.Name.Equals("tencent"))
                                        {
                                            MoveAPK(filePath, directoryInfo, "tencent");
                                        }
                                        else if (smaliDirectoryInfo.Name.Equals("qihoo"))
                                        {
                                            MoveAPK(filePath, directoryInfo, "qihoo");
                                        }
                                    }*/
                                    if (smaliDirectoryInfos.Length>0)
                                    {
                                        if (stringBuilder == null)
                                        {
                                            stringBuilder = new StringBuilder();
                                        }
                                        MoveAPK(filePath, directoryInfo, smaliDirectoryInfos[0].Name);
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
                if (stringBuilder != null && stringBuilder.Length > 0)
                {
                    string fileName = ParentName(filePath) + "\\已加固.txt";
                    FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileStream.SetLength(0);
                    fileStream.Close();

                    //true 若要将数据追加到该文件; false 覆盖该文件。 如果指定的文件不存在，该参数无效，且构造函数将创建一个新文件。
                    StreamWriter sw = new StreamWriter(fileName, false);
                    sw.WriteLine(stringBuilder);
                    sw.Close();
                }
            }
            return isEnciphered;
        }

        private string ParentName(string path)
        {
            Directory.SetCurrentDirectory(Directory.GetParent(path).FullName);
            return Directory.GetCurrentDirectory();
        }

        private void MoveAPK(string filePath,DirectoryInfo directoryInfo, string storeName)
        {
            string sourceFileName = filePath + ".apk";
            stringBuilder.AppendLine(Path.GetFileNameWithoutExtension(sourceFileName) + " 已 " + storeName + "加固\n\n");
            Console.WriteLine("{0}已加固", Path.GetFileName(sourceFileName));
            string path = ParentName(filePath);
            if (!Directory.Exists(path + "\\加固"))
            {
                Directory.CreateDirectory(path + "\\加固");
            }

            if (!Directory.Exists(path + "\\加固\\"+ storeName))
            {
                Directory.CreateDirectory(path + "\\加固\\" + storeName);
            }
            File.Move(sourceFileName, path + "\\加固\\" + storeName + "\\" + Path.GetFileName(sourceFileName) + ".apk");
        }
    }
}
