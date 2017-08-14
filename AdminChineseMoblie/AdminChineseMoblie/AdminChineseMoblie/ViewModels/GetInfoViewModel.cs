using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AdminChineseMoblie.Models;
using AdminChineseMoblie.Views;
using AdminChineseMoblie.Services;
using Plugin.Connectivity;

namespace AdminChineseMoblie.ViewModels
{
    public class GetInfoViewModel : INotifyPropertyChanged
    {
        public ICommand GetInfoCommand { get; set; }
        public INavigation Navigation { get; set; }
        private string promoCode { get; set; }
        private bool isBusy { get; set; }
        PromoCodeService promoCodeService = new PromoCodeService();

        public GetInfoViewModel()
        {
            this.GetInfoCommand = new Command(GetInfo);
            IsBusy = false;
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy != value)
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

        private async void GetInfo()
        {
            if (String.IsNullOrEmpty(promoCode))
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Enter promo code!", "OK");
                return;
            }
            else if (promoCode.Length != 8)
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Promo code has to have length of 8 characters!", "OK");
                return;
            }

            //sample of able characters
            char[] stuff = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
            'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm'};
            bool contains = false;
            //checking is promoCode correct
            foreach (char tmp in promoCode)
            {
                bool tmpB = false;
                foreach (char ch in stuff)
                {
                    if (tmp == ch)
                    {
                        tmpB = true;
                        break;
                    }
                }
                if (!tmpB)
                {
                    contains = true;
                }

            }
            if (contains)
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Promo code contains not allowed symbols!", "OK");
                return;
            }



            bool answer = await App.Current.MainPage.DisplayAlert("Is promo code correct?", "\n  '" + promoCode + "'\n", "YES", "NO");
            if (!answer)
            {
                return;
            }

            //checking on internet connection
            bool isConnected = CrossConnectivity.Current.IsConnected;
            if (!isConnected)
            {
                await App.Current.MainPage.DisplayAlert("Oops", "Sorry, no internet connection!", "OK");
                return;
            }

            IsBusy = true;
            PromoCodeModel result = new PromoCodeModel();
            try
            {
                result = await promoCodeService.GetInfoAboutCode(promoCode);
            }
            catch
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Oops!", "Something wrong while connecting the server!", "OK");
                return;
            }

            if(result.AmountAttempts <= 0)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Oops!", "Such promo code doesn't exist", "OK");
                return;
            }

            IsBusy = false;
            PromoCode = "";
            await Navigation.PushModalAsync(new PromoInfo(result));
           
        }

        public string PromoCode
        {
            get { return promoCode; }
            set
            {
                if (promoCode != value)
                {
                    promoCode = value;
                    OnPropertyChanged("PromoCode");
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
