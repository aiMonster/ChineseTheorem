﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobileMVVM.DatabaseController
{
    //interface for dependency
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
