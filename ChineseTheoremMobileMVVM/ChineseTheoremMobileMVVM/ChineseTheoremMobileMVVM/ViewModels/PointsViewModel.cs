using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobileMVVM.ViewModels
{
    class PointsViewModel : INotifyPropertyChanged
    {
        //singelton viewModel to be accessble from all pages
        private static PointsViewModel Current;

        private PointsViewModel() { Points = CrossSettings.Current.GetValueOrDefault("PointsAmount", 0); } //here we will load amount of our points

        public static PointsViewModel getInstance
        {
            get
            {
                if (Current == null)
                {
                    Current = new PointsViewModel();
                }
                return Current;
            }

        }

        private int points { get; set; }

        public int Points
        {
            get { return points; }
            set
            {
                points = (value >= 0) ? value : 0;

                //here we will rewrite points in file
                CrossSettings.Current.AddOrUpdateValue("PointsAmount", points);
                OnPropertyChanged("Points");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
