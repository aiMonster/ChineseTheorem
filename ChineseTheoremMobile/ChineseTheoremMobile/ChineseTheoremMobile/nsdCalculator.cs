﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobile
{
    public static class nsdCalculator
    {
        public static expressionModel Count(int a1, int b1)
        {
            expressionModel model = new expressionModel();

            int p, q, nsd;
            bool tf; //  to know how to display than p and q

            // arrays to store data
            int[] ar = new int[50];
            int[] br = new int[50];
            int[] cr = new int[50];
            int[] dr = new int[50];
            int[] pr = new int[50];
            //int ar[50], br[50], cr[50], dr[50], pr[50];

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

                //tf == true ? (p = 1) : (q = 1);
                //tf == 0 ? p = (ar[1] / br[1] - 1) * -1 : q = (ar[1] / br[1] - 1) * -1;
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


                model.p = p;
                model.q = q;
                model.nsd = nsd;
                model.nsd_full += ar[1] + " = " + br[1] + "*" + br[1] + " + 0";
                model.p_and_q_full += "In this case expression does't exist";
                return model;

                //return nsd, p, q;
            }

            // in another case calculation nsd and showing that number
            else
            {
                for (; cr[i - 1] != 0;)
                {
                    cr[i] = ar[i] % br[i];
                    dr[i] = ar[i] / br[i] / -1; // Just for next step
                    ar[i + 1] = br[i];
                    br[i + 1] = cr[i];
                    model.nsd_full += ar[i] + " = " + br[i] + "*" + Math.Abs(dr[i]) + " + " + cr[i] + "\n";
                    i++;
                }
                nsd = cr[i - 2];

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
                if(tf == true)
                {
                    p = 1;
                    q = (ar[1] / br[1]) / -1;
                }
                else
                {
                    p = (ar[1] / br[1]) / -1;
                    q = 1;
                }
                model.p_and_q_full += "In this case expression does't exist";
                //tf == 1 ? p = 1 : p = (ar[1] / br[1]) / -1;
                //tf == 0 ? q = 1 : q = (ar[1] / br[1]) / -1;

            }
            else // using formula for diofant expression
            {
                pr[1] = 1;
                pr[2] = dr[1];
                dr[i - 1] = 1;
                model.p_and_q_full += nsd + " = " + ar[i - 2] + " - " + br[i - 2] + "*" + Math.Abs(dr[i - 2]);
                for (; i != 3; i--)

                {
                    pr[i - 2] = dr[i - 1] + dr[i - 2] * dr[i - 3];
                    pr[i - 3] = dr[i - 2];
                    dr[i - 3] = pr[i - 2];

                    //here we are writing our how we decided our expression
                    if (dr[i - 1] < 0)
                    {
                        model.p_and_q_full += " =\n= " + Math.Abs(dr[i - 2]) + "*(" + ar[i - 3] + " - " + br[i - 3] + "*" + Math.Abs(ddr[i - 3]) + ")" + " - " + ar[i - 2] + "*" + Math.Abs(dr[i - 1]);
                        model.p_and_q_full += " =\n= " + Math.Abs(dr[i - 2]) + "*" + ar[i - 3] + " - " + br[i - 3] + "*" + (Math.Abs(dr[i - 2]) * Math.Abs(ddr[i - 3])) + " - " + ar[i - 2] + "*" + Math.Abs(dr[i - 1]);
                        model.p_and_q_full += " =\n= " + Math.Abs(dr[i - 2]) + "*" + ar[i - 3] + " - " + ((Math.Abs(dr[i - 2]) * Math.Abs(ddr[i - 3])) + Math.Abs(dr[i - 1])) + "*" + ar[i - 2];
                    }
                    else
                    {
                        model.p_and_q_full += " =\n= " + ar[i - 2] + "*" + dr[i - 1] + " - " + Math.Abs(dr[i - 2]) + "*(" + ar[i - 3] + " - " + br[i - 3] + "*" + Math.Abs(ddr[i - 3]) + ")";
                        model.p_and_q_full += " =\n= " + ar[i - 2] + "*" + dr[i - 1] + " - " + Math.Abs(dr[i - 2]) + "*" + ar[i - 3] + " + " + br[i - 3] + "*" + (Math.Abs(ddr[i - 3]) * Math.Abs(dr[i - 2]));
                        model.p_and_q_full += " =\n= " + (dr[i - 1] + (Math.Abs(ddr[i - 3]) * Math.Abs(dr[i - 2]))) + "*" + ar[i - 2] + " - " + Math.Abs(dr[i - 2]) + "*" + ar[i - 3];
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
            
            return model;           
           
        }

        public static ToDBModel CountWithM(int[] numbers_b, int[] numbers_p, int amount)
        {
            ToDBModel finalModel = new ToDBModel();
            int P = 1;
            int[] numbers_m = new int[amount+1];
            int[] numbers_M = new int[amount + 1];

            string tmpEmpression = "";
            finalModel.expression += "P = ";
            for(int i = 1; i <= amount; i++)
            {
                finalModel.condition += "X ≡ " + numbers_b[i] + " mod " + numbers_p[i] + "\n";
                if(i < amount)
                {
                    finalModel.expression += "p" + i + "*";
                    tmpEmpression += numbers_p[i] + "*";
                }
                else
                {
                    finalModel.expression += "p" + i + " = ";
                    tmpEmpression += numbers_p[i] + " = ";
                }
                P *= numbers_p[i];
            }
            finalModel.expression += tmpEmpression + P + ";\n\n";

            for(int i = 1; i <= amount; i++)
            {
                numbers_m[i] = P / numbers_p[i];
                finalModel.expression += "m" + i + " = P/p" + i + " = " + P + "/" + numbers_p[i] + " = ";
                for(int ii = 1; ii <= amount; ii++)
                {
                    if(ii != i)
                    {
                        finalModel.expression += numbers_p[ii] + "*";
                    }
                }
                finalModel.expression = finalModel.expression.Remove(finalModel.expression.Length - 1);
                finalModel.expression += " = " + numbers_m[i] + ";\n";

            }
            finalModel.expression += "\n";


            for(int i = 1; i <= amount; i++)
            {
                finalModel.expression += "M" + i + " = m" + i + "^(-1) mod p" + i + " = " + numbers_m[i] + "^(-1) mod " + numbers_p[i] + ";\n";
            }
            finalModel.expression += "\n";

            for(int i = 1; i <= amount; i++)
            {
                finalModel.expression += "M" + i + ":\n";

                expressionModel eModel = new expressionModel();
                eModel = nsdCalculator.Count(numbers_m[i], numbers_p[i]);

                finalModel.expression += eModel.nsd_full + "\n";
                finalModel.expression += eModel.p_and_q_full;
                finalModel.expression += "\np = " + eModel.p + "  q = " + eModel.q + "\n\n";

                if (eModel.q < 0)
                {
                    finalModel.expression += "(" + eModel.p + "*" + numbers_m[i] + " mod " + numbers_p[i] + " - " + Math.Abs(eModel.q) + "*" + numbers_p[i] + " mod " + numbers_p[i] + ") mod " + numbers_p[i] + " = 1 mod " + numbers_p[i] + "\n";
                }
                else
                {
                    finalModel.expression += "(" + eModel.q + "*" + numbers_p[i] + "mod " + numbers_p[i] + " - " + Math.Abs(eModel.p) + "*" + numbers_m[i] + "mod " + numbers_p[i] + ") mod " + numbers_p[i] + " = 1 mod " + numbers_p[i] + "\n";

                }

                finalModel.expression += eModel.p + "*" + numbers_m[i] + " mod " + numbers_p[i] + " = 1 mod " + numbers_p[i] + "     /" + numbers_m[i] + "\n";

                int tmpP = eModel.p;
                while(tmpP <= 0)
                {
                    finalModel.expression += tmpP + " mod " + numbers_p[i] + " = " + numbers_m[i] + "^(-1) mod " + numbers_p[i] + "\n";
                    tmpP += numbers_p[i];
                }
                finalModel.expression += tmpP + " mod " + numbers_p[i] + " = " + numbers_m[i] + "^(-1) mod " + numbers_p[i] + "\n";
                finalModel.expression += "M" + i + " = " + tmpP + "\n";
                numbers_M[i] = tmpP;


                finalModel.expression += "\n";
            }

            finalModel.expression += "X = (";

            for(int i = 1; i <= amount; i++)
            {
                finalModel.expression += "(m" + i + "*b" + i + "*M" + i + ")";
                if(i < amount)
                {
                    finalModel.expression += " + ";
                }
            }
            finalModel.expression += ")mod P\n";

            long X = 0;
            finalModel.expression += "X = (";
            for (int i = 1; i <= amount; i++)
            {
                X += numbers_m[i] * numbers_b[i] * numbers_M[i];
                finalModel.expression += "(" + numbers_m[i] + " *" + numbers_b[i] + "*" + numbers_M[i] + ")";
                if (i < amount)
                {
                    finalModel.expression += " + ";
                }
            }
            finalModel.expression += ")mod " + P + " = \n = (";


            for(int i = 1; i <= amount; i++)
            {
                finalModel.expression += numbers_m[i] * numbers_b[i] * numbers_M[i];
                if (i < amount)
                {
                    finalModel.expression += " + ";
                }
            }                          
                
            finalModel.expression += ") mod " + P + " =\n = " + X + " mod " + P + "\n";
            finalModel.expression += "X = " + X % P + "\n";
            finalModel.date = DateTime.Now;

            finalModel.status = true;
            for(int i = 1; i <= amount; i++)
            {
                if (X % numbers_p[i] != numbers_b[i])
                    finalModel.status = false;
            }


            return finalModel;
        }
    }
}