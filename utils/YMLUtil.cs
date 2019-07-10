using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MotifyPackage.utils
{
    class YMLUtil
    {
        // 所有行
        private readonly String[] lines;
        // 格式化为节点
        private List<Node> nodeList = new List<Node>();
        // 文件所在地址
        private readonly String path;

        public YMLUtil(String path)
        {
            this.path = path;
            this.lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Length; i++)
            {
                String line = lines[i];
                if (line.Trim() == "")
                {
                    Console.WriteLine("空白行，行号：" + (i + 1));
                    continue;
                }
                else if (line.Trim().Substring(0, 1) == "#")
                {
                    Console.WriteLine("注释行，行号：" + (i + 1));
                    continue;
                }

                String[] kv = Regex.Split(line, ":", RegexOptions.IgnoreCase);
                FindPreSpace(line);
                Node node = new Node();
                node.Space = FindPreSpace(line);
                node.Name = kv[0].Trim();

                // 去除前后空白符
                String fline = line.Trim();
                int first = fline.IndexOf(':');
                node.Value = first == fline.Length - 1 ? null : fline.Substring(first + 2, fline.Length - first - 2);
                node.Parent = FindParent(node.Space);
                nodeList.Add(node);
            }

            this.Formatting();
        }

        // 修改值 允许key为多级 例如：spring.datasource.url
        public void Modify(String key, String value)
        {
            Node node = FindNodeByKey(key);
            if (node != null)
            {
                node.Value = value;
            }
        }

        // 读取值
        public String Read(String key)
        {
            Node node = FindNodeByKey(key);
            if (node != null)
            {
                return node.Value;
            }
            return null;
        }

        // 根据key找节点
        private Node FindNodeByKey(String key)
        {
            String[] ks = key.Split('.');
            for (int i = 0; i < nodeList.Count; i++)
            {
                Node node = nodeList[i];
                if (node.Name == ks[ks.Length - 1])
                {
                    // 判断父级
                    Node tem = node;
                    // 统计匹配到的次数
                    int count = 1;
                    for (int j = ks.Length - 2; j >= 0; j--)
                    {
                        if (tem.Parent.Name == ks[j])
                        {
                            count++;
                            // 继续检查父级
                            tem = tem.Parent;
                        }
                    }

                    if (count == ks.Length)
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        // 保存到文件中
        public void Save()
        {
            StreamWriter stream = File.CreateText(this.path);
            for (int i = 0; i < nodeList.Count; i++)
            {
                Node node = nodeList[i];
                StringBuilder sb = new StringBuilder();
                // 放入前置空格
                for (int j = 0; j < node.Tier; j++)
                {
                    sb.Append("  ");
                }
                sb.Append(node.Name);
                if (node.Value != null)
                {
                    if (!node.Name.Contains(node.Value))
                    {
                        sb.Append(": ");
                        sb.Append(node.Value);
                    }

                    //Console.WriteLine("node.value的第 {0} 个值为：{1}", i, node.value);
                }
                else
                {
                    sb.Append(": ");
                }
                stream.WriteLine(sb.ToString());
            }
            stream.Flush();
            stream.Close();
        }

        // 格式化
        public void Formatting()
        {
            // 先找出根节点
            List<Node> parentNode = new List<Node>();
            for (int i = 0; i < nodeList.Count; i++)
            {
                Node node = nodeList[i];
                if (node.Parent == null)
                {
                    parentNode.Add(node);
                }
            }

            List<Node> fNodeList = new List<Node>();
            // 遍历根节点
            for (int i = 0; i < parentNode.Count; i++)
            {
                Node node = parentNode[i];
                fNodeList.Add(node);
                FindChildren(node, fNodeList);
            }

            Console.WriteLine("完成");

            // 指针指向格式化后的
            nodeList = fNodeList;
        }


        // 层级
        int tier = 0;
        // 查找孩子并进行分层
        private void FindChildren(Node node, List<Node> fNodeList)
        {
            // 当前层 默认第一级，根在外层进行操作
            tier++;

            for (int i = 0; i < nodeList.Count; i++)
            {
                Node item = nodeList[i];
                if (item.Parent == node)
                {
                    item.Tier = tier;
                    fNodeList.Add(item);
                    FindChildren(item, fNodeList);
                }
            }

            // 走出一层
            tier--;
        }

        //查找前缀空格数量
        private int FindPreSpace(String str)
        {
            List<char> chars = str.ToList();
            int count = 0;
            foreach (char c in chars)
            {
                if (c == ' ')
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }

        // 根据缩进找上级
        private Node FindParent(int space)
        {

            if (nodeList.Count == 0)
            {
                return null;
            }
            else
            {
                // 倒着找上级
                for (int i = nodeList.Count - 1; i >= 0; i--)
                {
                    Node node = nodeList[i];
                    if (node.Space < space)
                    {
                        return node;
                    }
                }
                // 如果没有找到 返回null
                return null;
            }
        }

        // 私有节点类
        private class Node
        {
            // 名称
            public String Name { get; set; }
            // 值
            public String Value { get; set; }
            // 父级
            public Node Parent { get; set; }
            // 前缀空格
            public int Space { get; set; }
            // 所属层级
            public int Tier { get; set; }
        }
    }
}
