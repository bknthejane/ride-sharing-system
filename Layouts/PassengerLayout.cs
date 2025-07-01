using System;
using RideSharingSystem.Models;

namespace RideSharingSystem.Layouts
{
    internal class PassengerLayout
    {
        public static void PassengerMenu(Passenger passenger)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"--- Passenger Menu ({passenger.Username}) ---");
                Console.WriteLine("1. Request a Ride");
                Console.WriteLine("2. View Wallet Balance");
                Console.WriteLine("3. Add funds to Wallet");
                Console.WriteLine("4. View Ride History");
                Console.WriteLine("5. Logout");
                Console.Write("Select option: ");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter PickUp Location: ");
                            string pickup = Console.ReadLine();

                            Console.Write("Enter DropOff Location: ");
                            string dropOff = Console.ReadLine();

                            passenger.RequestRide(pickup, dropOff);
                            Program.SaveData();
                            break;

                        case 2:
                            Console.WriteLine($"Wallet Balance: R{passenger.Wallet}");
                            Program.SaveData();
                            break;

                        case 3:
                            Console.Write("Amount: ");
                            try
                            {
                                decimal amount = Convert.ToDecimal(Console.ReadLine());
                                passenger.AddFunds(amount);
                                Console.WriteLine($"Added R{amount} to wallet.");
                            }
                            catch
                            {
                                Console.WriteLine("Invalid amount.");
                            }
                            Program.SaveData();
                            break;

                        case 4:
                            if (passenger.RideHistory.Count == 0)
                            {
                                Console.WriteLine("No ride history.");
                            }
                            else
                            {
                                foreach (var ride in passenger.RideHistory)
                                {
                                    Console.WriteLine($"Ride to {ride.DropOffLocation}, Fare: R{ride.Fare}, Completed: {ride.Completed}");
                                }
                            }
                            Program.SaveData();
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
        }
    }
}
