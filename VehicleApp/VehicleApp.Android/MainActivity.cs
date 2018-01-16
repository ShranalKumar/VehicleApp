using Android.App;
using Android.Widget;
using Android.OS;

namespace VehicleApp.Droid
{
    [Activity(Label = "VehicleApp", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private ImageButton _addIcon;
        private ListView _carList;
        public VehicleRowCustomAdapter _vehicleListAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);
            _addIcon = FindViewById<ImageButton>(Resource.Id.AddIcon);
            _addIcon.SetImageResource(Resource.Drawable.Other);
            _addIcon.Click += delegate { StartActivity(typeof(AddNewVehicle)); };
            _carList = FindViewById<ListView>(Resource.Id.MainListView);
            //_carList.ItemClick += showVehicleDetails;
        }

        public void ReloadData()
        {
            ReadAllData.Read(this);
            _vehicleListAdapter = new VehicleRowCustomAdapter(this, AppData.vehicles);
            _carList.Adapter = _vehicleListAdapter;
        }
    }    
}

