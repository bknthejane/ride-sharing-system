using System;
using System.Linq;
using RideSharingSystem.Models;

namespace RideSharingSystem.Layouts
{
    internal class DriverLayout
    {
        public static void DriversMenu(Driver driver)
        {
            while (true)
            {
                Console.WriteLine($"\n--- Driver Menu ({driver.Username}) ---");
                Console.WriteLine("1. View Available Ride Requests");
                Console.WriteLine("2. Accept a Ride");
                Console.WriteLine("3. Complete a Ride");
                Console.WriteLine("4. View Earnings");
                Console.WriteLine("5. Logout");
                Console.Write("Select option: ");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            ViewRides();
                            break;

                        case 2:
                            AcceptRide();
                            break;

                        case 3:
                            CompleteRide();
                            break;

                        case 4:
                            Console.WriteLine($"Total Earnings: R{driver.Earnings}");
                            break;

                        case 5:
                            Console.WriteLine("Goodbye!");
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

            void ViewRides()
            {
                var availableRides = Program.PendingRides
                                .Where(r => r.Driver == null)
                                .ToList();

                if (availableRides.Count == 0)
                {
                    Console.WriteLine("No rides available.");
                }
                else
                {
                    Console.WriteLine("\nAvailable Rides:");
                    for (int i = 0; i < availableRides.Count; i++)
                    {
                        var r = availableRides[i];
                        Console.WriteLine($"{i + 1}. From {r.PickupLocation} to {r.DropOffLocation}, Distance: {r.Distance}km, Fare: R{r.Fare}");
                    }
                }
            }

            void AcceptRide()
            {
                var acceptRides = Program.PendingRides
                                .Where(r => r.Driver == null)
                                .ToList();

                if (acceptRides.Count == 0)
                {
                    Console.WriteLine("No rides to accept.");
                }
                else
                {
                    Console.WriteLine("\nAvailable Rides:");
                    for (int i = 0; i < acceptRides.Count; i++)
                    {
                        var r = acceptRides[i];
                        Console.WriteLine($"{i + 1}. From {r.PickupLocation} to {r.DropOffLocation}, Distance: {r.Distance}km, Fare: R{r.Fare}");
                    }

                    Console.Write("Enter the number of the ride to accept: ");
                    int selectedIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (selectedIndex < 0 || selectedIndex >= acceptRides.Count)
                    {
                        Console.WriteLine("Invalid ride number.");
                    }
                    else
                    {
                        var rideToAccept = acceptRides[selectedIndex];

                        if (rideToAccept.Distance > 15)
                        {
                            Console.WriteLine("Ride too far away to accept.");
                        }
                        else
                        {
                            rideToAccept.Driver = driver;
                            driver.Available = false;
                            Console.WriteLine("Ride accepted.");
                            Program.SaveData();
                        }
                    }
                }
            }

            void CompleteRide()
            {
                var myRides = Program.PendingRides
                                .Where(r => r.Driver == driver && !r.Completed)
                                .ToList();

                if (myRides.Count == 0)
                {
                    Console.WriteLine("No rides to complete.");
                }
                else
                {
                    Console.WriteLine("\nYour Active Rides:");
                    for (int i = 0; i < myRides.Count; i++)
                    {
                        var r = myRides[i];
                        Console.WriteLine($"{i + 1}. From {r.PickupLocation} to {r.DropOffLocation}, Fare: R{r.Fare}");
                    }

                    Console.Write("Enter the number of the ride to complete: ");
                    int completeIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                    if (completeIndex < 0 || completeIndex >= myRides.Count)
                    {
                        Console.WriteLine("Invalid ride number.");
                    }
                    else
                    {
                        var rideToComplete = myRides[completeIndex];

                        try
                        {
                            if (rideToComplete.Passenger.Wallet < rideToComplete.Fare)
                                throw new Exception("Passenger does not have enough funds.");

                            rideToComplete.Passenger.Wallet -= rideToComplete.Fare;
                            driver.Earnings += rideToComplete.Fare;
                            rideToComplete.Completed = true;
                            rideToComplete.Passenger.RideHistory.Add(rideToComplete);
                            Program.CompletedRides.Add(rideToComplete);
                            Program.PendingRides.Remove(rideToComplete);
                            driver.RidesCompleted++;
                            driver.Available = true;

                            Console.WriteLine($"Ride completed. Passenger charged R{rideToComplete.Fare}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error completing ride: {ex.Message}");
                        }
                        Program.SaveData();
                    }
                }
            }
        }
    }
}
