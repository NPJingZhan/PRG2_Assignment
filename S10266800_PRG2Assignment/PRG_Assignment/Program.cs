using PRG_Assignment;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml.Linq;

//==========================================================
// Student Number : S10266800H
// Student Name : Sew Jing Zhan
//==========================================================

Dictionary<string, Airline> airlineDict = new();
Dictionary<string, Flight> flightsDict = new();
Dictionary<string, BoardingGate> boardinggateDict = new();

Console.WriteLine("Loading Airlines...");
string[] airlines = File.ReadAllLines("airlines.csv");
for (int i = 1; i < airlines.Length; i++)
{
    string[] airline = airlines[i].Split(",");
    Airline airlineobj = new(airline[0], airline[1]);
    airlineDict[airline[1]] = airlineobj;
}
Console.WriteLine(airlines.Count() - 1 + " Airlines Loaded!");


Console.WriteLine("Loading Boarding Gates...");
string[] boardinggates = File.ReadAllLines("boardinggates.csv");
for (int i = 1; i < boardinggates.Length; i++)
{
    string[] boardinggate = boardinggates[i].Split(",");
    BoardingGate boardinggateobj = new(boardinggate[0], Convert.ToBoolean(boardinggate[1]), Convert.ToBoolean(boardinggate[2]), Convert.ToBoolean(boardinggate[3]), null);
    boardinggateDict[boardinggate[0]] = boardinggateobj;
}
Console.WriteLine(boardinggates.Count() - 1 + " Boarding Gates Loaded!");

Console.WriteLine("Loading Flights...");
string[] flights = File.ReadAllLines("flights.csv");
for (int i = 1; i < flights.Length; i++)
{
    string[] flight = flights[i].Split(",");
    string flightno = flight[0];
    string origin = flight[1];
    string destination = flight[2];
    DateTime expectedarrival = Convert.ToDateTime(flight[3]);
    string src = flight[4];
    switch (src)
    {
        case "":
            Flight norm = new NORMFlight(flightno, origin, destination, expectedarrival, "On Time");
            flightsDict[flightno] = norm;
            foreach (string airline in airlineDict.Keys)
            {
                if (airline == flightno[..2])
                {
                    airlineDict[airline].AddFlight(norm);
                }
            }
            break;
        case "CFFT":
            Flight cfft = new CFFTFlight(150, flightno, origin, destination, expectedarrival, "On Time");
            flightsDict[flightno] = cfft;
            foreach (string airline in airlineDict.Keys)
            {
                if (airline == flightno[..2])
                {
                    airlineDict[airline].AddFlight(cfft);
                }
            }
            break;
        case "DDJB":
            Flight ddjb = new DDJBFlight(300, flightno, origin, destination, expectedarrival, "On Time");
            flightsDict[flightno] = ddjb;
            foreach (string airline in airlineDict.Keys)
            {
                if (airline == flightno[..2])
                {
                    airlineDict[airline].AddFlight(ddjb);
                }
            }
            break;
        case "LWTT":
            Flight lwtt = new LWTTFlight(500, flightno, origin, destination, expectedarrival, "On Time");
            flightsDict[flightno] = lwtt;
            foreach (string airline in airlineDict.Keys)
            {
                if (airline == flightno[..2])
                {
                    airlineDict[airline].AddFlight(lwtt);
                }
            }
            break;
    }
   
}
Console.WriteLine(flights.Count() - 1 + " Flights Loaded!");

void ListingAirlines()
{
    string[] airlineheading = airlines[0].Split(",");
    Console.WriteLine($"{airlineheading[0],-18} {airlineheading[1]}");
    foreach (var airline in airlineDict)
    {
        Console.WriteLine($"{airline.Key,-18} {airline.Value.Name}");
    }
}

