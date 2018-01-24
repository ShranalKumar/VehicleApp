using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Java.Lang;
using Android.Content;
using Android.Content.PM;

namespace VehicleApp.Droid
{
    [Activity(Label = "VehicleApp", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private ImageButton _addIcon;
        private ListView _carList;
        public static VehicleRowCustomAdapter _vehicleListAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            _addIcon = FindViewById<ImageButton>(Resource.Id.AddIcon);
            _carList = FindViewById<ListView>(Resource.Id.MainListView);

            _addIcon.SetImageResource(Resource.Drawable.Other);

            _addIcon.Click += delegate { StartActivity(typeof(AddNewVehicle)); };
            ReloadData();

            _carList.ItemClick += showVehicleDetails;
            _carList.ItemLongClick += DeleteVehicleDetailsAlert;
        }

        private void showVehicleDetails(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(ShowVehicleDetails));
            intent.PutExtra("Vehicle", e.Position);
            StartActivity(intent);
        }

        public void DeleteVehicleDetailsAlert(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            VehicleList toRemove = AppData.vehicles[e.Position];

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Confirm Delete");
            alert.SetMessage("Are you sure you want to delete this vehicle?");
            alert.SetPositiveButton("Delete", (senderAlert, eAlert) => DeleteVehicle(toRemove, e));
            alert.SetNegativeButton("Cancel", (senderAlert, eAlert) => { });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

        public void DeleteVehicle(VehicleList inpList, AdapterView.ItemLongClickEventArgs e)
        {
            e.View.Animate().SetDuration(750).Alpha(0).WithEndAction(new Runnable(() => 
                {
                    AppData.vehicles.Remove(inpList);
                    ReadWrite.WriteData();
                    _vehicleListAdapter.NotifyDataSetChanged();
                    e.View.Alpha = 1;
                }));
        }

        public void ReloadData()
        {
            ReadAllData.Read(this);
            _vehicleListAdapter = new VehicleRowCustomAdapter(this, AppData.vehicles);
            _carList.Adapter = _vehicleListAdapter;
        }
    }    
}

