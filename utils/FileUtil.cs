using System;
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

        public void ModifyChannel(string directoryPath)
        {
            if(File.Exists(directoryPath + "\\assets\\channel.ini"))
            {

            }
        }
    }
}
