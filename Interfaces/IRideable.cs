using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharingSystem.Interfaces
{
    internal interface IRideable
    {
        void RequestRide(string pickup, string dropOff);
    }
}
