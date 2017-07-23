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
    public partial class InstructionPage : ContentPage
    {
        

        public InstructionPage()
        {
            InitializeComponent();            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PromoCodePage());
    
        }

        //private void btnEnglish_Clicked(object sender, EventArgs e)
        //{
        //    btnUkrainian.IsEnabled = true;
        //    btnEnglish.IsEnabled = false;
        //    BindingContext = new LanguageController(LanguageLibrary.English());
        //}

        //private void btnUkrainian_Clicked(object sender, EventArgs e)
        //{
        //    btnUkrainian.IsEnabled = false;
        //    btnEnglish.IsEnabled = true;
        //    BindingContext = new LanguageController(LanguageLibrary.Ukrainian());
        //}
    }
}