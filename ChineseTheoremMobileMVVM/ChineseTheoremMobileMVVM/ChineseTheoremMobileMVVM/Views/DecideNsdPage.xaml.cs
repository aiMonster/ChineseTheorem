﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ChineseTheoremMobileMVVM.ViewModels;

namespace ChineseTheoremMobileMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DecideNsdPage : ContentPage
    {
        public DecideNsdPage()
        {
            InitializeComponent();
            BindingContext = new DecideNsdViewModel();
        }
    }
}