using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChineseTheoremMobile;

namespace ChineseTheoremMobile
{
    public class ExpressionModel
    {
        public string name { get; set; }
        public string condition { get; set; }
        public string solution { get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }
    }
}
