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

namespace VehicleApp.Droid
{
    public class ReadAllData
    {
        public static void Read(MainActivity thisActivity)
        {
            ReadWrite.ReadData();
        }
    }
}