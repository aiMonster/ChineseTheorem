using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ChineseTheoremMobileMVVM.Droid;
using ChineseTheoremMobileMVVM.Services;
using Android.Telephony;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ImeiGetter_Android))]
namespace ChineseTheoremMobileMVVM.Droid
{
    public class ImeiGetter_Android :IImeiGetter
    {
        //we will add imei in MainActivity
        public ImeiGetter_Android() { }
        public static string imei { get; set; }
        public string GetImei()
        {           
            return imei;
        }
    }
}