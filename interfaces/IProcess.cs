﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModifyPackage.control
{
    interface IProcess
    {
        //获取别名结束
        void GetAliasEnd(string alias);
        //反编译结束
        void DecodeEnd();
        //打包结束
        void BuildEnd();
        //签名完成的回调
        void SignerEnd();
        //继续执行反编译
        void GoOnDecode();
    }
}
