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
    public partial class OneExpressionPage : ContentPage
    {
        public OneExpressionPage(ExpressionModel model)
        {
            InitializeComponent();
            OneExpressionViewModel viewmodel = new OneExpressionViewModel(model);
            viewmodel.Navigation = Navigation; //to be able close that page by button
            BindingContext = viewmodel;
            editorSolution.Focused += (sender, e) => { editorSolution.Unfocus(); }; //to deny text editing
        }
    }
}