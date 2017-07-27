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

        private void Decide()
        {
            if(!isValid())
            {
                return;
            }

            App.Current.MainPage.DisplayAlert("Notification", "Filled ok", "OK");
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

            foreach (NumbersModel nM in dataList)
            {
                if (nM.number_a.Contains(".") || nM.number_b.Contains("."))
                {
                    App.Current.MainPage.DisplayAlert("Caution", "Remove all dots from cells", "OK");
                    return false;
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

     
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
