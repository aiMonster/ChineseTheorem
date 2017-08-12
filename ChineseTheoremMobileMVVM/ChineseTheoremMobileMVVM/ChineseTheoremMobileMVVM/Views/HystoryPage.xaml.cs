using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChineseTheoremMobileMVVM.Models;
using ChineseTheoremMobileMVVM.ViewModels;

namespace ChineseTheoremMobileMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HystoryPage : ContentPage
    {
        public HystoryPage()
        {
            
            HistoryViewModel hm = new HistoryViewModel();
            base.Appearing +=(o,e)=> hm.OnAppearing(o, e);  //adding onAppearing event to update our list                      
            BindingContext = hm;
            InitializeComponent();
        }

        //when item selected open this expression on new page
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ExpressionModel model = (ExpressionModel)e.SelectedItem;
                ((ListView)sender).SelectedItem = null;
                await Navigation.PushModalAsync(new OneExpressionPage(model));
            }

        }
    }
}