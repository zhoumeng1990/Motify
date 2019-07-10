using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModifyPackage.entify
{
    class Node
    {
        // 名称
        public String NodeName { get; set; }
        // 值
        public String NodeValue { get; set; }
        // 父级
        public Node ParentNode { get; set; }
        // 前缀空格
        public int Space { get; set; }
        // 所属层级
        public int Tier { get; set; }
    }
}
