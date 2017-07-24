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

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {


            //Friend selectedFriend = (Friend)e.SelectedItem;
            //FriendPage friendPage = new FriendPage();
            //friendPage.BindingContext = selectedFriend;
            //await Navigation.PushAsync(friendPage);



            if (e.SelectedItem != null)
            {
                ExpressionModel model = (ExpressionModel)e.SelectedItem;
                int id = model.Id;

                string toShow = "";

                toShow += model.status + "\n\n";


                toShow += model.condition + "\n\n" + model.solution;

                await DisplayAlert("Result:", toShow, "CLOSE");
                //App.Database.SaveItem(mm);
                ((ListView)sender).SelectedItem = null;
                //App.Database.DeleteItem(id);
            }

        }
    }
}