using ChineseTheoremMobileMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;

namespace ChineseTheoremMobileMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]   
    public partial class PromoCodePage : ContentPage
    {
        public PromoCodePage()
        {
            
            PromoCodeViewModel pcv = new PromoCodeViewModel();
            base.Appearing += (o, e) => pcv.onAppearing();
            //base.Appearing += (o, e) => this.onApp();
                      
            BindingContext = pcv;
            InitializeComponent();
            onApp();
        }

        private void onApp()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                DisplayAlert("cai", "no internet", "ok");

            }
            else
                DisplayAlert("is", "internet", "ok");
        }
    }
}