void ReturnToMenu()
{
    while (true)
    {
        Console.WriteLine("Enter B to return to Main Menu");
        string? b = Console.ReadLine();
        if (b.ToUpper() != "B")
        {
            Console.WriteLine("Invalid Input.");
        }
        else
        {
            break;
        }
    }
}

Airline SearchAirline(Dictionary<string, Airline> airlineDict, string input)
{
    foreach (string airline in airlineDict.Keys)
    {
        if (input == airline)
        {
            return airlineDict[input];
        }
    }
    return null;
}
Flight Searchflight(Dictionary<string, Flight> flightsDict, string flightno)
{
    foreach (string flight in flightsDict.Keys)
    {
        if (flightno == flight)
        {
            return flightsDict[flightno];
        }
    }
    return null;
}

BoardingGate SearchBoardingGate(Dictionary<string, BoardingGate> boardinggateDict, string bgno)
{
    foreach (string boarding_gate in boardinggateDict.Keys)
    {
        if (boarding_gate == bgno)
        {
            return boardinggateDict[bgno];
        }
    }
    return null;
}

Queue<Flight> flightqueue = new();

foreach (var flight in flightsDict.Values)
{
    bool check = false;
    foreach (var gate in boardinggateDict.Values)
    {
        if (gate.Flight == flight)
        {
            check = true;
            break;
        }
    }
    if (check is false)
    {
        flightqueue.Enqueue(flight);
    }
}

Console.WriteLine($"Number of flights not assigned to boarding gate: {flightqueue.Count}");
int bgcount = 0;
foreach (var gate in boardinggateDict.Values)
{
    if (gate.Flight is null)
    {
        bgcount++;
    }
}
Console.WriteLine($"Number of Boarding Gates that do not have any flights assigned: {bgcount}");

while (flightqueue.Count > 0)
{
    Flight flightassignment = flightqueue.Dequeue();
    foreach (var gate in boardinggateDict.Values)
    {
        if (gate.Flight == null)
        {
            if (flightassignment is DDJBFlight && gate.SupportsDDJB)
            {
                gate.Flight = flightassignment;
                break;
            }
            else if (flightassignment is CFFTFlight && gate.SupportsCFFT)
            {
                gate.Flight = flightassignment;
                break;
            }
            else if (flightassignment is LWTTFlight && gate.SupportsLWTT)
            {
                gate.Flight = flightassignment;
                break;
            }
            else if (flightassignment is NORMFlight)
            {
                gate.Flight = flightassignment;
                break;
            }
        }
    }
}

int bgflightassignment = 0;
string[] flightsheading = flights[0].Split(',');
Console.WriteLine($"{flightsheading[0],-16} {flightsheading[1],-20} {flightsheading[2],-25} {flightsheading[3]}");
foreach (var flight in flightsDict.Values)
{
    bool check = false;
    BoardingGate boardingGate = null;
    foreach (var gate in boardinggateDict.Values)
    {
        if (gate.Flight == flight)
        {
            check = true;
            boardingGate = gate;
            break;
        }
    }
    if (check)
    {
        Console.WriteLine($"{flight,-20}\t {boardingGate.GateName}");
        bgflightassignment++;
    }
    else
    {
        Console.WriteLine(flight.ToString());
    }
}
Console.WriteLine("");
Console.WriteLine("Number of Flights and Boarding Gates processed and assigned: " + bgflightassignment);

Console.WriteLine($"Percentage Flights and Boarding Gates processed and assigned: {(bgflightassignment/Convert.ToDouble(flightsDict.Count) * 100).ToString("F2")}%");

Console.WriteLine("");
Console.WriteLine("");

