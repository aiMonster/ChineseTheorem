using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseTheoremMobile
{
    public class PointsController : INotifyPropertyChanged
    {
        //singelton
        private static PointsController Current;
        
        private PointsController()
        { }

        public static PointsController getInstance()
        {
            if(Current == null)
            {
                Current = new PointsController();
            }
            return Current;
        }

        private int points = 5;

        public int IntPoints
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
                OnPropertyChanged("Points");
            }
        }

        public string Points
        {
            get
            {
                return  "Left " + points + " points";                                
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
