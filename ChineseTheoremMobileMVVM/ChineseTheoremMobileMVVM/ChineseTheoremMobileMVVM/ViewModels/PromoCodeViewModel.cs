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
        private bool isBusy { get; set; }

        //bool initialized = false;
        PromoCodeService promoCodeService = new PromoCodeService();
        

        public PromoCodeViewModel()
        {
            this.ActivateCommand = new Command(Activate);
            this.TransferCommand = new Command(Transfer);
            IsBusy = false;
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if(isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                    OnPropertyChanged("IsBusyInverted");
                }
            }

        }

        public bool IsBusyInverted
        {
            get { return !isBusy; }
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


        private async void Activate()
        {
            if(String.IsNullOrEmpty(promoCode))
            {
                await App.Current.MainPage.DisplayAlert("Caution", "Enter promo code!", "OK");
                return;
            }
            else if(promoCode.Length != 8)
            {
                await App.Current.MainPage.DisplayAlert("Caution", "Promo code has to have length of 8 characters", "OK");
                return;
            }

            char[] stuff = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm'};
            bool contains = false;
            foreach(char tmp in promoCode)
            {
                bool tmpB = false;
                foreach(char ch in stuff)
                {
                    if(tmp == ch)
                    {
                        tmpB = true;
                        break;
                    }
                }

                if(!tmpB)
                {
                    contains = true;
                }
                    
            }
            if(contains)
            {
                await App.Current.MainPage.DisplayAlert("Caution", "Invalid string!", "OK");
                return;
            }



            bool answer = await App.Current.MainPage.DisplayAlert("Notification", "Is promo code correct - " + promoCode + " ?", "YES", "NO");
            if (!answer)
            {
                return;
            }

            bool isConnected = CrossConnectivity.Current.IsConnected;
            if (!isConnected)
            {
                await App.Current.MainPage.DisplayAlert("Caution", "Sorry, no internet connection!", "OK");
                return;
            }

            IsBusy = true;
            int result = 0;
            try
            {
                result = await promoCodeService.TransferCode(promoCode);
            }
            catch
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Problem", "Something went wrong while connecting the server", "OK");                
                return;
            }

            if(result <= 0)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Problem", "Promo code is Incorrect or already used, try again", "OK");                
                return;
            }
            PointsViewModel.getInstance.Points += result;
            PromoCode = "";            
            IsBusy = false;
            Attempts = Convert.ToString(PointsViewModel.getInstance.Points);
            await App.Current.MainPage.DisplayAlert("Activated", "You have added " + result + " attempts", "OK");                  
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
            bool isConnected = CrossConnectivity.Current.IsConnected;
            if(!isConnected)
            {
                await App.Current.MainPage.DisplayAlert("Caution", "Sorry, no internet connection!", "OK");
                return;
            }

            IsBusy = true;
            string result = "";
            try
            {
                result = await promoCodeService.GetCode(Convert.ToInt32(attempts));
            }
            catch
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Problem", "Something went wrong while connecting the server", "OK");                
                return;
            }
                     
            PointsViewModel.getInstance.Points -= Convert.ToInt32(attempts);
            Attempts = Convert.ToString(PointsViewModel.getInstance.Points);
            GotCode = result;
            IsBusy = false;
            await App.Current.MainPage.DisplayAlert("Your code:", result, "OK");
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
