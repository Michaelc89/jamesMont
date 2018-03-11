using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace jamesMont.Droid
{
    [Activity(Label = "Cut & Die", Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            System.Threading.Thread.Sleep(700);
            StartActivity(typeof(MainActivity));
            Finish();
            OverridePendingTransition(0,0);

        }
    }
}