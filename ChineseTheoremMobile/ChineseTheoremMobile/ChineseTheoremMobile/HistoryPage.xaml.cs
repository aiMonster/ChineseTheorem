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
            List<DBTableModel> tmpMod = new List<DBTableModel>(App.Database.GetItems());
            List<DBTableModel> Mod = new List<DBTableModel>();
            foreach(var a in tmpMod)
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
                DBTableModel model = (DBTableModel)e.SelectedItem;
                int id = model.Id;

                string toShow = "";
                if(model.status)
                {
                    toShow += "Expression made CORRECT\n\n";
                }
                else
                {
                    toShow += "Expression made INCORRECT\n\n";
                }

                toShow += model.condition + "\n\n" + model.expression;

                await DisplayAlert("Result:", toShow, "CLOSE");
                //App.Database.SaveItem(mm);
                ((ListView)sender).SelectedItem = null;
                //App.Database.DeleteItem(id);
            }
                

                      
        }
    }
}