using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingSystem.Models
{
    internal class Passenger: User
    {
        public decimal Wallet { get; set; }

        public Passenger(string username, string password) : base(username, password) { }

        public void AddFunds(decimal amount)
        {
            this.Wallet = amount;
        }

        public void RequestRide(string pickup, string dropoff)
        {
            
        }
    }
}
