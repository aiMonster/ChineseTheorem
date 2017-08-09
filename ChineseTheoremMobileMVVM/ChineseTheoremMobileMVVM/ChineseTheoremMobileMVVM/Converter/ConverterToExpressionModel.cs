using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChineseTheoremMobileMVVM.Models;


namespace ChineseTheoremMobileMVVM.Converter
{
    public static class ConverterToExpressionModel
    {
        public static ExpressionModel Convert(OnlyNsdModel origin, bool isItOnlyNsd)
        {

            ExpressionModel result = new ExpressionModel();            
            
            result.status = (origin.final_status) ? "Expression made OK" : "Expression FAILED";
            result.date = DateTime.Now;

            if(isItOnlyNsd)
            {
                result.condition = "a = " + origin.a + ", b = " + origin.b;
                result.name = "Only NSD";
                result.solution += origin.nsd_solution + "NSD = " + origin.nsd;
            }
            else
            {
                result.condition = origin.a + "p + " + origin.b + "q";
                result.name = "NSD, p and q";
                result.solution += origin.nsd_solution + "NSD = " + origin.nsd + "\n\n\n" + origin.p_and_q_solution + "\n\n";
                result.solution += "p = " + origin.p + ", q = " + origin.q;

            }         

            return result;
        }

        public static ExpressionModel Convert(NsdWithMModel origin)
        {
            ExpressionModel result = new ExpressionModel();

            result.name = "Chinese theorem";
            result.status = (origin.status) ? "Expression made OK" : "Expression FAILED";
            result.date = DateTime.Now;
            result.condition = origin.condition;

            result.solution += origin.P_solution + "\n\n";

            for(int i = 1; i <= origin.amountOfElements; i++)
            {
                result.solution += origin.m_solution[i] + "\n";
            }
            result.solution += "\n\n";

            for (int i = 1; i <= origin.amountOfElements; i++)
            {
                result.solution += origin.M_solution_condition[i] + "\n";
            }
            result.solution += "\n\n";


            for (int i = 1; i <= origin.amountOfElements; i++)
            {
                result.solution += "M" + i + ":\n\n";
                result.solution += ConverterToExpressionModel.Convert(origin.M_solution[i], false).solution + "\n\n";
                result.solution += origin.M_end_solution[i] + "\nM" + i + "= " + origin.M[i] + "\n\n\n";
            }
            result.solution += origin.X_solution + "\n\nX = " + origin.X;
           
            return result;
        }
    }
}
