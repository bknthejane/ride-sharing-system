using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RideSharingSystem.Models;
using System.IO;

namespace RideSharingSystem.DataAccess
{
    internal class DataManager
    {
        private const string DataFile = "Data.json";

        public static void SaveData(
            List<Passenger> passengers,
            List<Driver> drivers,
            List<Ride> pendingRides,
            List<Ride> completedRides)
        {
            var data = new SaveData
            {
                Passengers = passengers,
                Drivers = drivers,
                PendingRides = pendingRides,
                CompletedRides = completedRides
            };

            File.WriteAllText(DataFile, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true}));

            Console.WriteLine("Data saved.");
        }

        public static SaveData LoadData()
        {
            if (!File.Exists(DataFile))
                return new SaveData
                {
                    Passengers = new List<Passenger>(),
                    Drivers = new List<Driver>(),
                    PendingRides = new List<Ride>(),
                    CompletedRides = new List<Ride>()
                };

            var json = File.ReadAllText(DataFile);
            var data = JsonSerializer.Deserialize<SaveData>(json);

            return data ?? new SaveData
            {
                Passengers = new List<Passenger>(),
                Drivers = new List<Driver>(),
                PendingRides = new List<Ride>(),
                CompletedRides = new List<Ride>()
            };
        }
    }
}
