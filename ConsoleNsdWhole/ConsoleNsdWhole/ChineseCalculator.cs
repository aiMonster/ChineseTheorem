using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChineseTheoremMobile;

namespace ChineseTheoremMobile
{
    //all logic here I wrote when my experience in programming was no more than two months
    //so even I don't understand how I'm doing it

    //some comments from when i created this code:
    /* 
        How expression looks: a*p + b*q = 1
        How we displayed: a1*p + b1*q = 1

        For example: 2881 = 47*61 + 14
        We displayed it as: 2881*1= 47*61 + 14
                    ar[]*pr[] = br[]*dr[] + cr[] 
    */
    public static class ChineseCalculator
    {
        //gets two parameters and return p,q,nsd between them        
        public static OnlyNsdModel Count_Nsd_p_q(int a1, int b1)
        {
            OnlyNsdModel model = new OnlyNsdModel();

            int p, q, nsd; // numbers to return info
            bool tf; //  to know how to display than p and q

            // arrays to store data
            int[] ar = new int[50];
            int[] br = new int[50];
            int[] cr = new int[50];
            int[] dr = new int[50];
            int[] pr = new int[50];
            

            int i = 1;
            cr[0] = 1;

            //it will be better to store in a[]-bigger and in b[]-lower number
            if (a1 > b1)
            {
                ar[1] = a1;
                br[1] = b1;
                tf = true;
            }
            else
            {
                ar[1] = b1;
                br[1] = a1;
                tf = false;
            }


            // if one number is NSD of second - displaing it and finishing job
            if (ar[1] % br[1] == 0)
            {
                nsd = br[1];
               
                if (tf == true)
                {
                    p = 1;
                    q = (ar[1] / br[1] - 1) * -1;
                }
                else
                {
                    q = 1;
                    p = (ar[1] / br[1] - 1) * -1;
                }

                //returning current info
                model.p = p;
                model.q = q;
                model.nsd = nsd;
                model.nsd_solution += ar[1] + " = " + br[1] + "*" + br[1] + " + 0";
                model.p_and_q_solution += "In this case expression does't exist";

                //before returning checking is expression made correct
                {
                    if (a1 % model.nsd == 0 && b1 % model.nsd == 0)
                        model.nsd_status = true;
                    else
                        model.nsd_status = false;

                    if (a1 * model.p + b1 * model.q == model.nsd)
                        model.p_and_q_status = true;
                    else
                        model.p_and_q_status = false;

                    if (model.p_and_q_status && model.nsd_status)
                        model.final_status = true;
                    else
                        model.final_status = false;
                }

                return model;
            }

            // in another case calculating nsd and showing that number
            else
            {
                for (; cr[i - 1] != 0;)
                {
                    cr[i] = ar[i] % br[i];
                    dr[i] = ar[i] / br[i] / -1; // Just for next step
                    ar[i + 1] = br[i];
                    br[i + 1] = cr[i];
                    model.nsd_solution += ar[i] + " = " + br[i] + "*" + Math.Abs(dr[i]) + " + " + cr[i];

                    //in last row we should'n put NewLine
                    if(cr[i -1] > 1)
                    {
                        model.nsd_solution += "\n";
                    }

                    i++;
                }
                nsd = cr[i - 2];//getting nsd
            }


            //we will need it on next step to show how we decided expression
            int[] ddr = new int[50];
            for (int ii = 0; ii < 50; ii++)
            {
                ddr[ii] = dr[ii];
            }


            // diofant expression
            if (ar[1] % br[1] == 1) // if rest of dividing one number on anthter == 1
            {
                if (tf == true)
                {
                    p = 1;
                    q = (ar[1] / br[1]) / -1;
                }
                else
                {
                    p = (ar[1] / br[1]) / -1;
                    q = 1;
                }
                model.p_and_q_solution += "In this case expression does't exist";                

            }
            else // using formula for diofant expression
            {
                pr[1] = 1;
                pr[2] = dr[1];
                dr[i - 1] = 1;
                model.p_and_q_solution += nsd + " = " + ar[i - 2] + " - " + br[i - 2] + "*" + Math.Abs(dr[i - 2]);
                for (; i != 3; i--)

                {
                    pr[i - 2] = dr[i - 1] + dr[i - 2] * dr[i - 3];
                    pr[i - 3] = dr[i - 2];
                    dr[i - 3] = pr[i - 2];

                    //here we are writing our how we decided our expression
                    if (dr[i - 1] < 0)
                    {
                        model.p_and_q_solution += " =\n= " + Math.Abs(dr[i - 2]) + "*(" + ar[i - 3] + " - " + br[i - 3] + "*" + Math.Abs(ddr[i - 3]) + ")" + " - " + ar[i - 2] + "*" + Math.Abs(dr[i - 1]);
                        model.p_and_q_solution += " =\n= " + Math.Abs(dr[i - 2]) + "*" + ar[i - 3] + " - " + br[i - 3] + "*" + (Math.Abs(dr[i - 2]) * Math.Abs(ddr[i - 3])) + " - " + ar[i - 2] + "*" + Math.Abs(dr[i - 1]);
                        model.p_and_q_solution += " =\n= " + Math.Abs(dr[i - 2]) + "*" + ar[i - 3] + " - " + ((Math.Abs(dr[i - 2]) * Math.Abs(ddr[i - 3])) + Math.Abs(dr[i - 1])) + "*" + ar[i - 2];
                    }
                    else
                    {
                        model.p_and_q_solution += " =\n= " + ar[i - 2] + "*" + dr[i - 1] + " - " + Math.Abs(dr[i - 2]) + "*(" + ar[i - 3] + " - " + br[i - 3] + "*" + Math.Abs(ddr[i - 3]) + ")";
                        model.p_and_q_solution += " =\n= " + ar[i - 2] + "*" + dr[i - 1] + " - " + Math.Abs(dr[i - 2]) + "*" + ar[i - 3] + " + " + br[i - 3] + "*" + (Math.Abs(ddr[i - 3]) * Math.Abs(dr[i - 2]));
                        model.p_and_q_solution += " =\n= " + (dr[i - 1] + (Math.Abs(ddr[i - 3]) * Math.Abs(dr[i - 2]))) + "*" + ar[i - 2] + " - " + Math.Abs(dr[i - 2]) + "*" + ar[i - 3];
                    }
                }

                // depending on input data
                // choosig where is p and where is q
                if (tf == true)
                {
                    p = pr[1];
                    q = pr[2];
                }

                else
                {
                    p = pr[2];
                    q = pr[1];
                }
            }


            model.p = p;
            model.q = q;
            model.nsd = nsd;

            //before returning checking is expression made correct
            {
                if (a1 % model.nsd == 0 && b1 % model.nsd == 0)
                    model.nsd_status = true;
                else
                    model.nsd_status = false;

                if (a1 * model.p + b1 * model.q == model.nsd)
                    model.p_and_q_status = true;
                else
                    model.p_and_q_status = false;

                if (model.p_and_q_status && model.nsd_status)
                    model.final_status = true;
                else
                    model.final_status = false;
            }

            return model;

        }

