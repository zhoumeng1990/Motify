using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModifyPackage.utils
{
    class CommonUtil
    {
        public static Boolean IsEmpty(string str)
        {
            return str == null || str.Equals("");
        }
    }
}
