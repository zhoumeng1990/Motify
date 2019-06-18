using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotifyPackage.utils
{
    class CommonUtil
    {
        public static Boolean IsEmpty(string str)
        {
            return str == null || str.Equals("");
        }
    }
}
