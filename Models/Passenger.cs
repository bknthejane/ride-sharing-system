using System;
using System.Collections.Generic;
using RideSharingSystem.Interfaces;

namespace RideSharingSystem.Models
{
    internal class Passenger: User, IPayable, IRideable
    {
        public List<Ride> RideHistory { get; set; } = new List<Ride>();
        public decimal Wallet { get; set; }

        public Passenger(string username, string password) : base(username, password)
        {
            this.Wallet = 50;
        }

        public void AddFunds(decimal amount)
        {
            this.Wallet += amount;
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

            Program.PendingRides.Add(ride);

            Console.WriteLine($"Ride requested from {pickup} to {dropoff}, approximate distance {ride.Distance}km.");
        }
    }
}
