using AdminChineseMoblie.Services;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AdminChineseMoblie.ViewModels
{
    public class GetPromoViewModel : INotifyPropertyChanged
    {
        public ICommand TransferCommand { get; set; }
        private string attempts { get; set; }
        private string gotCode { get; set; }
        private bool isBusy { get; set; }
        PromoCodeService promoCodeService = new PromoCodeService();

        public GetPromoViewModel()
        {
            this.TransferCommand = new Command(Transfer);
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

        public string GotCode
        {
            get { return gotCode; }
            set
            {
                if (gotCode != value)
                {
                    gotCode = value;
                    OnPropertyChanged("GotCode");
                }
            }

        }

        public string Attempts
        {
            get { return attempts; }
            set
            {
                if (attempts != value)
                {
                    attempts = value.Replace(".", "");
                    //attempts.Replace(".", "");
                    OnPropertyChanged("Attempts");
                }
            }
        }


        private async void Transfer()
        {
            int attempts_int;
            if (String.IsNullOrEmpty(attempts))
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Enter number!", "OK");
                return;
            }
            
            try
            {
                attempts_int = Convert.ToInt32(attempts);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Too big number!", "OK");
                return;
            }

            if (Convert.ToInt32(attempts_int) <= 0)
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Enter number higher than 0!", "OK");
                return;
            }
            

            bool answer = await App.Current.MainPage.DisplayAlert("Notification", "Do you really want to transfer " + attempts + " points?", "YES", "NO");
            if (!answer)
            {
                return;
            }

            //checking on internet connection
            bool isConnected = CrossConnectivity.Current.IsConnected;
            if (!isConnected)
            {
                await App.Current.MainPage.DisplayAlert("Oops!", "Sorry, no internet connection!", "OK");
                return;
            }

            IsBusy = true;
            string result = "";
            try
            {
                result = await promoCodeService.TransferCode(attempts_int);
            }
            catch
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Oops!", "Something wrong while connecting the server!", "OK");
                return;
            }

            Attempts = "";
            GotCode = result;
            IsBusy = false;
            await App.Current.MainPage.DisplayAlert("Your code:", "\n  '" + result + "'\n", "OK");
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
