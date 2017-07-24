using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChineseTheoremMobileMVVM.Models;

namespace ChineseTheoremMobileMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HystoryPage : ContentPage
    {
        public HystoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            List<ExpressionModel> tmpMod = new List<ExpressionModel>(App.Database.GetItems());
            List<ExpressionModel> Mod = new List<ExpressionModel>();
            foreach (var a in tmpMod)
            {
                Mod.Insert(0, a);
            }
            expressionsList.ItemsSource = Mod;
            base.OnAppearing();
        }
    }
}