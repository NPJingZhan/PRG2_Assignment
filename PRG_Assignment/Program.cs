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
            NORMFlight norm = new(flightno, origin, destination, expectedarrival, "On Time");
            flightsDict["NORMFlight"] = norm;
            break;
        case "CFFT":
            CFFTFlight cfft = new(150, flightno, origin, destination, expectedarrival, "On Time");
            flightsDict["CFFTFlight"] = cfft;
            break;
        case "DDJB":
            DDJBFlight ddjb = new(300, flightno, origin, destination, expectedarrival, "On Time");
            flightsDict["DDJBFlight"] = ddjb;
            break;
        case "LWTT":
            LWTTFlight lwtt = new(500, flightno, origin, destination, expectedarrival, "On Time");
            flightsDict["LWTTFlight"] = lwtt;
            break;
    }
}
Console.WriteLine(flights.Count() - 1 + " Flights Loaded!");

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
    int option = Convert.ToInt32(Console.ReadLine());
    try
    {
        if (option < 0 || option > 7)
        {
            throw new ArgumentOutOfRangeException("x");
        }
    }
    catch
    {
        Console.WriteLine("INVALID OPTION");
    }

    if (option == 0)
    {
        Console.WriteLine("Goodbye");
        break;
    }
    switch (option)
    {
        case 1:
            Console.WriteLine("");
            break;
        case 2:
            Console.WriteLine("");
            break;
        case 3:
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
