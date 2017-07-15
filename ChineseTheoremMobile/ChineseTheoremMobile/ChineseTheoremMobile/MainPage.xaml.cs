using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChineseTheoremMobile
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            setLanguage(LanguageLibrary.Ukrainian());
        }

        public void setLanguage(LanguageModel model)
        {
            BindingContext = new LanguageController(model);
        }
    }
}
