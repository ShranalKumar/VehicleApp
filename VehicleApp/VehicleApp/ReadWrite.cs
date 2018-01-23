using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VehicleApp
{
    static class ReadWrite
    {
        static readonly string mainPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        static readonly string dataPath = Path.Combine(mainPath, "data.json");


        public static void WriteData()
        {
            //AppData.vehicles = new List<VehicleList>();

            //if (AppData.vehicles != null)
            //    foreach (VehicleList any in AppData.vehicles)
            //        AppData.vehicles.Add(any);

            string dataJson = JsonConvert.SerializeObject(AppData.vehicles, Formatting.Indented);
            File.WriteAllText(dataPath, dataJson);
        }

        public static void ReadData()
        {
            AppData.vehicles = new List<VehicleList>();

            if (File.Exists(dataPath))
            {
                using (StreamReader file = File.OpenText(dataPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    AppData.vehicles = (List<VehicleList>)serializer.Deserialize(file, typeof(List<VehicleList>));
                }
            }
        }
    }
}
