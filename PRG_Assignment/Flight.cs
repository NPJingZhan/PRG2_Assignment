using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_Assignment
{
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Orign { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        public Flight(string flightnum, string origin, string destination, DateTime expectedtime, string status)
        {
            FlightNumber = flightnum;
            Orign = origin;
            Destination = destination;
            ExpectedTime = expectedtime;
            Status = status;
        }
        public virtual string ToString()
        {
            return $"{FlightNumber} {Orign} {Destination} {ExpectedTime}";
        }
    }
}
