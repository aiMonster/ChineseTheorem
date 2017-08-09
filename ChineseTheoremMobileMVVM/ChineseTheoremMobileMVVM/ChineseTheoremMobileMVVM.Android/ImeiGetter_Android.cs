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

namespace ChineseTheoremMobileMVVM.Droid
{
    public class ImeiGetter_Android :IImeiGetter
    {
        public ImeiGetter_Android() { }
        public string GetImei()
        {
            Android.Telephony.TelephonyManager mTelephonyMgr;
            //mTelephonyMgr = (Android.Telephony.TelephonyManager)GetSystemService(TelephonyService);
            mTelephonyMgr =  (TelephonyManager)Forms.Context.GetSystemService(Android.Content.Context.TelephonyService);
            string tmp = mTelephonyMgr.DeviceId;
            return tmp;
        }
    }
}