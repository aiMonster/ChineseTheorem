using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChineseTheoremMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
        }

        private void btnDecide_Clicked(object sender, EventArgs e)
        {
            //amount rows in expression
            int amountElements = 0;
            foreach(var x in slRowsToCalculate.Children)
            {
                amountElements++;
            }

            //our numbers
            int[] numbers_b = new int[amountElements + 1];
            int[] numbers_p = new int[amountElements + 1];

            //////////////////////////////////// CHECKING /////////////////////////////////////

            //checking on free cells
            string tmp_b = "";
            string tmp_p = "";
            int rowCounter = 1;
            foreach (StackLayout row in slRowsToCalculate.Children)
            {
                int innerCounter = 0;                
                foreach (var num in row.Children)
                {
                    if (innerCounter == 1)
                    {
                        Entry tmpE = num as Entry;
                        tmp_b = tmpE.Text;
                        if(tmp_b == null || tmp_b == "" || tmp_b == "0")
                        {
                            DisplayAlert("Caution", "b" + Convert.ToString(rowCounter) + " - is not filled or = 0", "ОK");
                            return;
                        }
                    }
                    else if (innerCounter == 3)
                    {
                        Entry tmpE = num as Entry;
                        tmp_p = tmpE.Text;
                        if(tmp_p == null || tmp_p == "" || tmp_p == "0")
                        {
                            DisplayAlert("Caution", "p" + Convert.ToString(rowCounter) + " - is not filled or = 0", "ОK");
                            return;
                        }

                    }
                    innerCounter++;                    
                }

                numbers_b[rowCounter] = Convert.ToInt32(tmp_b);
                numbers_p[rowCounter] = Convert.ToInt32(tmp_p);
                //DisplayAlert("Caution", "In " + Convert.ToString(rowCounter) + "row our b = " + tmp_b + "and p = " + tmp_p, "ОK");
                rowCounter++;
            }
            //end checking on free cells
            
      
            //checking if b > p
            for(int i = 1; i <= amountElements; i++)
            {
                if(numbers_b[i] >= numbers_p[i])
                {
                    DisplayAlert("Caution", "b" + i + " >= p" + i + " is not allowed", "ОK");
                    return;
                }                
            }
            //end checking if b > p

            
            //checking on 'both primes'

            //here some code

            //end checking on 'both primes'

            ///////////////////////////////// END CHECKING //////////////////////////////////

            //deciding and writing to data base





        }

        private void btnRemoveRow_Clicked(object sender, EventArgs e)
        {
           
           
            int counter = 0;            
            foreach (var s in slRowsToCalculate.Children)
            {
                counter++;
            }
            
            if(counter <= 2)
            {
                DisplayAlert("Caution", "Can't delete anymore rows", "ОK");
            }
            else
            {
                slRowsToCalculate.Children.Remove(slRowsToCalculate.Children.Last());
                svForRowsToCalculate.ScrollToAsync(0, svForRowsToCalculate.Height, true);
            }
            
           
        }

        private void btnAddRow_Clicked(object sender, EventArgs e)
        {
            int counter = 0;
            foreach (var s in slRowsToCalculate.Children)
            {
                counter++;
            }

            if(counter >= 10)
            {
                DisplayAlert("Caution", "Can't add anymore rows", "ОK");
                return;
            }

            StackLayout SL = new StackLayout();
            SL.Orientation = StackOrientation.Horizontal;
            SL.HorizontalOptions = LayoutOptions.Center;
            SL.VerticalOptions = LayoutOptions.Center;

            Label lblX = new Label();
            lblX.Text = "X ≡ ";
            lblX.FontSize = Device.GetNamedSize(NamedSize.Large, lblX);
            lblX.VerticalOptions = LayoutOptions.Center;

            Entry enB = new Entry();
            enB.Placeholder = " b" + (counter + 1) + " ";
            enB.FontSize = Device.GetNamedSize(NamedSize.Large, enB);
            enB.Keyboard = Keyboard.Numeric;
            enB.BackgroundColor = Color.LightBlue;
            enB.VerticalOptions = LayoutOptions.Center;
            enB.TextChanged += (ss, ee) => Entry_TextChanged(ss, ee);

            Label lblMod = new Label();
            lblMod.Text = " mod ";
            lblMod.FontSize = Device.GetNamedSize(NamedSize.Large, lblX);
            lblMod.VerticalOptions = LayoutOptions.Center;

            Entry enP = new Entry();
            enP.Placeholder = " p" + (counter + 1) + " ";
            enP.FontSize = Device.GetNamedSize(NamedSize.Large, enB);
            enP.Keyboard = Keyboard.Numeric;
            enP.BackgroundColor = Color.LightBlue;
            enP.VerticalOptions = LayoutOptions.Center;
            enP.TextChanged += (ss, ee) => Entry_TextChanged(ss, ee);

            SL.Children.Add(lblX);
            SL.Children.Add(enB);
            SL.Children.Add(lblMod);
            SL.Children.Add(enP);

            slRowsToCalculate.Children.Add(SL);
            svForRowsToCalculate.ScrollToAsync(0, svForRowsToCalculate.Height, true);
        }

        private void btnFillByRandom_Clicked(object sender, EventArgs e)
        {
            
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry tmpE = sender as Entry;
            string replacedString = tmpE.Text.Replace(".", "");
            tmpE.Text = replacedString;
        }
    }
}