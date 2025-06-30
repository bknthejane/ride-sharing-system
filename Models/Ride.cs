using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingSystem.Models
{
    internal class Ride
    {
        public Passenger Passenger { get; set; }
        public Driver Driver { get; set; }
        public string PickupLocation { get; set; }
        public string DropOffLocation { get; set; }
        public int Distance { get; set; }
        public decimal Fare => Distance * 2;

        public bool Completed { get; set; }
    }
}
