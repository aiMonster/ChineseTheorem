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


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