        //gets two arrays of data !!!!- start count from 1, not from 0 -!!!!
        public static NsdWithMModel Count_Nsd_with_M(int[] numbers_b, int[] numbers_p, int amount)
        {
            NsdWithMModel finalModel = new NsdWithMModel();
            finalModel.amountOfElements = amount;
            
            int P = 1;
            int[] numbers_m = new int[amount + 1];
            int[] numbers_M = new int[amount + 1];

            string tmpEmpression = "";
            finalModel.P_solution += "P = ";
            //finalModel.name = "Chinese";
            for (int i = 1; i <= amount; i++)
            {

                if (i < amount)
                {
                    finalModel.P_solution += "p" + i + "*";
                    tmpEmpression += numbers_p[i] + "*";
                    finalModel.condition += "X ≡ " + numbers_b[i] + " mod " + numbers_p[i] + "\n";
                }
                else
                {
                    finalModel.P_solution += "p" + i + " = ";
                    tmpEmpression += numbers_p[i] + " = ";
                    finalModel.condition += "X ≡ " + numbers_b[i] + " mod " + numbers_p[i];
                }
                P *= numbers_p[i];
            }
            finalModel.P_solution += tmpEmpression + P;

            for (int i = 1; i <= amount; i++)
            {
                numbers_m[i] = P / numbers_p[i];
                finalModel.m_solution[i] += "m" + i + " = P/p" + i + " = " + P + "/" + numbers_p[i] + " = ";
                for (int ii = 1; ii <= amount; ii++)
                {
                    if (ii != i)
                    {
                        finalModel.m_solution[i] += numbers_p[ii] + "*";
                    }
                }
                finalModel.m_solution[i] = finalModel.m_solution[i].Remove(finalModel.m_solution[i].Length - 1);                
                finalModel.m_solution[i] += " = " + numbers_m[i];
                

            }
            //finalModel.expression += "\n";


            for (int i = 1; i <= amount; i++)
            {
                finalModel.M_solution_condition[i] += "M" + i + " = m" + i + "^(-1) mod p" + i + " = " + numbers_m[i] + "^(-1) mod " + numbers_p[i];
                //if(i < amount)
                //{
                //    finalModel.M_solution_condition[i] += "\n";
                //}
            }
            //finalModel.expression += "\n";

            for (int i = 1; i <= amount; i++)
            {
                //finalModel.expression += "M" + i + ":\n";

                OnlyNsdModel nsdModel = new OnlyNsdModel();
                nsdModel = ChineseCalculator.Count_Nsd_p_q(numbers_m[i], numbers_p[i]);
                finalModel.M_solution[i] = nsdModel;
                //finalModel.expression += nsdModel.nsd_full + "\n";
                //finalModel.expression += nsdModel.p_and_q_full;
                //finalModel.expression += "\np = " + nsdModel.p + "  q = " + nsdModel.q + "\n\n";

                if (nsdModel.q < 0)
                {
                    finalModel.M_end_solution[i] += "(" + nsdModel.p + "*" + numbers_m[i] + " mod " + numbers_p[i] + " - " + Math.Abs(nsdModel.q) + "*" + numbers_p[i] + " mod " + numbers_p[i] + ") mod " + numbers_p[i] + " = 1 mod " + numbers_p[i] + "\n";
                }
                else
                {
                    finalModel.M_end_solution[i] += "(" + nsdModel.q + "*" + numbers_p[i] + "mod " + numbers_p[i] + " - " + Math.Abs(nsdModel.p) + "*" + numbers_m[i] + "mod " + numbers_p[i] + ") mod " + numbers_p[i] + " = 1 mod " + numbers_p[i] + "\n";

                }

                finalModel.M_end_solution[i] += nsdModel.p + "*" + numbers_m[i] + " mod " + numbers_p[i] + " = 1 mod " + numbers_p[i] + "     /" + numbers_m[i] + "\n";

                int tmpP = nsdModel.p;
                while (tmpP <= 0)
                {
                    finalModel.M_end_solution[i] += tmpP + " mod " + numbers_p[i] + " = " + numbers_m[i] + "^(-1) mod " + numbers_p[i] + "\n";
                    tmpP += numbers_p[i];
                }
                finalModel.M_end_solution[i] += tmpP + " mod " + numbers_p[i] + " = " + numbers_m[i] + "^(-1) mod " + numbers_p[i];
                finalModel.M[i] = tmpP;
                finalModel.m[i] = numbers_m[i];
                numbers_M[i] = tmpP;


                //finalModel.expression += "\n";
            }

            finalModel.X_solution += "X = (";

            for (int i = 1; i <= amount; i++)
            {
                finalModel.X_solution += "(m" + i + "*b" + i + "*M" + i + ")";
                if (i < amount)
                {
                    finalModel.X_solution += " + ";
                }
            }
            finalModel.X_solution += ")mod P\n";

            long X = 0;
            finalModel.X_solution += "X = (";
            for (int i = 1; i <= amount; i++)
            {
                X += numbers_m[i] * numbers_b[i] * numbers_M[i];
                finalModel.X_solution += "(" + numbers_m[i] + " *" + numbers_b[i] + "*" + numbers_M[i] + ")";
                if (i < amount)
                {
                    finalModel.X_solution += " + ";
                }
            }
            finalModel.X_solution += ")mod " + P + " = \n = (";


            for (int i = 1; i <= amount; i++)
            {
                finalModel.X_solution += numbers_m[i] * numbers_b[i] * numbers_M[i];
                if (i < amount)
                {
                    finalModel.X_solution += " + ";
                }
            }

            finalModel.X_solution += ") mod " + P + " =\n = " + X + " mod " + P + "\n";
            //finalModel.X_solution += "X = " + X % P + "\n";
            finalModel.X = System.Convert.ToInt32(X) % P;
            finalModel.date = DateTime.Now;

            finalModel.status = true;
            for (int i = 1; i <= amount; i++)
            {
                if (X % numbers_p[i] != numbers_b[i])
                    finalModel.status = false;
            }


            return finalModel;

        }

