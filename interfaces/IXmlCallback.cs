﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotifyPackage.interfaces
{
    interface IXmlCallback
    {
        void ModifyPackageNameEnd();
        void ModifyIcon(string iconName);
        void ModifyAppNameEnd();
    }
}
