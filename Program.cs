using System;
using System.Collections.Generic;
using RideSharingSystem.DataAccess;
using RideSharingSystem.Layouts;
using RideSharingSystem.Models;

namespace RideSharingSystem
{
    class Program
    {
        public static List<Passenger> Passengers = new List<Passenger>();
        public static List<Driver> Drivers = new List<Driver>();
        public static List<Ride> PendingRides = new List<Ride>();
        public static List<Ride> CompletedRides = new List<Ride>();

        static void Main(string[] args)
        {
            var data = DataManager.LoadData();
            Passengers = data.Passengers;
            Drivers = data.Drivers;
            PendingRides = data.PendingRides;
            CompletedRides = data.CompletedRides;

            while (true)
            {
                Console.WriteLine("\n=== Ride Sharing System ===");
                Console.WriteLine("1. Register as Passenger");
                Console.WriteLine("2. Register as Driver");
                Console.WriteLine("3. Login");
                Console.WriteLine("4. Exit");
                Console.Write("Select option: ");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AuthLayout.RegisterPassenger();
                            SaveData();
                            break;
                        case 2:
                            AuthLayout.RegisterDriver();
                            SaveData();
                            break;
                        case 3:
                            AuthLayout.Login();
                            break;
                        case 4:
                            SaveData();
                            Console.WriteLine("Data saved. Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        public static void SaveData()
        {
            DataManager.SaveData(Passengers, Drivers, PendingRides, CompletedRides);
        }
    }
}
