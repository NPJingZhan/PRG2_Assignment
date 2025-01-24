using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_Assignment
{
    internal class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight(double requestFee, string flightnum, string origin, string destination, DateTime expectedtime, string status) : base(flightnum, origin, destination, expectedtime, status)
        {
            RequestFee = requestFee;
        }
        public override string ToString()
        {
            return $"{FlightNumber,-16} {Orign,-20} {Destination,-25} {ExpectedTime}";
        }
    }
}

