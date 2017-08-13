using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChineseTheoremMobileMVVM.Models;
using ChineseTheoremMobileMVVM.Calculator;
using ChineseTheoremMobileMVVM.Converter;
using System.Windows.Input;

using Xamarin.Forms;

namespace ChineseTheoremMobileMVVM.ViewModels
{
    public class DecidePandQViewModel : INotifyPropertyChanged
    {
        //ICommand
        public ICommand DecideCommand { protected set; get; }

        public NumbersModel numbersModel { get; private set; }

        public DecidePandQViewModel()
        {
            numbersModel = new NumbersModel();
            this.DecideCommand = new Command(Decide);
        }

        private async void Decide()
        {

            if (!isValid())
            {
                return;
            }

            int a = 0, b = 0;
            try
            {
                a = Convert.ToInt32(numbersModel.number_a);
                b = Convert.ToInt32(numbersModel.number_b);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Too big numbers in condition!", "OK");
                return;
            }


            string condition = a + "p + " + b + "q";
            bool answer = await App.Current.MainPage.DisplayAlert("Is condition correct?", condition, "It's ok", "Cancel");
            if (!answer)
            {
                return;
            }

            //if not enough points - exit
            if (PointsViewModel.getInstance.Points < 4)
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Not enough points, price of that exercise 4!", "OK");
                return;
            }


            OnlyNsdModel resultExpressionModel = ChineseCalculator.Count_Nsd_p_q(a, b);
            ExpressionModel toDbModel = ConverterToExpressionModel.Convert(resultExpressionModel, false);

            if (resultExpressionModel.nsd_status)
            {
                //expression decided correct
                PointsViewModel.getInstance.Points -= 4;
            }
            else
            {
                //sorry something wrong
                await App.Current.MainPage.DisplayAlert("Oops, something wrong!", "We couldn't decide expression correct, so we did'n minus points, write to developer  with screenshot of that exercise", "OK");
            }

            //saving to db
            try
            {
                await App.Database.SaveItem(toDbModel);
            }
            catch
            {
                //and returning points
                await App.Current.MainPage.DisplayAlert("Oops, something wrong!", "We couldn't save expression to dataBase, write to developer", "OK");
                PointsViewModel.getInstance.Points += 4;
            }


            await App.Current.MainPage.DisplayAlert("Excellent!", "Expression added to History", "OK");
            Number_a = "";
            Number_b = "";

        }

        private bool isValid()
        {
            if (String.IsNullOrEmpty(Number_a) || String.IsNullOrEmpty(Number_b))
            {
                App.Current.MainPage.DisplayAlert("Oops!", "You did'n fill all cells!", "OK");
                return false;
            }
            else if (Number_a == "0" || Number_b == "0")
            {
                App.Current.MainPage.DisplayAlert("Oops!", "One of numbers is equal zero!", "OK");
                return false;
            }

            return true;
        }


        public string Number_a
        {
            get { return numbersModel.number_a; }
            set
            {
                if (numbersModel.number_a != value)
                {
                    numbersModel.number_a = value.Replace(".", "");
                    OnPropertyChanged("Number_a");
                }
            }
        }

        public string Number_b
        {
            get { return numbersModel.number_b; }
            set
            {
                if (numbersModel.number_b != value)
                {
                    numbersModel.number_b = value.Replace(".", "");
                    OnPropertyChanged("Number_b");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }



    }
}
