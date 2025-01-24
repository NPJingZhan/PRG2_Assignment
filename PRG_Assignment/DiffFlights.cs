using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_Assignment
{
    internal class NORMFlight : Flight
    {
        public NORMFlight(string flightnum, string origin, string destination, DateTime expectedtime, string status) : base(flightnum, origin, destination, expectedtime, status) { }

    }

    internal class LWTTFlight : Flight
    {
        public double RequestFee {  get; set; }
        public LWTTFlight(double requestFee, string flightnum, string origin, string destination, DateTime expectedtime, string status) : base(flightnum, origin, destination, expectedtime, status)
        {
            RequestFee = requestFee;
        }
    }

    internal class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight(double requestFee, string flightnum, string origin, string destination, DateTime expectedtime, string status) : base(flightnum, origin, destination, expectedtime, status)
        {
            RequestFee = requestFee;
        }
    }

    internal class CFFTlight : Flight
    {
        public double RequestFee { get; set; }
        public CFFTlight(double requestFee, string flightnum, string origin, string destination, DateTime expectedtime, string status) : base(flightnum, origin, destination, expectedtime, status)
        {
            RequestFee = requestFee;
        }
    }
}
