using System.Text.Json.Serialization;

namespace RideSharingSystem.Models
{
    internal class Ride
    {
        [JsonIgnore]
        public Passenger Passenger { get; set; }
        public Driver Driver { get; set; }
        public string PickupLocation { get; set; }
        public string DropOffLocation { get; set; }
        public int Distance { get; set; }
        public decimal Fare => Distance * 2;
        public bool Completed { get; set; }
    }
}
