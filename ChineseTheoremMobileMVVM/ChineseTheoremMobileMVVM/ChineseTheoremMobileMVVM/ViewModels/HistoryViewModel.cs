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
    class HistoryViewModel : INotifyPropertyChanged
    {              
        List<ExpressionModel> exModelList { get; set; }
        private bool isEmpty { get; set; }
   
        public HistoryViewModel()
        {
            
            exModelList = new List<ExpressionModel>();             
        }        

        public bool IsEmpty
        {
            get { return isEmpty; }
            set
            {
                if(isEmpty != value)
                {
                    isEmpty = value;
                    OnPropertyChanged("IsEmpty");
                }
            }
        }

        public List<ExpressionModel> ExModelList
        {
            get
            {
                return exModelList;
            }
            set
            {
                if(exModelList != value)
                {
                   
                    exModelList = value;
                    OnPropertyChanged("ExModelList");
                  
                    
                }
            }
        }            
          

        public async void OnAppearing(object sender, EventArgs e)
        {
                        
            List<ExpressionModel> tmpList = new List<ExpressionModel>();
            //ExModelList.Clear();

            await App.Database.CreateTable();
            foreach (var a in await App.Database.GetItemsAsync())
            {
                tmpList.Insert(0, a);
            }

            if (tmpList.Count == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }
            ExModelList = tmpList;   
                 
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
               PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
