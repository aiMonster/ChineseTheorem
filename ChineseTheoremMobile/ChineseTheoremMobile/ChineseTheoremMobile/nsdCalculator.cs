using System;
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
            bool tf; // Ùîá çíàòè â ÿê³é ïîñë³äîâíîñò³ âèâåñòè íà åêðàí q òà p

            // Ñòâîðþºìî ìàñèâè, ÿê³ áóäåìî âèêîðèñòîâóâàòè ÿê òèì÷àñîâ³ çì³íí³, òà çì³ííó ³, ùîá ìè ¿¿ çìîãëè âèâåñòè ï³ñëÿ öèêëó
            int[] ar = new int[50];
            int[] br = new int[50];
            int[] cr = new int[50];
            int[] dr = new int[50];
            int[] pr = new int[50];
            //int ar[50], br[50], cr[50], dr[50], pr[50];

            int i = 1;
            cr[0] = 1;

            // Äëÿ çðó÷í³øîãî êîèñòóâàííÿ ïèñâîþºìî ìàñèâó à[] á³ëüøå ÷èñëî, à ìàñèâó â[] ìåíøå ÷èñëî 
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


            // ßêùî îäíå ç ÷èñåë º ÍÑÄ ³íøîãî ÷èñëà, âèâîäèìî öå ÷èñëî
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
                return model;

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
                    i++;
                }
                nsd = cr[i - 2];

            }








            // Ä³îôàíòîâå ð³âíÿííÿ
            if (ar[1] % br[1] == 1) // ßêùî îñòà÷à ä³ëåííÿ îäíîãî ÷èñëà íà ³íøå = 1
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

                //tf == 1 ? p = 1 : p = (ar[1] / br[1]) / -1;
                //tf == 0 ? q = 1 : q = (ar[1] / br[1]) / -1;

            }
            else // Âèêîíóºìî çà ôîðìóëîþ ä³îôàíòîâîãî ð³âíÿííÿ
            {
                pr[1] = 1;
                pr[2] = dr[1];
                dr[i - 1] = 1;
                for (; i != 3; i--)

                {
                    pr[i - 2] = dr[i - 1] + dr[i - 2] * dr[i - 3];
                    pr[i - 3] = dr[i - 2];
                    dr[i - 3] = pr[i - 2];
                }

                // Ä³çíàºìîñü â ÿêîìó ïîðÿäêó áóäå ïðàâèëüíî âèâåñòè ÷èñëà íà åêðàí
                // Çàëåæèò â³ä òîãî ÿê êîðèñòóâà÷ ââ³â äàí³
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
    }
}
