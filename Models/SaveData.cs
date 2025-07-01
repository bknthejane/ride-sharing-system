using System.Collections.Generic;

namespace RideSharingSystem.Models
{
    internal class SaveData
    {
        public List<Passenger> Passengers { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<Ride> PendingRides { get; set; }
        public List<Ride> CompletedRides { get; set; }
    }
}
