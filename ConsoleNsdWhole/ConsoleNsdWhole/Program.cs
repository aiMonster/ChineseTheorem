using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNsdWhole
{
    class Program
    {
        static void Main(string[] args)
        {
            int a1 = 5, b1 = 25;
            
            while(true)
            {
                nsdModel model = new nsdModel();
                Console.Write("\nEnter a: ");
                a1 = Convert.ToInt32(Console.ReadLine());
                //Console.WriteLine(a1);

                Console.Write("\nEnter b: ");
                b1 = Convert.ToInt32(Console.ReadLine());

                int p, q, nsd;
                bool tf; // to know how to display than p and q

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
                    Console.WriteLine(model);
                    continue;

                    //return nsd, p, q;
                }

                // Â ³íøîìó âèïàäêó ðàõóºìî ÍÑÄ òà âèâîäèìî öå ÷èñëî
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



                int[] ddr = new int[50];
                for(int ii = 0; ii < 50; ii++)
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

                    //tf == 1 ? p = 1 : p = (ar[1] / br[1]) / -1;
                    //tf == 0 ? q = 1 : q = (ar[1] / br[1]) / -1;

                }
                else // using formula for diofant expression
                {
                    pr[1] = 1;
                    pr[2] = dr[1];
                    dr[i - 1] = 1;
                    model.p_and_q_full += nsd + " = " + ar[i-2] + " - " + br[i-2] + "*" + Math.Abs(dr[i-2]); 
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
                        //model.p_and_q_full += " =\n= " + ar[i-2] + "*" + dr[i-1];
                        //model.p_and_q_full += " =\n= ";
                        //model.p_and_q_full += " =\n= ";
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
                Console.WriteLine(model);
                

            }

        }
    }
}
