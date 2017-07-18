using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNsdWhole
{
    public class nsdModel
    {

        public int p { get; set; }
        public int q { get; set; }
        public int nsd { get; set; }
        public string nsd_full { get; set; }
        public string p_and_q_full { get; set; }

        public override string ToString()
        {
            string result = "Our p = " + p + " and q = " + q + "\nnsd = " + nsd + "\n" + nsd_full + "\n" + p_and_q_full;
            return result;
        }
    }
}
