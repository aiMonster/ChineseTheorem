using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobile
{
    public class OnlyNsdModel
    {
        public int a { get; set; }
        public int b { get; set; }

        public int p { get; set; }
        public int q { get; set; }
        public int nsd { get; set; }

        public string nsd_solution { get; set; }
        public string p_and_q_solution { get; set; }

        public bool nsd_status { get; set; }
        public bool p_and_q_status { get; set; }

        public bool final_status { get; set; }
    }
}
