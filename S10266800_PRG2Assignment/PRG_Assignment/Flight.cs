using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10266800H
// Student Name : Sew Jing Zhan
//==========================================================

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
        public override string ToString()
        {
            return $"{FlightNumber} {Orign} {Destination} {ExpectedTime}";
        }
        public virtual double CalculateFees(double origin)
        {
            if (Destination == "Singapore(SIN)")
            {
                origin += 500;
            }
            if (Orign == "Singapore (SIN)")
            {
                origin += 800;
            }
            if (Orign == "Dubai (DXB)" || Orign == "Bangkok (BKK)" || Orign == "Tokyo (NRT)")
            {
                origin -= 25;
            }
            if (ExpectedTime.TimeOfDay < new TimeSpan(11, 0, 0) || ExpectedTime.TimeOfDay > new TimeSpan(21, 0, 0))
            {
                origin -= 110;
            }
            return origin;
        }
    }
}
