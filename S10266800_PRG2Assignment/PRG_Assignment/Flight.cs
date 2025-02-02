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
        public virtual double CalculateFees()
        {
            double fees = 300;
            if (Destination == "Singapore (SIN)")
            {
                fees += 500;
            }
            if (Orign == "Singapore (SIN)")
            {
                fees += 800;
            }
            return fees;
        }
    }
}
