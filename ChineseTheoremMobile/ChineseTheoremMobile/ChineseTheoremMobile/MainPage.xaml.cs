using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChineseTheoremMobile
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            PointsController p = PointsController.getInstance();            
            BindingContext = p;


            //here we are setting and checking our amount or points
            p.IntPoints = 25;
            
        }

        
    }
}
