using Accommodations.Models;

namespace Accommodations;

public class BookingService : IBookingService
{
    private List<Booking> _bookings = [];

    private readonly IReadOnlyList<RoomCategory> _categories =
   [
       new RoomCategory { Name = "Standard", BaseRate = 100, AvailableRooms = 10 },
        new RoomCategory { Name = "Deluxe", BaseRate = 200, AvailableRooms = 5 }
   ];

    private readonly IReadOnlyList<User> _users =
    [
        new User { Id = 1, Name = "Alice Johnson" },
        new User { Id = 2, Name = "Bob Smith" },
        new User { Id = 3, Name = "Charlie Brown" },
        new User { Id = 4, Name = "Diana Prince" },
        new User { Id = 5, Name = "Evan Wright" }
    ];

    public Booking Book(int userId, string categoryName, DateTime startDate, DateTime endDate, Currency currency)
    {
        if (endDate <= startDate) //Дата начала бронирования не может быть равна дате окончания бронирования
        {
            throw new ArgumentException("End date cannot be earlier or equal to start date");
        }

        RoomCategory? selectedCategory = _categories.FirstOrDefault(c => c.Name == categoryName);
        if (selectedCategory == null)
        {
            throw new ArgumentException("Category not found");
        }

        if (selectedCategory.AvailableRooms <= 0)
        {
            throw new ArgumentException("No available rooms");
        }

        User? user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        int days = (endDate - startDate).Days;
        decimal currencyRate = GetCurrencyRate(currency);
        decimal totalCost = CalculateBookingCost(selectedCategory.BaseRate, days, currencyRate);

        Booking? booking = new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            StartDate = startDate,
            EndDate = endDate,
            RoomCategory = selectedCategory,
            Cost = totalCost,
            Currency = currency
        };

        _bookings.Add(booking);
        selectedCategory.AvailableRooms--;

        return booking;
    }

    public void CancelBooking(Guid bookingId)
    {
        Booking? booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking == null)
        {
            throw new ArgumentException($"Booking with id: '{bookingId}' does not exist");
        }

        if (booking.StartDate <= DateTime.Now)
        {
            throw new ArgumentException("Start date cannot be earlier than now date");
        }

        Console.WriteLine($"Refund of {booking.Cost} {booking.Currency}");
        _bookings.Remove(booking);
        RoomCategory? category = _categories.FirstOrDefault(c => c.Name == booking.RoomCategory.Name);
        category.AvailableRooms++;
    }

    private static decimal CalculateDiscount() //Нет необходимости передавать переменную userId в качестве параметра
    {
        return 0.1m;
    }

    public Booking? FindBookingById(Guid bookingId)
    {
        return _bookings.FirstOrDefault(b => b.Id == bookingId);
    }

    public IEnumerable<Booking> SearchBookings(DateTime startDate, DateTime endDate, string categoryName)
    {
        IQueryable<Booking> query = _bookings.AsQueryable();

        query = query.Where(b => b.StartDate >= startDate);

        query = query.Where(b => b.EndDate <= endDate); //Конечная дата включена в результат фильтрации 

        if (!string.IsNullOrEmpty(categoryName))
        {
            query = query.Where(b => b.RoomCategory.Name == categoryName);
        }

        return query.ToList();
    }

    public decimal CalculateCancellationPenaltyAmount(Booking booking)
    {
        if (booking.StartDate <= DateTime.Now)
        {
            throw new ArgumentException("Start date cannot be earlier than now date");
        }

        int daysBeforeArrival = Math.Max((booking.StartDate - DateTime.Now).Days, 1); //Исправлен расчет величины штрафа

        return 5000.0m / daysBeforeArrival;
    }

    private static decimal GetCurrencyRate(Currency currency)
    {
        decimal currencyRate = 1m;
        currencyRate *= currency switch
        {
            Currency.Usd => (decimal)(new Random().NextDouble() * 100) + 1,
            Currency.Cny => (decimal)(new Random().NextDouble() * 12) + 1,
            Currency.Rub => 1m,
            _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
        };

        return currencyRate;
    }

    //Поправлен расчет стоимости при использовании валюты
    private static decimal CalculateBookingCost(decimal baseRate, int days, decimal currencyRate) //Удален параметр userId, так как в нем нет необходимости
    {
        decimal cost = baseRate * days;
        decimal discountedCost = cost - cost * CalculateDiscount();
        decimal totalCost = discountedCost / currencyRate;
        return totalCost;
    }
}
