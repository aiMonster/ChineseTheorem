using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AdminChineseMoblie.ViewModels;

namespace AdminChineseMoblie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetPromoPage : ContentPage
    {
        public GetPromoPage()
        {
            InitializeComponent();
            BindingContext = new GetPromoViewModel();
        }
    }
}