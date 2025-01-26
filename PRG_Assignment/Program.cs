using PRG_Assignment;

Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, Flight> flightsDict = new Dictionary<string, Flight>();
Dictionary<string, BoardingGate> boardinggateDict = new Dictionary<string, BoardingGate>();

Console.WriteLine("Loading Airlines...");
string[] airlines = File.ReadAllLines("airlines.csv");
for (int i = 1; i < airlines.Length; i++)
{
    string[] airline = airlines[i].Split(",");
    Airline airlineobj = new(airline[0], airline[1]);
    airlineDict[airline[0]] = airlineobj;
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
            break;
        case "CFFT":
            Flight cfft = new CFFTFlight(150, flightno, origin, destination, expectedarrival, "On Time");
            flightsDict[flightno] = cfft;
            break;
        case "DDJB":
            Flight ddjb = new DDJBFlight(300, flightno, origin, destination, expectedarrival, "On Time");
            flightsDict[flightno] = ddjb;
            break;
        case "LWTT":
            Flight lwtt = new LWTTFlight(500, flightno, origin, destination, expectedarrival, "On Time");
            flightsDict[flightno] = lwtt;
            break;
    }
}
Console.WriteLine(flights.Count() - 1 + " Flights Loaded!");
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

while (true)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    Console.WriteLine("");
    Console.WriteLine("Please enter your option:");

    try
    {
        int option = Convert.ToInt32(Console.ReadLine());
        if (option < 0 || option > 7)
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
            case 1: //List Flights
                Console.WriteLine("=============================================");
                Console.WriteLine("List of Flights for Changi Airport Terminal 5");
                Console.WriteLine("=============================================");
                string[] flightsheading = flights[0].Split(',');
                Console.WriteLine($"{flightsheading[0],-16} {flightsheading[1],-20} {flightsheading[2],-25} {flightsheading[3]} Time");
                foreach (var flight in flightsDict.Values)
                {
                    Console.WriteLine(flight.ToString());
                }
                Console.WriteLine("");
                while (true)
                {
                    Console.WriteLine("Enter B to return to Main Menu");
                    string b = Console.ReadLine();
                    if (b.ToUpper() != "B")
                    {
                        Console.WriteLine("Invalid Input.");
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine("");
                break;
            case 2:
                Console.WriteLine("=============================================");
                Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
                Console.WriteLine("=============================================");
                string[] boardinggateheading = boardinggates[0].Split(",");
                Console.WriteLine($"{boardinggateheading[0], -16} {boardinggateheading[1], -8} {boardinggateheading[2],-8} {boardinggateheading[3],-8}");
                foreach (var boardinggate in boardinggateDict.Values)
                {
                    Console.WriteLine(boardinggate.ToString());
                }
                Console.WriteLine("");
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
                    Console.WriteLine("Enter Boarding Gate Name:");
                    string bgno = Console.ReadLine().ToUpper();
                    bgvalid = SearchBoardingGate(boardinggateDict, bgno);
                    if (bgvalid is not null)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Boarding Gate Number");
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
                Console.WriteLine($"Flight {flightvalid.FlightNumber} has been assigned to Boarding Gate {bgvalid.GateName}!");
                Console.WriteLine("");
                break;
            case 4:
                Console.WriteLine("");
                break;
            case 5:
                Console.WriteLine("");
                break;
            case 6:
                Console.WriteLine("");
                break;
            case 7:
                Console.WriteLine("");
                break;
        }
    }
    catch
    {
        Console.WriteLine("INVALID OPTION");
        Console.WriteLine("");
    }
}
