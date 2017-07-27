using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using ChineseTheoremMobileMVVM.DatabaseController;

namespace ChineseTheoremMobileMVVM
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "expressions.db";
        public static ExpressionRepositoryAsync database;
        public static ExpressionRepositoryAsync Database
        {
            get
            {
                if (database == null)
                {
                    database = new ExpressionRepositoryAsync(DATABASE_NAME);
                }
                return database;
            }
        }



        public App()
        {
            InitializeComponent();

            MainPage = new ChineseTheoremMobileMVVM.Views.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
