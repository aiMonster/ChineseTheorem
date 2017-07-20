using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChineseTheoremMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            expressionsList.ItemsSource = App.Database.GetItems();
            base.OnAppearing();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            //Friend selectedFriend = (Friend)e.SelectedItem;
            //FriendPage friendPage = new FriendPage();
            //friendPage.BindingContext = selectedFriend;
            //await Navigation.PushAsync(friendPage);
            await DisplayAlert("Caution", "selected", "ОK");            
        }
    }
}