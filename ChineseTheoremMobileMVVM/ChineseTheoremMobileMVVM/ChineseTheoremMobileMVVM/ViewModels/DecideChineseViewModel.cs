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
        static int amount = 3; //means that we will have 3 rows/elements
        List<NumbersModel> dataList { get; set; }
        public ICommand DecideCommand { get; protected set; }
        public ICommand FillByRandomCommand { get; protected set; }
        public ICommand AddRowCommand { get; protected set; }
        public ICommand RemoveRowCommand { get; protected set; }

        public DecideChineseViewModel()
        {
            this.DecideCommand = new Command(Decide);
            this.FillByRandomCommand = new Command(FillByRandom);
            this.AddRowCommand = new Command(AddRow);
            this.RemoveRowCommand = new Command(RemoveRow);
            //adding rows to our list
            dataList = new List<NumbersModel>();
            for (int i = 1; i <= amount; i++)
            {
                dataList.Add(new NumbersModel());
            }
        }

        private void AddRow()
        {
            if(amount <= 5)
            {
                amount++;
                DataList.Add(new NumbersModel());
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Oops!", "Can't add anymore rows", "OK");
            }
            
        }

        private void RemoveRow()
        {
            if(amount >= 3)
            {
                //amount--;
                //DataList.Remove()
                App.Current.MainPage.DisplayAlert("Yes", "We removed", "OK");

            }
            else
            {
                App.Current.MainPage.DisplayAlert("Oops!", "Can't delete anymore rows", "OK");
            }
        }

        private void FillByRandom()
        {
            //we need it to avoid app stopping while ofen pressing this button
            //foreach (NumbersModel nM in dataList)
            //{
            //    if (!String.IsNullOrEmpty(nM.number_a) || !String.IsNullOrEmpty(nM.number_b))
            //    {
            //        App.Current.MainPage.DisplayAlert("Oops!", "Before filling by random clear all cells!", "OK");
            //        return;
            //    }
            //}


            //our numbers
            int[] numbers_p = new int[dataList.Count + 1];
            int[] numbers_b = new int[dataList.Count + 1];
            List<NumbersModel> listOfNumbers = new List<NumbersModel>(dataList);

            //filling only b numbers   
            int rowCounterB = 0;
            foreach (var row in listOfNumbers)
            {
                Random rnd = new Random();
                while (true)
                {
                Start:
                    int tmpRand = rnd.Next(1, 50);
                    for (int i = rowCounterB; i > 0; i--)
                    {
                        if (numbers_b[i - 1] == tmpRand)
                        {
                            goto Start;
                        }
                    }
                    row.number_a = Convert.ToString(tmpRand);
                    numbers_b[rowCounterB] = tmpRand;
                    break;
                }
                rowCounterB++;

            }

            //filling our p numbers      
            int rowCounter = 0;
            foreach (var row in listOfNumbers)
            {
                Random rnd = new Random();
                while (true)
                {
                Start:
                    int tmpRand = rnd.Next(51, 100);

                    if (tmpRand <= Convert.ToInt32(row.number_a))
                    {
                        goto Start;
                    }
                    else if (tmpRand % Convert.ToInt32(row.number_a) == 0)
                    {
                        goto Start;
                    }

                    for (int i = rowCounter; i > 0; i--)
                    {
                        OnlyNsdModel tmpMod = ChineseCalculator.Count_Nsd_p_q(numbers_p[i], tmpRand);
                        if (tmpMod.nsd > 1)
                        {
                            goto Start;
                        }
                    }
                    row.number_b = Convert.ToString(tmpRand);
                    numbers_p[rowCounter + 1] = tmpRand;
                    break;
                }
                rowCounter++;
            }

            DataList = listOfNumbers;
        }

        private async void Decide()
        {
            if (!isValid())
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
                await App.Current.MainPage.DisplayAlert("Oops!", "Too big numbers in condition!", "OK");
                return;
            }



            //checking on is correct numbers filled by all rules
            if (!isCorrect(numbers_b, numbers_p))
            {
                return;
            }

            //asking user is condition correct
            string condition = "";
            for (int i = 1; i < numbers_b.Length; i++)
            {
                condition += "X ≡ " + numbers_b[i] + " mod " + numbers_p[i] + "\n";
            }
            bool answer = await App.Current.MainPage.DisplayAlert("Is expression correct?", condition + "\nPrice: 15 points", "It's ok", "Cancel");
            if (!answer)
            {
                return;
            }

            //if not enough points - exit
            if (PointsViewModel.getInstance.Points < 15)
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Not enough points!", "OK");
                return;
            }

            NsdWithMModel resultExpressionModel = new NsdWithMModel();
            try
            {
                resultExpressionModel = ChineseCalculator.Count_Nsd_with_M(numbers_b, numbers_p);
            }
            catch (System.OverflowException)
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "You choosed to big numbers, and we can't count it correct!", "OK");
                return;
            }

            ExpressionModel toDbModel = ConverterToExpressionModel.Convert(resultExpressionModel);
            if (resultExpressionModel.status)
            {
                // expression decided correct
                PointsViewModel.getInstance.Points -= 15;
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
                PointsViewModel.getInstance.Points += 15;
            }


            await App.Current.MainPage.DisplayAlert("Excellent!", "Expression added to History", "OK");
            //clearing fields
            ClearEntries();

        }

        private void ClearEntries()
        {
            List<NumbersModel> tmpL = new List<NumbersModel>(dataList);
            foreach (var m in tmpL)
            {
                m.number_a = "";
                m.number_b = "";
            }
            DataList = tmpL;
        }

        private bool isValid()
        {
            foreach (NumbersModel nM in dataList)
            {
                if (String.IsNullOrEmpty(nM.number_a) || String.IsNullOrEmpty(nM.number_b))
                {
                    App.Current.MainPage.DisplayAlert("Oops!", "You didn't fill all cells!", "OK");
                    return false;
                }
            }

            foreach (NumbersModel nM in dataList)
            {
                if (nM.number_a == "0" || nM.number_b == "0")
                {
                    App.Current.MainPage.DisplayAlert("Oops!", "One of numbers is equal zero!", "OK");
                    return false;
                }
            }

            foreach (NumbersModel nM in dataList)
            {
                if (nM.number_a.Contains(".") || nM.number_b.Contains("."))
                {
                    App.Current.MainPage.DisplayAlert("Oops!", "Remove all dots!", "OK");
                    return false;
                }
            }

            return true;
        }

        private bool isCorrect(int[] numbers_b, int[] numbers_p)
        {
            //checking on b >= p
            for (int i = 1; i < numbers_b.Length; i++)
            {
                if (numbers_b[i] >= numbers_p[i])
                {
                    App.Current.MainPage.DisplayAlert("Oops!", "b >= p in" + i + " row - it is not allowed!", "ОK");
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
                        App.Current.MainPage.DisplayAlert("Oops!", "Numbers 'p' in " + i + " and " + y + " rows are 'Both primes'!", "ОK");
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
                if (dataList != value)
                {
                    dataList = value;
                    OnPropertyChanged("DataList");
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
