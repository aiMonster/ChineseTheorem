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
    public partial class OnlyNsdPage : ContentPage
    {
        public OnlyNsdPage()
        {
            InitializeComponent();
        }

        private async void btnDecide_Clicked(object sender, EventArgs e)
        {
            
            if (entryA.Text == null || entryA.Text == "" || entryA.Text == "0")
            {
                await DisplayAlert("Caution", "a - is not filled or = 0", "ОK");
                return;
            }
            else if (entryB.Text == null || entryB.Text == "" || entryB.Text == "0")
            {
                await DisplayAlert("Caution", "b - is not filled or = 0", "ОK");
                return;
            }

            int a = Convert.ToInt32(entryA.Text);
            int b = Convert.ToInt32(entryB.Text);



            string condition = "a = " + a + "   b = " + b;
            bool answer = await DisplayAlert("Is expression correct?", condition, "It's ok", "Cancel");

            if(!answer)
            {
                return;
            }


           

            ToDBModel dbModel = new ToDBModel();
            expressionModel eModel = new expressionModel();
            
            eModel = nsdCalculator.Count(a, b);

            dbModel.name = "Only NSD";
            dbModel.condition = condition;
            dbModel.date = DateTime.Now;
            dbModel.expression = eModel.nsd_full + "\nNSD = " + eModel.nsd;

            if(a%eModel.nsd == 0 && b%eModel.nsd == 0)
            {
                dbModel.status = true;
            }
            else
            {

                await DisplayAlert("Caution", "Sorry, we could'n decide it right, write to developer with screenshoot of history", "ОK");                
                dbModel.status = false;
                //return;
            }

            PointsController p = PointsController.getInstance();
            if (p.IntPoints < 1)
            {
                await DisplayAlert("Caution", "Not enough points", "ОK");
                return;
            }

            bool result = DBSaver.Save(dbModel);
            
            if (result == false)
            {
                await DisplayAlert("Caution", "Sorry happend something wrong with writing to db, try to reinstall application", "ОK");
                return;
            }

            await DisplayAlert("Caution", "We successfully added your expression to History", "ОK");

            if (dbModel.status)
            {
                p.IntPoints--;
            }

            string toShow = "";
            toShow += dbModel.name + "\n";
            toShow += dbModel.condition + "\n";
            toShow += dbModel.expression + "\n";
            toShow += dbModel.date + "\n";
            toShow += dbModel.status + "\n";

            await DisplayAlert("Caution", toShow, "ОK");

            entryA.Text = "";
            entryB.Text = "";

        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry tmpE = sender as Entry;
            string replacedString = tmpE.Text.Replace(".", "");
            tmpE.Text = replacedString;
        }
    }
}