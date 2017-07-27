using ChineseTheoremMobileMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChineseTheoremMobileMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DecideChinesePage : ContentPage
    {
        public DecideChinesePage()
        {
            InitializeComponent();
            BindingContext = new DecideChineseViewModel();
        }
    }
}