public class Room
{
    public int Id { get; set; }
    public string Type { get; set; } // "Single" or "Double"
    public int ExtraBeds { get; set; } // 0, 1, or 2
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; } = true;
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}

public class Booking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Room Room { get; set; }
    public Customer Customer { get; set; }
}

public enum RoomType
{
    Single,
    Double
}



public class HotelDbContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("YourConnectionStringHere");
    }
}

using Microsoft.EntityFrameworkCore;

public class HotelBookingContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=HotelBookingDb;Trusted_Connection=True;");
    }
}


public class HotelDbContext : DbContext
{
    // ...

    public static void Initialize(HotelDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Rooms.Any())
        {
            var rooms = new[]
            {
                new Room { Type = "Single", ExtraBeds = 0, PricePerNight = 500 },
                new Room { Type = "Double", ExtraBeds = 1, PricePerNight = 800 },
                new Room { Type = "Double", ExtraBeds = 2, PricePerNight = 1000 },
                new Room { Type = "Single", ExtraBeds = 0, PricePerNight = 500 }
            };
            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {
            var customers = new[]
            {
                new Customer { Name = "Alice", Phone = "123456789", Email = "alice@example.com" },
                new Customer { Name = "Bob", Phone = "987654321", Email = "bob@example.com" },
                new Customer { Name = "Charlie", Phone = "555555555", Email = "charlie@example.com" },
                new Customer { Name = "Diana", Phone = "444444444", Email = "diana@example.com" }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        using (var context = new HotelDbContext())
        {
            HotelDbContext.Initialize(context);
            
            // Meny för att registrera rum, kunder och bokningar.
            // Exempel på logik för bokning
        }
    }
}




public static void CreateBooking(HotelDbContext context, int roomId, int customerId, DateTime startDate, DateTime endDate)
{
    if (startDate < DateTime.Today || endDate < DateTime.Today || startDate >= endDate)
    {
        Console.WriteLine("Ogiltiga datum.");
        return;
    }

    var room = context.Rooms.Find(roomId);
    if (room == null || !room.IsAvailable)
    {
        Console.WriteLine("Rummet är inte tillgängligt.");
        return;
    }

    var existingBooking = context.Bookings
        .Any(b => b.RoomId == roomId && 
                   ((startDate >= b.StartDate && startDate <= b.EndDate) || 
                    (endDate >= b.StartDate && endDate <= b.EndDate)));

    if (existingBooking)
    {
        Console.WriteLine("Rummet är redan bokat för dessa datum.");
        return;
    }

    var booking = new Booking
    {
        RoomId = roomId,
        CustomerId = customerId,
        StartDate = startDate,
        EndDate = endDate
    };

    context.Bookings.Add(booking);
    room.IsAvailable = false; // Markera rummet som upptaget
    context.SaveChanges();
    Console.WriteLine("Bokning skapad.");



    {
    "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
    }
}
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new HotelBookingContext())
        {
            if (!context.Rooms.Any())
            {
                context.Rooms.AddRange(
                    new Room { RoomNumber = "101", Type = RoomType.Single, IsAvailable = true, MaxBeds = 1, Extras = 0 },
                    new Room { RoomNumber = "102", Type = RoomType.Double, IsAvailable = true, MaxBeds = 2, Extras = 1 },
                    new Room { RoomNumber = "103", Type = RoomType.Double, IsAvailable = true, MaxBeds = 2, Extras = 2 },
                    new Room { RoomNumber = "104", Type = RoomType.Single, IsAvailable = true, MaxBeds = 1, Extras = 0 }
                );
                context.SaveChanges();
            }

            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                    new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" },
                    new Customer { FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com" },
                    new Customer { FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com" }
                );
                context.SaveChanges();
            }
        }
    }
}
public static void MakeBooking(int roomId, int customerId, DateTime startDate, DateTime endDate)
{
    using (var context = new HotelBookingContext())
    {
        var existingBooking = context.Bookings
            .FirstOrDefault(b => b.RoomId == roomId && b.StartDate < endDate && b.EndDate > startDate);

        if (existingBooking != null)
        {
            Console.WriteLine("This room is already booked for the selected dates.");
            return;
        }

        var booking = new Booking
        {
            RoomId = roomId,
            CustomerId = customerId,
            StartDate = startDate,
            EndDate = endDate
        };

        context.Bookings.Add(booking);
        context.SaveChanges();

        Console.WriteLine($"Room {roomId} has been booked for {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.");
    }
}
if (startDate < DateTime.Today || endDate < DateTime.Today)
{
    Console.WriteLine("You cannot book a room for past dates.");
    return;
}
class Program
{
    static void Main(string[] args)
    {
        using (var context = new HotelBookingContext())
        {
            SeedData.Initialize(context);

            Console.WriteLine("Welcome to the Hotel Booking System!");
            Console.WriteLine("1. Make a Booking");
            Console.WriteLine("2. Exit");

            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter Room ID: ");
                int roomId = int.Parse(Console.ReadLine());

                Console.Write("Enter Customer ID: ");
                int customerId = int.Parse(Console.ReadLine());

                Console.Write("Enter Start Date (YYYY-MM-DD): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter End Date (YYYY-MM-DD): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                MakeBooking(roomId, customerId, startDate, endDate);
            }
        }
    }
}
