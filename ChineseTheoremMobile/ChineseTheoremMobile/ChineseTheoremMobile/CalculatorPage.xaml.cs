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

            SL.Children.Add(lblX);
            SL.Children.Add(enB);
            SL.Children.Add(lblMod);
            SL.Children.Add(enP);

            slRowsToCalculate.Children.Add(SL);
            svForRowsToCalculate.ScrollToAsync(0, svForRowsToCalculate.Height, true);
        }
    }
}