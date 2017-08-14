using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AdminChineseMoblie.Models;
using AdminChineseMoblie.ViewModels;

namespace AdminChineseMoblie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PromoInfo : ContentPage
    {
        public PromoInfo(PromoCodeModel model)
        {
            InitializeComponent();            
            BindingContext = new PromoInfoViewModel(model);
        }
    }
}