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
    public class DecideChineseViewModel : INotifyPropertyChanged
    {
        static int amount = 3; //means that we will have 3 elements
        List<NumbersModel> dataList { get; set; }
        public ICommand DecideCommand { get; protected set; }

        public DecideChineseViewModel()
        {
            this.DecideCommand = new Command(Decide);
            dataList = new List<NumbersModel>();
            for(int i = 1; i <= amount; i++)
            {
                dataList.Add(new NumbersModel());
            }
        }

        private async void Decide()
        {
            if(!isValid())
            {
                return;
            }

            // !!!! starting fill array from 1 !!!!!
            //creating array from list for next work
            int[] numbers_b = new int[dataList.Count + 1];
            int[] numbers_p = new int[dataList.Count + 1];
            //and filling them
            try
            {
                int tmp_i = 1;
                foreach (var tempModel in dataList)
                {
                    numbers_b[tmp_i] = Convert.ToInt32(tempModel.number_a);
                    numbers_p[tmp_i] = Convert.ToInt32(tempModel.number_b);
                    tmp_i++;
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Caution", "Too big numbers in condition", "OK");
                return;
            }
            


            //checking on is correct numbers filled correct
            if(!isCorrect(numbers_b, numbers_p))
            {
                return;
            }

            //asking user is condition correct
            string condition = "";
            for (int i = 1; i < numbers_b.Length; i++)
            {
                condition += "X ≡ " + numbers_b[i] + " mod " + numbers_p[i] + "\n";
            }
            bool answer = await App.Current.MainPage.DisplayAlert("Is expression correct?", condition, "It's ok", "Cancel");
            if(!answer)
            {
                return;
            }

            //if not enough points - exit
            if (PointsViewModel.getInstance.Points < 15)
            {
                await App.Current.MainPage.DisplayAlert("Caution", "Not enough points!", "OK");
                return;
            }

            NsdWithMModel resultExpressionModel = ChineseCalculator.Count_Nsd_with_M(numbers_b, numbers_p);
            ExpressionModel toDbModel = ConverterToExpressionModel.Convert(resultExpressionModel);

            if(resultExpressionModel.status)
            {
                // expression decided correct
                PointsViewModel.getInstance.Points -= 15;
            }
            else
            {
                //sorry something wrong
                await App.Current.MainPage.DisplayAlert("Something wrong", "We couldn't decide expression correct, so we did'n minus points, write to developer", "OK");
            }

            //saving to db
            await App.Database.SaveItem(toDbModel);

            await App.Current.MainPage.DisplayAlert("Notification", "Expression added to History", "OK");

            //clearing fields
            ClearEntries();            

        }

        private void ClearEntries()
        {
            //DataList = 
        }

        private bool isValid()
        {
            foreach(NumbersModel nM in dataList)
            {
                if (String.IsNullOrEmpty(nM.number_a) || String.IsNullOrEmpty(nM.number_b))
                {
                    App.Current.MainPage.DisplayAlert("Caution", "You did'n fill all cells", "OK");
                    return false;
                }
            }

            foreach(NumbersModel nM in dataList)
            {
                if (nM.number_a == "0" || nM.number_b == "0")
                {
                    App.Current.MainPage.DisplayAlert("Caution", "One of numbers is equal zero", "OK");
                    return false;
                }
            }           

            return true;
        }

        private bool isCorrect(int[] numbers_b, int[] numbers_p)
        {
            //checking on b >= p
            for(int i = 1; i < numbers_b.Length; i++)
            {
                if(numbers_b[i] >= numbers_p[i])
                {
                    App.Current.MainPage.DisplayAlert("Caution", "b >= p in" + i + " row - is not allowed", "ОK");
                    return false;
                }
            }


            //checking on 'both primes'
            for (int i = 1; i < numbers_b.Length; i++)
            {
                for (int y = i + 1; y < numbers_b.Length; y++)
                {
                    OnlyNsdModel model = ChineseCalculator.Count_Nsd_p_q(numbers_p[i], numbers_p[y]);
                    if (model.nsd > 1)
                    {
                        App.Current.MainPage.DisplayAlert("Caution", "Numbers 'p' in " + i + " and " + y + " rows are 'Both primes'", "ОK");
                        return false;
                    }
                }

            }

            return true;
        }

        public List<NumbersModel> DataList
        {
            get
            {
                return dataList;
            }
            set
            {
                if(dataList != value)
                {                    
                    dataList = value;
                    OnPropertyChanged("DataList");
                }
            }
        }

        public string number_a
        {
            get
            {
                return "";
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
