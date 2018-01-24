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

namespace VehicleApp.Droid
{
    [Activity(Label = "AddNewVehicle", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AddNewVehicle : Activity
    {
        private ImageButton _addVehicleSaveButton;
        private ImageButton _addVehicleBackButton;
        private TextView _addVehicleHeader;
        private EditText _vehicleMakeText;
        private EditText _vehicleModelText;
        private EditText _vehicleYearText;
        private EditText _vehicleRegoText;
        private EditText _vehicleWOFText;
        private EditText _vehicleRegoDueText;
        private EditText _vehicleNextServiceText;
        private EditText _vehicleOdometerText;
        private EditText _vehicleVINPlateText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddNewVehicleLayout);
            Window.SetSoftInputMode(Android.Views.SoftInput.AdjustPan);

            _addVehicleSaveButton = FindViewById<ImageButton>(Resource.Id.AddVehicleSaveButton);            
            _addVehicleBackButton = FindViewById<ImageButton>(Resource.Id.AddVehicleBackButton);
            _addVehicleHeader = FindViewById<TextView>(Resource.Id.AddVehicleHeader);
            _vehicleMakeText = FindViewById<EditText>(Resource.Id.vehicleMakeText);
            _vehicleModelText = FindViewById<EditText>(Resource.Id.vehicleModelText);
            _vehicleYearText = FindViewById<EditText>(Resource.Id.vehicleYearText);
            _vehicleRegoText = FindViewById<EditText>(Resource.Id.vehicleRegoText);
            _vehicleWOFText = FindViewById<EditText>(Resource.Id.vehicleWofText);
            _vehicleRegoDueText = FindViewById<EditText>(Resource.Id.vehicleRegoDueText);
            _vehicleNextServiceText = FindViewById<EditText>(Resource.Id.vehicleNextServiceText);
            _vehicleOdometerText = FindViewById<EditText>(Resource.Id.vehicleOdometerText);
            _vehicleVINPlateText = FindViewById<EditText>(Resource.Id.vehicleVINPlateText);

            _addVehicleSaveButton.SetImageResource(Resource.Drawable.DoneIcon);
            _addVehicleBackButton.SetImageResource(Resource.Drawable.SelectedAccountTick);
            _addVehicleHeader.Text = "Add New Vehicle";

            _addVehicleSaveButton.Click += CreateVehicleInstance;
            _addVehicleBackButton.Click += delegate { StartActivity(typeof(MainActivity)); };
        }

        private void CreateVehicleInstance(object sender, EventArgs e)
        {
            VehicleList newVehicleToAdd = new VehicleList();
            newVehicleToAdd.VehicleName = _vehicleMakeText.Text + " " + _vehicleModelText.Text;
            newVehicleToAdd.Details = new VehicleDetails()
            {
                Make = _vehicleMakeText.Text,
                Model = _vehicleModelText.Text,
                Year = _vehicleYearText.Text,
                Rego = _vehicleRegoText.Text,
                WofDueOn = Convert.ToDateTime(_vehicleWOFText.Text),
                RegoDueOn = Convert.ToDateTime(_vehicleRegoDueText.Text),
                NextService = Convert.ToDateTime(_vehicleNextServiceText.Text),
                OdometerReading = Convert.ToInt32(_vehicleOdometerText.Text),
                VinPlateNumber = _vehicleVINPlateText.Text
            };
            AppData.vehicles.Add(newVehicleToAdd);
            ReadWrite.WriteData();
            ShowAddedDialog();
            MainActivity._vehicleListAdapter.NotifyDataSetChanged();
        }

        public void ShowAddedDialog()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Vehicle Added!");
            alert.SetMessage("Your vehice has been added to the database.");
            alert.SetNeutralButton("Okay", (senderAlert, eAlert) => { Finish(); });
            Dialog dialog = alert.Create();
            dialog.Show();
        }
    }
}