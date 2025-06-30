using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingSystem.Models
{
    internal class Driver : User
    {
        public Driver(string username, string password) : base(username, password)
        {
        }

        public bool Available { get; set; }

        public decimal Earnings { get; set; }

        public double Rating { get; set; }

        public int RidesCompleted { get; set; }
    }
}
