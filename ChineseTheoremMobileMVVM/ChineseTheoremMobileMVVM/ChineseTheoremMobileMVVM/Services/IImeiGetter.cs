using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobileMVVM.Services
{
    //interface for imei dependency
    public interface IImeiGetter
    {
        string GetImei();
    }
}
