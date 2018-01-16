using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleApp
{
    public class AppData
    {
        public static List<VehicleList> vehicles;
        private static AppData instance;

        public static AppData GetInstance()
        {
            if (instance == null)
            {
                instance = new AppData();
            }
            return instance;
        }
    }
}
