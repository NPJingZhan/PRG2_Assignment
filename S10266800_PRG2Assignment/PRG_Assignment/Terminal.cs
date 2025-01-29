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
    internal class Terminal
    {
        public string TerminalName {  get; set; }
        public Dictionary <string, Airline> Airlines { get; set; }
        public Dictionary <string, Flight> Flights { get; set; }
        public Dictionary <string, BoardingGate> BoardingGates { get; set; }
        public Dictionary <string, double> GateFees { get; set; }

        public Terminal(string terminalName, Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, double> gateFees)
        {
            TerminalName = terminalName;
            Airlines = airlines;
            Flights = flights;
            BoardingGates = boardingGates;
            GateFees = gateFees;
        }
    }
}
