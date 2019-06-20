﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotifyPackage.control
{
    interface IProcess
    {
        //获取别名结束
        void GetAliasEnd(string alias);
        //反编译结束
        void DosEnd();
        //打包结束
        void BuildEnd();
    }
}
