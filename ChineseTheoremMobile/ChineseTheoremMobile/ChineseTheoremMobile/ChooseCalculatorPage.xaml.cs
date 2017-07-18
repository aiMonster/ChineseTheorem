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
    public partial class ChooseCalculatorPage : TabbedPage
    {
        public ChooseCalculatorPage()
        {
            InitializeComponent();
        }

        //private async void btnChinese_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new CalculatorPage());
        //}
    }
}