using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminChineseMoblie.Models;

namespace AdminChineseMoblie.ViewModels
{
    public class PromoInfoViewModel : INotifyPropertyChanged
    {
        private PromoCodeModel promoModel;
        public PromoInfoViewModel(PromoCodeModel model)
        {
            promoModel = model;
        }

        public int Id
        {
            get { return promoModel.Id; }
            set
            {
                if (promoModel.Id != value)
                {
                    promoModel.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Date
        {
            get { return promoModel.Date; }
            set
            {
                if (promoModel.Date != value)
                {
                    promoModel.Date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public string Imei
        {
            get { return promoModel.IMEI; }
            set
            {
                if (promoModel.IMEI != value)
                {
                    promoModel.IMEI = value;
                    OnPropertyChanged("Imei");
                }
            }
        }

        public string PromoCode
        {
            get { return promoModel.PromoCode; }
            set
            {
                if (promoModel.PromoCode != value)
                {
                    promoModel.PromoCode = value;
                    OnPropertyChanged("PromoCode");
                }
            }
        }

        public int AmountAttempts
        {
            get { return promoModel.AmountAttempts; }
            set
            {
                if (promoModel.AmountAttempts != value)
                {
                    promoModel.AmountAttempts = value;
                    OnPropertyChanged("AmountAttempts");
                }
            }
        }

        public string IsUsed
        {
            get { return Convert.ToString(promoModel.isUsed); }
            set
            {
                if (Convert.ToString(promoModel.isUsed) != value)
                {
                    promoModel.isUsed = Convert.ToBoolean(value);
                    OnPropertyChanged("IsUsed");
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
