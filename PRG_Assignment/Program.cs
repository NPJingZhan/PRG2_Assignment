using PRG_Assignment;

Console.WriteLine("Loading Airlines...");
string[] airlines = File.ReadAllLines("airlines.csv");
Console.WriteLine(airlines.Count()-1 + " Airlines Loaded!");

Console.WriteLine("Loading Boarding Gates...");
string[] boardinggates = File.ReadAllLines("boardinggates.csv");
Console.WriteLine(boardinggates.Count()-1 + " Boarding Gates Loaded!");

Console.WriteLine("Loading Flights...");
string[] flights = File.ReadAllLines("flights.csv");
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
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
            break;
    }
}
