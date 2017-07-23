using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChineseTheoremMobile;

namespace ConsoleNsdWhole
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a1 = {0,31,25,28};
            int[] b1 = { 0, 61, 63, 64 };
            
            while(true)
            {
                //OnlyNsdModel model = new OnlyNsdModel();
                NsdWithMModel model = new NsdWithMModel();
                //model = ChineseCalculator.Count_Nsd_with_M(a1, b1, 3);
                model = ChineseCalculator.Count_Nsd_with_M(a1, b1, 3);

                ExpressionModel em = ChineseCalculator.Convert(model);
                Console.WriteLine(em.condition);








                Console.ReadLine();
            }

        }
    }
}
