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
    public partial class GetInfoPage : ContentPage
    {
        public GetInfoPage()
        {
            InitializeComponent();
            GetInfoViewModel model = new GetInfoViewModel();
            model.Navigation = this.Navigation;
            BindingContext = model;
        }
    }
}