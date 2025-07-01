namespace RideSharingSystem.Models
{
    internal class Driver: User
    {
        public bool Available { get; set; }
        public decimal Earnings { get; set; }
        public double Rating { get; set; }
        public int RidesCompleted { get; set; }
        public Driver(string username, string password) : base(username, password)
        {
            
        }

        
    }
}
