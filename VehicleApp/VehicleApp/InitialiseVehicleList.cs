using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleApp
{
    public class InitialiseVehicleList
    {
        public static void Initialise()
        {

            AppData.vehicles = new List<VehicleList>();


            AppData.vehicles[0].Details = new VehicleDetails()
            {
                Make = "Nissan",
                Model = "Primera",
                Year = "1996",
                Rego = "BBH900",
                WofDueOn = new DateTime(2018, 07, 13),
                RegoDueOn = new DateTime(2018, 10, 24),
                NextService = new DateTime(2018, 01, 05),
                OdometerReading = 297779,
                VinPlateNumber = "abcd"
            };

            AppData.vehicles[0].VehicleName = AppData.vehicles[0].Details.Make + " " + AppData.vehicles[0].Details.Model;
        }
    }
}
