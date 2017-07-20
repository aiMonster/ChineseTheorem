using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobile
{
    public class ToDBModel
    {
        public string name { get; set; }
        public string condition { get; set; }
        public string expression { get; set; }
        public bool status { get; set; }
        public DateTime date { get; set; }
    }
}
