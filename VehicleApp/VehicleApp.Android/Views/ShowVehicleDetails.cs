using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content.PM;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace VehicleApp.Droid
{
    [Activity(Label = "ShowVehicleDetails", ScreenOrientation=ScreenOrientation.Portrait)]
    public class ShowVehicleDetails : Activity
    {
        private int _vehiclePosition;
        private VehicleList _vehicle;
        private ImageButton _viewVehicleDetailsBackButton;
        private TextView _viewVehicleDetailsTitle;
        private TextView _viewVehicleDetailsMakeText;
        private TextView _viewVehicleDetailsModelText;
        private TextView _viewVehicleDetailsYearText;
        private TextView _viewVehicleDetailsRegoText;
        private TextView _viewVehicleDetailsWOFText;
        private TextView _viewVehicleDetailsRegoDueText;
        private TextView _viewVehicleDetailsNextServiceText;
        private TextView _viewVehicleDetailsOdometerText;
        private TextView _viewVehicleDetailsVINPlateText;
        private List<TextView> _viewVehicleDetailsTextViews;
        private Button _viewVehicleDetailsUpdateButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewVehicleDetailsLayout);

            _vehiclePosition = Intent.Extras.GetInt("Vehicle");
            _vehicle = AppData.vehicles[_vehiclePosition];


            _viewVehicleDetailsBackButton = FindViewById<ImageButton>(Resource.Id.ViewVehicleDetailsBackButton);
            _viewVehicleDetailsTitle = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsTitle);
            _viewVehicleDetailsMakeText = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsMakeText);
            _viewVehicleDetailsModelText = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsModelText);
            _viewVehicleDetailsYearText = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsYearText);
            _viewVehicleDetailsRegoText = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsRegoText);
            _viewVehicleDetailsWOFText = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsWOFText);
            _viewVehicleDetailsRegoDueText = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsRegoDueText);
            _viewVehicleDetailsNextServiceText = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsNextServiceText);
            _viewVehicleDetailsOdometerText = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsOdometerText);
            _viewVehicleDetailsVINPlateText = FindViewById<TextView>(Resource.Id.ViewVehicleDetailsVINPlateText);
            _viewVehicleDetailsUpdateButton = FindViewById<Button>(Resource.Id.ViewVehicleDetailsUpdateButton);

            _viewVehicleDetailsBackButton.SetImageResource(Resource.Drawable.ArrowBackIcon);
            _viewVehicleDetailsTitle.Text = _vehicle.VehicleName;
            _viewVehicleDetailsMakeText.Text = _vehicle.Details.Make;
            _viewVehicleDetailsModelText.Text = _vehicle.Details.Model;
            _viewVehicleDetailsYearText.Text = _vehicle.Details.Year;
            _viewVehicleDetailsRegoText.Text = _vehicle.Details.Rego;
            _viewVehicleDetailsWOFText.Text = _vehicle.Details.WofDueOn.ToShortDateString();
            _viewVehicleDetailsRegoDueText.Text = _vehicle.Details.RegoDueOn.ToShortDateString();
            _viewVehicleDetailsNextServiceText.Text = _vehicle.Details.NextService.ToShortDateString();
            _viewVehicleDetailsOdometerText.Text = _vehicle.Details.OdometerReading.ToString();
            _viewVehicleDetailsVINPlateText.Text = _vehicle.Details.VinPlateNumber;

            _viewVehicleDetailsTextViews = new List<TextView>();
            _viewVehicleDetailsTextViews.Add(_viewVehicleDetailsMakeText);
            _viewVehicleDetailsTextViews.Add(_viewVehicleDetailsModelText);
            _viewVehicleDetailsTextViews.Add(_viewVehicleDetailsYearText);
            _viewVehicleDetailsTextViews.Add(_viewVehicleDetailsRegoText);
            _viewVehicleDetailsTextViews.Add(_viewVehicleDetailsWOFText);
            _viewVehicleDetailsTextViews.Add(_viewVehicleDetailsRegoDueText);
            _viewVehicleDetailsTextViews.Add(_viewVehicleDetailsNextServiceText);
            _viewVehicleDetailsTextViews.Add(_viewVehicleDetailsOdometerText);
            _viewVehicleDetailsTextViews.Add(_viewVehicleDetailsVINPlateText);

            _viewVehicleDetailsBackButton.Click += delegate { Finish(); };
            _viewVehicleDetailsUpdateButton.Click += UpdateVehicleDetails;



            foreach (TextView text in _viewVehicleDetailsTextViews)
            {
                text.Click += (o, s) =>
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Change Value");
                    alert.SetMessage("Please enter new value for this field.");
                    EditText input = new EditText(this)
                    {
                        TextSize = 22,
                        Gravity = GravityFlags.Center
                    };
                    input.SetSingleLine(true);
                    alert.SetView(input);
                    alert.SetPositiveButton("Save", (senderAlert, eAlert) => text.Text = input.Text);
                    Dialog dialog = alert.Create();
                    dialog.Show();
                };
            }
        }

        private void UpdateVehicleDetails(object sender, EventArgs e)
        {
            _vehicle.VehicleName = _viewVehicleDetailsMakeText.Text + " " + _viewVehicleDetailsModelText.Text;
            _vehicle.Details = null;
            _vehicle.Details = new VehicleDetails()
            {
                Make = _viewVehicleDetailsMakeText.Text,
                Model = _viewVehicleDetailsModelText.Text,
                Year = _viewVehicleDetailsYearText.Text,
                Rego = _viewVehicleDetailsRegoText.Text,
                WofDueOn = Convert.ToDateTime(_viewVehicleDetailsWOFText.Text),
                RegoDueOn = Convert.ToDateTime(_viewVehicleDetailsRegoDueText.Text),
                NextService = Convert.ToDateTime(_viewVehicleDetailsNextServiceText.Text),
                OdometerReading = Convert.ToInt32(_viewVehicleDetailsOdometerText.Text),
                VinPlateNumber = _viewVehicleDetailsVINPlateText.Text
            };
            ReadWrite.WriteData();
            ShowUpdatedDialog();
            MainActivity._vehicleListAdapter.NotifyDataSetChanged();
        }

        public void ShowUpdatedDialog()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Vehicle Updated!");
            alert.SetMessage("Your vehice has been updated.");
            alert.SetNeutralButton("Okay", (senderAlert, eAlert) => { Finish(); });
            Dialog dialog = alert.Create();
            dialog.Show();
        }
    }
}