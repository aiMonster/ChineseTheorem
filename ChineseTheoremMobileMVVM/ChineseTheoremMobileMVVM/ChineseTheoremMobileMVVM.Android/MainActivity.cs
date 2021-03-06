﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using Xamarin.Forms;
using static Android.Media.Audiofx.BassBoost;

namespace ChineseTheoremMobileMVVM.Droid
{
    [Activity(Label = "Chinese Theorem", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //trying to get phone imei
            try
            {
                Android.Telephony.TelephonyManager mTelephonyMgr;
                mTelephonyMgr = (Android.Telephony.TelephonyManager)GetSystemService(TelephonyService);                
                ImeiGetter_Android.imei = mTelephonyMgr.DeviceId;
            }
            catch
            {
                ImeiGetter_Android.imei = "phone_imei";
            }
            

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

