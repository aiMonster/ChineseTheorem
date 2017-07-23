using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobileMVVM.Models
{
    public class NsdWithMModel
    {
        //amount of max elements our expression can get in one column
        private static int max = 5;

        public int amountOfElements { get; set; }
        public string condition { get; set; }
        public DateTime date { get; set; }
        public bool status { get; set; }

        public int P { get; set; }
        public int[] m = new int[max];
        public int[] M = new int[max];
        public int X { get; set; }

        public string P_solution { get; set; }
        public string[] m_solution = new string[max];
        public string[] M_solution_condition = new string[max];
        public OnlyNsdModel[] M_solution = new OnlyNsdModel[max];
        public string[] M_end_solution = new string[max];
        public string X_solution { get; set; }
    }
}
