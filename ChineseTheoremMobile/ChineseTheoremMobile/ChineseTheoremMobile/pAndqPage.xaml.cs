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
    public partial class pAndqPage : ContentPage
    {
        public pAndqPage()
        {
            InitializeComponent();
        }

        
        private async void btnDecide_Clicked(object sender, EventArgs e)
        {
            if (entryA.Text == null || entryA.Text == "" || entryA.Text == "0")
            {
                await DisplayAlert("Caution", "a - is not filled or = 0", "ОK");
                return;
            }
            else if (entryB.Text == null || entryB.Text == "" || entryB.Text == "0")
            {
                await DisplayAlert("Caution", "b - is not filled or = 0", "ОK");
                return;
            }
        
    }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry tmpE = sender as Entry;
            string replacedString = tmpE.Text.Replace(".", "");
            tmpE.Text = replacedString;
        }
    }
}