while (true)
{    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Calculate Fees");
    Console.WriteLine("0. Exit");
    Console.WriteLine("");
    Console.WriteLine("Please enter your option:");

    try
    {
        int option = Convert.ToInt32(Console.ReadLine());
        if (option < 0 || option > 6)
        {
            throw new ArgumentOutOfRangeException("x");
        }
        if (option == 0)
        {
            Console.WriteLine("Goodbye");
            break;
        }
        switch (option)
        {
            case 1:
                Console.WriteLine("=============================================");
                Console.WriteLine("List of Flights for Changi Airport Terminal 5");
                Console.WriteLine("=============================================");
                flightsheading = flights[0].Split(',');
                Console.WriteLine($"{flightsheading[0],-16} {flightsheading[1],-20} {flightsheading[2],-25} {flightsheading[3]} Time");
                foreach (var flight in flightsDict.Values)
                {
                    Console.WriteLine(flight.ToString());
                }
                Console.WriteLine("");
                ReturnToMenu();
                Console.WriteLine("");
                break;
            case 2:
                Console.WriteLine("=============================================");
                Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
                Console.WriteLine("=============================================");
                string[] boardinggateheading = boardinggates[0].Split(",");
                Console.WriteLine($"{boardinggateheading[0],-16} {boardinggateheading[1],-8} {boardinggateheading[2],-8} {boardinggateheading[3],-8}");
                foreach (var boardinggate in boardinggateDict.Values)
                {
                    Console.WriteLine(boardinggate.ToString());
                }
                Console.WriteLine("");
                ReturnToMenu();
                Console.WriteLine("");
                break;
            case 3:
                Flight flightvalid = null;
                BoardingGate bgvalid = null;

                Console.WriteLine("=============================================");
                Console.WriteLine("Assign a Boarding Gate to a Flight");
                Console.WriteLine("=============================================");

                while (true)
                {
                    Console.WriteLine("Enter flight number: ");
                    string flightno = Console.ReadLine().ToUpper();
                    flightvalid = Searchflight(flightsDict, flightno);
                    if (flightvalid is not null)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Flight Number");
                    }
                }
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Enter Boarding Gate Name:");
                        string bgno = Console.ReadLine().ToUpper();
                        bgvalid = SearchBoardingGate(boardinggateDict, bgno);
                        if (bgvalid is not null)
                        {
                            if (bgvalid.Flight != null)
                            {
                                throw new ArgumentException();
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Boarding Gate Number");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Boarding Gate has already been assigned. Enter another Boarding Gate");
                    }
                }
                Console.WriteLine($"Flight Number: {flightvalid.FlightNumber}");
                Console.WriteLine($"Origin: {flightvalid.Orign}");
                Console.WriteLine($"Destination: {flightvalid.Destination}");
                Console.WriteLine($"Expected Time: {flightvalid.ExpectedTime}");
                if (flightvalid is NORMFlight)
                {
                    Console.WriteLine("Special Request Code: None");
                }
                else if (flightvalid is CFFTFlight)
                {
                    Console.WriteLine("Special Request Code: CFFT");
                }
                else if (flightvalid is DDJBFlight)
                {
                    Console.WriteLine("Special Request Code: DDJB");
                }
                else
                {
                    Console.WriteLine("Special Request Code: LWTT");
                }
                Console.WriteLine($"Boarding Gate Name: {bgvalid.GateName}");
                Console.WriteLine($"Supports DDJB: {bgvalid.SupportsDDJB}");
                Console.WriteLine($"Supports CFFT: {bgvalid.SupportsCFFT}");
                Console.WriteLine($"Supports LWTT: {bgvalid.SupportsLWTT}");
                while (true)
                {
                    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
                    try
                    {
                        string? YorN = Console.ReadLine()?.ToUpper();
                        if (YorN == "Y")
                        {
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("1. Delayed");
                                    Console.WriteLine("2. Boarding");
                                    Console.WriteLine("3. On Time");
                                    Console.WriteLine("Please select the new status of the flight: ");
                                    int statusoption = Convert.ToInt32(Console.ReadLine());
                                    if (statusoption == 1)
                                    {
                                        flightvalid.Status = "Delayed";
                                        break;
                                    }
                                    else if (statusoption == 2)
                                    {
                                        flightvalid.Status = "Boarding";
                                        break;
                                    }
                                    else if (statusoption == 3)
                                    {
                                        flightvalid.Status = "On Time";
                                        break;
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Invalid Input");
                                }
                            }
                            break;
                        }
                        else if (YorN == "N")
                        {
                            break;
                        }
                        else
                        {
                            throw new Exception("Invalid Input");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                bgvalid.Flight = flightvalid;
                Console.WriteLine($"Flight {flightvalid.FlightNumber} has been assigned to Boarding Gate {bgvalid.GateName}!");
                Console.WriteLine("");
                ReturnToMenu();
                Console.WriteLine("");
                break;
            case 4:
                void ValidateFlightNumFormat(string input)
                {
                    string pattern = @"^[A-Z]{2} \d{3}$";

                    if (!Regex.IsMatch(input, pattern))
                    {
                        throw new FormatException($"'{input}' is not in the required format 'AA 123' (two letters, a space, and three digits).");
                    }
                }

                void ValidateCountryFormat(string input)
                {
                    string pattern = @"^[A-Za-z]+ \([A-Z]{3}\)$";

                    if (!Regex.IsMatch(input, pattern))
                    {
                        throw new FormatException($"'{input}' is not in the required format 'AA 123' (two letters, a space, and three digits).");
                    }
                }

                while (true)
                {
                    string flightnum = null;
                    string flightorigin = null;
                    string flightdest = null;
                    string src = null;
                    DateTime flightexpect = DateTime.MinValue;

                    while (true)
                    {
                        Console.Write("Enter Flight Number (XX 123): ");
                        try
                        {
                            flightnum = Console.ReadLine().ToUpper();
                            ValidateFlightNumFormat(flightnum);
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Input must be in the format 'XX 123' e.g. 'SP 420'");
                        }
                    }
                    while (true)
                    {
                        try
                        {
                            Console.Write("Enter Origin: ");
                            flightorigin = Console.ReadLine();
                            ValidateCountryFormat(flightorigin);
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Input must be in the format 'City (XXX)' e.g. 'Dubai (DUB)'");
                        }
                    }
                    while (true)
                    {
                        try
                        {
                            Console.Write("Enter Destination: ");
                            flightdest = Console.ReadLine();
                            ValidateCountryFormat(flightdest);
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Input must be in the format 'City (XXX)' e.g. 'Dubai (DUB)'");
                        }
                    }

                    if (flightdest == flightorigin)
                    {
                        Console.WriteLine("You cannot fly to the same city");
                        Console.WriteLine("Starting Over...");
                        Console.WriteLine("");
                    }
                    else
                    {
                        while (true)
                        {
                            try
                            {
                                Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                                flightexpect = Convert.ToDateTime(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Input must be in the format 'dd/mm/yyyy hh:mm' e.g. '21/9/2024 15:40'");
                            }
                        }
                        while (true)
                        {
                            Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
                            src = Console.ReadLine().ToUpper();
                            if (src == "NONE")
                            {
                                Flight norm = new NORMFlight(flightnum, flightorigin, flightdest, flightexpect, "On Time");
                                flightsDict[flightnum] = norm;
                                break;
                            }
                            else if (src == "CFFT")
                            {
                                Flight cfft = new CFFTFlight(150, flightnum, flightorigin, flightdest, flightexpect, "On Time");
                                flightsDict[flightnum] = cfft;
                                break;
                            }
                            else if (src == "DDJB")
                            {
                                Flight ddjb = new DDJBFlight(300, flightnum, flightorigin, flightdest, flightexpect, "On Time");
                                flightsDict[flightnum] = ddjb;
                                break;
                            }
                            else if (src == "LWTT")
                            {
                                Flight lwtt = new LWTTFlight(500, flightnum, flightorigin, flightdest, flightexpect, "On Time");
                                flightsDict[flightnum] = lwtt;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Enter a valid Special Request Code");
                            }
                        }

                        using (StreamWriter writer = new StreamWriter("flights.csv", append: true))
                        {
                            if (src == "NONE")
                            {
                                writer.WriteLine($"{flightnum},{flightorigin},{flightdest},{flightexpect},");
                            }
                            else
                            {
                                writer.WriteLine($"{flightnum},{flightorigin},{flightdest},{flightexpect},{src}");
                            }
                        }
                        Console.WriteLine($"Flight {flightnum} has been added!");

                        string choice = null;
                        while (true)
                        {
                            Console.Write("Would you like to add another flight? (Y/N) ");
                            choice = Console.ReadLine().ToUpper();
                            if (choice != "Y" && choice != "N")
                            {
                                Console.WriteLine("Invalid Option");
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (choice == "Y")
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                Console.WriteLine("");
                ReturnToMenu();
                Console.WriteLine("");
                break;
            case 5:
                Airline airlinevalid = null;
                Console.WriteLine("=============================================");
                Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
                Console.WriteLine("=============================================");
                ListingAirlines();
                while (true)
                {
                    Console.WriteLine("Enter Airline Code:");
                    string airlinecode = Console.ReadLine().ToUpper();
                    airlinevalid = SearchAirline(airlineDict, airlinecode);
                    if (airlinevalid == null)
                    {
                        Console.WriteLine("Airline not found. Please enter a valid Airline");
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine("=============================================");
                Console.WriteLine($"List of Flights for {airlinevalid.Name}");
                Console.WriteLine("=============================================");
                flightsheading = flights[0].Split(',');
                Console.WriteLine($"{flightsheading[0],-16} {flightsheading[1],-20} {flightsheading[2],-25} {flightsheading[3]} Time");
                foreach (var airlinee in airlinevalid.Flights)
                {
                    Console.WriteLine(airlinee.Value.ToString());
                }
                Console.WriteLine("");
                ReturnToMenu();
                Console.WriteLine("");
                break;
            case 6:
                bool allFlightsAssigned = true;
                foreach (var flight in flightsDict.Values)
                {
                    bool assigned = false;
                    foreach (var gate in boardinggateDict.Values)
                    {
                        if (gate.Flight == flight)
                        {
                            assigned = true;
                            break;
                        }
                    }
                    if (!assigned)
                    {
                        allFlightsAssigned = false;
                        break;
                    }
                }

                if (allFlightsAssigned)
                {
                    Console.WriteLine("=============================================");
                    Console.WriteLine("Fees for each Airline");
                    Console.WriteLine("=============================================");
                    foreach (Airline airline1 in airlineDict.Values)
                    {
                        double fees = 0;
                        double discount = 0;
                        foreach (Flight flight in airline1.Flights.Values)
                        {
                            fees += flight.CalculateFees();

                            if (airline1.Flights.Count > 5)
                            {
                                discount += fees * 0.03;
                                Console.WriteLine(fees * 0.03);
                            }

                            if (airline1.Flights.Count % 3 == 0)
                            {
                                discount += 350 * (airline1.Flights.Count / 3);
                            }

                            if (flight.Orign == "Dubai (DXB)" || flight.Orign == "Bangkok (BKK)" || flight.Orign == "Tokyo (NRT)")
                            {
                                discount += 25;
                            }

                            if (flight is NORMFlight)
                            {
                                discount += 50;
                            }
                        }

                        Console.WriteLine($"{airline1.Name} Fees");
                        Console.WriteLine("Total Fees: $" + fees);
                        Console.WriteLine("Discount: $" + discount);
                        Console.WriteLine($"Subtotal: ${fees - discount}");
                        Console.WriteLine("");
                    }
                    ReturnToMenu();
                }
                else
                {
                    Console.WriteLine("Not all flights are assigned to boarding gates.");
                    ReturnToMenu();
                }
                break;
        }
    }
    catch
    {
        Console.WriteLine("INVALID OPTION");
        Console.WriteLine("");
    }
}