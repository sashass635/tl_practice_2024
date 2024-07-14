using System.Globalization;
using Accommodations.Commands;
using Accommodations.Dto;

namespace Accommodations;

public static class AccommodationsProcessor
{
    private static BookingService _bookingService = new();
    private static Dictionary<int, ICommand> _executedCommands = new();
    private static int s_commandIndex = 0;

    public static void Run()
    {
        Console.WriteLine("Booking Command Line Interface");
        Console.WriteLine("Commands:");
        Console.WriteLine("'book <UserId> <Category> <StartDate> <EndDate> <Currency>' - to book a room");
        Console.WriteLine("'cancel <BookingId>' - to cancel a booking");
        Console.WriteLine("'undo' - to undo the last command");
        Console.WriteLine("'find <BookingId>' - to find a booking by ID");
        Console.WriteLine("'search <StartDate> <EndDate> <CategoryName>' - to search bookings");
        Console.WriteLine("'exit' - to exit the application");

        string input;
        while ((input = Console.ReadLine()) != "exit")
        {
            try
            {
                ProcessCommand(input);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static void ProcessCommand(string input)
    {
        string[] parts = input.Split(' ');
        string commandName = parts[0];

        switch (commandName)
        {
            case "book":
                if (parts.Length != 6)
                {
                    Console.WriteLine("Invalid number of arguments for booking.");
                    return;
                }

                //Добавлено исключение, когда введена неверная валюта
                CurrencyDto currency;
                if (!Enum.TryParse(parts[5], true, out currency))
                {
                    throw new ArgumentException($"Unknown currency:  {parts[5]}");
                }

                BookingDto bookingDto = new()
                {
                    UserId = int.Parse(parts[1]),
                    Category = parts[2],
                    StartDate = ParseDate(parts[3]), //Использование метода, проверяющего корректность введенной даты
                    EndDate = ParseDate(parts[4]),   //Использование метода, проверяющего корректность введенной даты
                    Currency = currency,
                };

                BookCommand bookCommand = new(_bookingService, bookingDto);
                bookCommand.Execute();
                _executedCommands.Add(++s_commandIndex, bookCommand);
                Console.WriteLine("Booking command run is successful.");
                break;

            case "cancel":
                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid number of arguments for canceling.");
                    return;
                }

                Guid bookingId = ParseGuid(parts[1]); //Использование метода, проверяющего корректность введенного id
                CancelBookingCommand cancelCommand = new(_bookingService, bookingId);
                cancelCommand.Execute();
                _executedCommands.Add(++s_commandIndex, cancelCommand);
                Console.WriteLine("Cancellation command run is successful.");
                break;

            case "undo":
                //Добавлена проверка на то, если количество команд будет 0
                if (s_commandIndex == 0)
                {
                    Console.WriteLine("The command history is empty.");
                    return;
                }
                _executedCommands[s_commandIndex].Undo();
                _executedCommands.Remove(s_commandIndex);
                s_commandIndex--;
                Console.WriteLine("Last command undone.");

                break;
            case "find":
                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid arguments for 'find'. Expected format: 'find <BookingId>'");
                    return;
                }
                Guid id = ParseGuid(parts[1]); //Использование метода, проверяющего корректность введенного id
                FindBookingByIdCommand findCommand = new(_bookingService, id);
                findCommand.Execute();
                break;

            case "search":
                if (parts.Length != 4)
                {
                    Console.WriteLine("Invalid arguments for 'search'. Expected format: 'search <StartDate> <EndDate> <CategoryName>'");
                    return;
                }
                DateTime startDate = ParseDate(parts[1]); //Использование метода, проверяющего корректность введенной даты
                DateTime endDate = ParseDate(parts[2]);   //Использование метода, проверяющего корректность введенной даты
                string categoryName = parts[3];
                SearchBookingsCommand searchCommand = new(_bookingService, startDate, endDate, categoryName);
                searchCommand.Execute();
                break;

            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }

    //Добавлен метод, проверяющий корректность введенной даты
    private static DateTime ParseDate(string dateStr)
    {
        if (!DateTime.TryParse(dateStr, out DateTime date))
        {
            throw new ArgumentException($"Invalid date format: {dateStr}");
        }
        return date;
    }

    //Добавлен метод, проверяющий корректность введенного id
    private static Guid ParseGuid(string guidStr)
    {
        if (!Guid.TryParse(guidStr, out Guid id))
        {
            throw new ArgumentException($"Invalid id:  {guidStr}");
        }
        return id;
    }
}
