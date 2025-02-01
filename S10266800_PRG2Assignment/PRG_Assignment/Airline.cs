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
    internal class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public bool AddFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                return false;
            }
            Flights[flight.FlightNumber] = flight;
            return true;
        }

        public double CalculateFees(double origin)
        {
            if (Flights.Count > 5)
            {
                origin *= 0.03;
            }
            origin -= Math.Floor((double)Flights.Count / 3) * 350;
            return origin;
        }
    }
}
