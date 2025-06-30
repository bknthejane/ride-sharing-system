using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharingSystem.Interfaces;

namespace RideSharingSystem.Models
{
    internal class Passenger: User, IPayable, IRideable
    {
        public decimal Wallet { get; set; }
        public List<Ride> RideHistory { get; set; } = new List<Ride>();

        public Passenger(string username, string password) : base(username, password) { }

        public void AddFunds(decimal amount)
        {
            this.Wallet = amount;
        }

        public void RequestRide(string pickup, string dropoff)
        {
            var ride = new Ride
            {
                Passenger = this,
                PickupLocation = pickup,
                DropOffLocation = dropoff,
                Distance = new Random().Next(1, 20)
            };

            Console.WriteLine($"Ride requested from {pickup} to {dropoff}, approximate distance {ride.Distance}km.");
        }
    }
}
