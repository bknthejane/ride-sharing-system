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

            Program.Passengers.Add(new Models.Passenger(username, password));
            Console.WriteLine("Passenger registered.");
        }

        public static void RegisterDriver()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Program.Drivers.Add(new Models.Driver(username, password));
            Console.WriteLine("Driver registered.");
        }

        public static void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var passenger = Program.Passengers.FirstOrDefault(p => p.Username == username && p.Password == password);
            if (passenger != null)
            {
                PassengerLayout.PassengerMenu(passenger);
                return;
            }

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
