﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MotifyPackage.utils
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
    }
}