        public static ExpressionModel Convert(OnlyNsdModel origin, bool isItOnlyNsd)
        {

            ExpressionModel result = new ExpressionModel();

            result.status = (origin.final_status) ? "Expression made OK" : "Expression FAILED";
            result.date = DateTime.Now;

            if (isItOnlyNsd)
            {
                result.condition = "a = " + origin.a + ", b = " + origin.b;
                result.name = "Only NSD";
                result.solution += origin.nsd_solution + "\n\n" + "NSD = " + origin.nsd;
            }
            else
            {
                result.condition = origin.a + "p + " + origin.b + "q";
                result.name = "NSD, p and q";
                result.solution += origin.nsd_solution + "\n\n" + "NSD = " + origin.nsd + "\n\n\n" + origin.p_and_q_solution + "\n\n";
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

            for (int i = 1; i <= origin.amountOfElements; i++)
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
                result.solution += ChineseCalculator.Convert(origin.M_solution[i], false).solution + "\n\n";
                result.solution += origin.M_end_solution[i] + "\nM" + i + "= " + origin.M[i] + "\n\n\n";
            }

            result.solution += origin.X_solution + "\n\nX = " + origin.X;

            return result;
        }


    }

    //public static class ConverterToExpressionModel
    //{
    //    public static ExpressionModel Convert(OnlyNsdModel origin)
    //    {

    //        ExpressionModel result = new ExpressionModel();


    //        return result;
    //    }

    //    public static ExpressionModel Convert(NsdWithMModel origin)
    //    {
    //        ExpressionModel result = new ExpressionModel();


    //        return result;
    //    }
    //}
}
