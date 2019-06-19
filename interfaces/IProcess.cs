using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotifyPackage.control
{
    interface IProcess
    {
        //反编译结束
        void DosEnd();
        //打包结束
        void BuildEnd();
    }
}
