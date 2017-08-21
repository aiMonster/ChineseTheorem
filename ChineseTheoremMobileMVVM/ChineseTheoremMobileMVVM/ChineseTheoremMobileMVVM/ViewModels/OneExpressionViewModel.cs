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
    class OneExpressionViewModel : INotifyPropertyChanged
    {
        public ICommand DeleteCommand { protected set; get; }
        public ICommand CloseCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        private ExpressionModel exModel;
        public OneExpressionViewModel(ExpressionModel model)
        {
            exModel = model;
            this.DeleteCommand = new Command(Delete);
            this.CloseCommand = new Command(Close);
        }

        private void Close()
        {
            Navigation.PopModalAsync();
        }

        private async void Delete()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Notification", "Do you really want to remove this entry?", "Yes", "No");
            if (!answer)
            {
                return;
            }
            await App.Database.DeleteItemAsync(exModel);
            Close();

        }

        public string Name
        {
            get { return exModel.name; }
            set
            {
                if (exModel.name != value)
                {
                    exModel.name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Condition
        {
            get { return exModel.condition; }
            set
            {
                if (exModel.condition != value)
                {
                    exModel.condition = value;
                    OnPropertyChanged("Condition");
                }
            }
        }

        public string Solution
        {
            get { return exModel.solution; }
            set
            {
                if (exModel.solution != value)
                {
                    exModel.solution = value;
                    OnPropertyChanged("Solution");
                }
            }
        }

        public string SolutionP2
        {
            get { return exModel.solutionP2; }
            set
            {
                if (exModel.solutionP2 != value)
                {
                    exModel.solutionP2 = value;
                    OnPropertyChanged("SolutionP2");
                }
            }
        }

        public string Status
        {
            get { return exModel.status; }
            set
            {
                if (exModel.status != value)
                {
                    exModel.status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public string Date
        {
            get { return Convert.ToString(exModel.date); }
            set
            {
                if (Convert.ToString(exModel.date) != value)
                {
                    exModel.date = Convert.ToDateTime(value);
                    OnPropertyChanged("Date");
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
