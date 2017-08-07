using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.Net;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;


using ChineseTheoremMobileMVVM.Models;
using ChineseTheoremMobileMVVM.Calculator;
using ChineseTheoremMobileMVVM.Converter;
using System.Windows.Input;
using Xamarin.Forms;
using ChineseTheoremMobileMVVM.Services;
using ChineseTheoremMobileMVVM.Internet;

using System.Net.NetworkInformation;


namespace ChineseTheoremMobileMVVM.ViewModels
{
    public class PromoCodeViewModel : INotifyPropertyChanged
    {
        public ICommand ActivateCommand { get; set; }
        public ICommand TransferCommand { get; set; }
        private string promoCode { get; set; }
        private string attempts { get; set; }
        private string gotCode { get; set; }

        //bool initialized = false;
        //PromoCodeService promoCodeService = new PromoCodeService();
        

        public PromoCodeViewModel()
        {
            this.ActivateCommand = new Command(Activate);
            this.TransferCommand = new Command(Transfer);
        }

        public string GotCode
        {
            get { return gotCode; }
            set
            {
                if(gotCode != value)
                {
                    gotCode = value;
                    OnPropertyChanged("GotCode");
                }
            }
          
        }

        public string PromoCode
        {
            get { return promoCode; }
            set
            {
                if(promoCode != value)
                {
                    promoCode = value;
                    OnPropertyChanged("PromoCode");
                }
            }
        }

        public string Attempts
        {
            get { return attempts; }
            set
            {
                if(attempts != value)
                {                    
                    attempts = value;                                       
                    OnPropertyChanged("Attempts");
                }
            }
        }


        private void Activate()
        {
            bool isConnected = CrossConnectivity.Current.IsConnected;
            //App.Current.MainPage.DisplayAlert("activate", isConnected.ToString(), "ok");
            //if (initialized == true) return;

            //IEnumerable<PromoCodeModel> codes = await promoCodeService.Get();




        }

        private async void Transfer()
        {
            if(String.IsNullOrEmpty(attempts))
            {
                await App.Current.MainPage.DisplayAlert("Caution", "Enter number!", "OK");
                return;
            }
            else if(Convert.ToInt32(attempts) <= 0)
            {
                await App.Current.MainPage.DisplayAlert("Caution", "Enter number higher than 0!", "OK");
                return;
            }
            else if(Convert.ToInt32(attempts) > PointsViewModel.getInstance.Points)
            {
                await App.Current.MainPage.DisplayAlert("Caution", "You don't have so much points, you have only " + PointsViewModel.getInstance.Points + " points!", "OK");
                return;
            }

            bool answer = await App.Current.MainPage.DisplayAlert("Notification", "Do you really want to transfer " + attempts + " points?", "YES", "NO");
            if(!answer)
            {
                return;
            }

            //checking on internet connection


            //if Ok
            //getting code

            //await App.Current.MainPage.DisplayAlert("Caution", "Wait on code!", "OK");
            PointsViewModel.getInstance.Points -= Convert.ToInt32(attempts);
            Attempts = "";
            GotCode = "1kd8df9s";
        }

        public void onAppearing()
        {           
            Attempts = Convert.ToString(PointsViewModel.getInstance.Points);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
