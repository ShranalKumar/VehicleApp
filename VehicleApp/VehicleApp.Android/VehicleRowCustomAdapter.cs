using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace VehicleApp.Droid
{
    public class VehicleRowCustomAdapter : BaseAdapter<VehicleList>
    {
        readonly List<VehicleList> currentVehicles;
        readonly Activity myContext;

        public VehicleRowCustomAdapter(MainActivity mainActivity, List<VehicleList> vehicles) : base()
        {
            this.myContext = mainActivity;
            this.currentVehicles = vehicles;
        }

        public override VehicleList this[int position]
        {
            get
            {
                return currentVehicles[position];
            }
        }

        public override int Count
        {
            get { return currentVehicles.Count(); }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
            {
                view = myContext.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }

            TextView MainTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            MainTextView.Text = currentVehicles[position].VehicleName;
            MainTextView.TextSize = 30;
            MainTextView.SetTypeface(null, TypefaceStyle.Bold);

            return view;
        }
    }
}