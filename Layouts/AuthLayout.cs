using System;
using System.Linq;

namespace RideSharingSystem.Layouts
{
    internal class AuthLayout
    {

        public static void RegisterPassenger()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            // Create new passenger and add to the list
            Program.Passengers.Add(new Models.Passenger(username, password));
            Console.WriteLine("Passenger registered.");
        }

        public static void RegisterDriver()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            // Create new driver and add to the list
            Program.Drivers.Add(new Models.Driver(username, password));
            Console.WriteLine("Driver registered.");
        }

        public static void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            // Find matching username and password based on inputs
            var passenger = Program.Passengers.FirstOrDefault(p => p.Username == username && p.Password == password);
            if (passenger != null)
            {
                PassengerLayout.PassengerMenu(passenger);
                return;
            }

            // Find matching username and password based on inputs
            var driver = Program.Drivers.FirstOrDefault(d => d.Username == username && d.Password == password);
            if (driver != null)
            {
                DriverLayout.DriversMenu(driver);
                return;
            }

            Console.WriteLine("Invalid credentials.");
        }
    }
}
