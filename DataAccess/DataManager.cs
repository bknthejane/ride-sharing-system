using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RideSharingSystem.Models;

namespace RideSharingSystem.DataAccess
{
    internal class DataManager
    {
        // File path for the JSON
        private const string DataFile = "Data.json";

        // Save system data to the DataFile
        public static void SaveData(
            List<Passenger> passengers,
            List<Driver> drivers,
            List<Ride> pendingRides,
            List<Ride> completedRides)
        {
            // Put data into a single object for serialization
            var data = new SaveData
            {
                Passengers = passengers,
                Drivers = drivers,
                PendingRides = pendingRides,
                CompletedRides = completedRides
            };

            // Serialize the data object into JSON format with indentation for readability
            File.WriteAllText(DataFile, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true}));
        }

        public static SaveData LoadData()
        {
            // If the DataFile doesn't exist, return a new SaveData object with empty lists
            if (!File.Exists(DataFile))
                return new SaveData
                {
                    Passengers = new List<Passenger>(),
                    Drivers = new List<Driver>(),
                    PendingRides = new List<Ride>(),
                    CompletedRides = new List<Ride>()
                };

            // Read JSON data
            var json = File.ReadAllText(DataFile);

            // Deserialize the JSON into a SaveData object
            var data = JsonSerializer.Deserialize<SaveData>(json);

            // Return the deserialized object or a fallback empty SaveData object if null
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
