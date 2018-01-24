using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleApp
{
    public class VehicleDetails
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Rego { get; set; }
        public DateTime WofDueOn { get; set; }
        public DateTime RegoDueOn { get; set; }
        public DateTime NextService { get; set; }
        public int OdometerReading { get; set; }
        public string VinPlateNumber { get; set; }
    }
}
