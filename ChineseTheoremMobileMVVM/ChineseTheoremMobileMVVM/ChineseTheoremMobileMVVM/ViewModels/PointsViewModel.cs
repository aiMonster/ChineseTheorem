﻿using Plugin.Settings;
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

        private PointsViewModel()
        {
            //loading points from settings, if empty setting zero
            Points = CrossSettings.Current.GetValueOrDefault("PointsAmount", 0);
        }

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

                //adding or updating our points in settings
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
