using System;
using System.Linq;
using RideSharingSystem.Models;

namespace RideSharingSystem.Layouts
{
    internal class DriverLayout
    {
        // Displays the driver menu and handles user actions
        public static void DriversMenu(Driver driver)
        {
            while (true)
            {
                Console.Clear();
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

                    Console.Clear();

                    switch (choice)
                    {
                        case 1:
                            // Displays a list of available rides (rides without assigned drivers)
                            ViewRides();
                            break;

                        case 2:
                            // Allows the driver to view and accept an available ride if it's within acceptable distance
                            AcceptRide();
                            break;

                        case 3:
                            // Completes a ride by charging the passenger, updating records, and saving the system state
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

                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                }
            }

            
            void ViewRides()
            {
                // Filter pending rides to find those that don't have an assigned driver
                var availableRides = Program.PendingRides
                                .Where(r => r.Driver == null)
                                .ToList();

                // If no available rides, inform the user
                if (availableRides.Count == 0)
                {
                    Console.WriteLine("No rides available.");
                }
                else
                {
                    // Display details of each available ride
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
                // Get list of pending rides that have not yet been accepted by a driver
                var acceptRides = Program.PendingRides
                                .Where(r => r.Driver == null)
                                .ToList();

                // If no rides are available for acceptance, notify the user
                if (acceptRides.Count == 0)
                {
                    Console.WriteLine("No rides to accept.");
                }
                else
                {
                    // Display the list of rides that can be accepted
                    Console.WriteLine("\nAvailable Rides:");
                    for (int i = 0; i < acceptRides.Count; i++)
                    {
                        var r = acceptRides[i];
                        Console.WriteLine($"{i + 1}. From {r.PickupLocation} to {r.DropOffLocation}, Distance: {r.Distance}km, Fare: R{r.Fare}");
                    }

                    // Prompt the user to select a ride from the list
                    Console.Write("Enter the number of the ride to accept: ");
                    int selectedIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                    // Validate the selected index
                    if (selectedIndex < 0 || selectedIndex >= acceptRides.Count)
                    {
                        Console.WriteLine("Invalid ride number.");
                    }
                    else
                    {
                        var rideToAccept = acceptRides[selectedIndex];

                        // Check if the ride is within an acceptable distance
                        if (rideToAccept.Distance > 15)
                        {
                            Console.WriteLine("Ride too far away to accept.");
                        }
                        else
                        {
                            // Assign the driver to the ride and mark them as unavailable
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
                // Get a list of rides assigned to this driver that are not yet completed
                var myRides = Program.PendingRides
                                .Where(r => r.Driver == driver && !r.Completed)
                                .ToList();

                // If there are no active rides, inform the driver
                if (myRides.Count == 0)
                {
                    Console.WriteLine("No rides to complete.");
                }
                else
                {
                    // Display all active rides assigned to this driver
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
