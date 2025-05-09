using HotelApp.Data;
using HotelApp.Models;
using Microsoft.EntityFrameworkCore;
using var context = new HotellContext();
context.Database.Migrate(); 


using var context = new HotellContext();

bool kör = true;

while (kör)
{
    Console.Clear();
    Console.WriteLine("=== Hotellhantering ===");
    Console.WriteLine("1. Hantera rum");
    Console.WriteLine("2. Hantera kunder");
    Console.WriteLine("3. Hantera bokningar");
    Console.WriteLine("0. Avsluta");
    Console.Write("Ditt val: ");
    var val = Console.ReadLine();
    static void HanteraRum(HotellContext context)
    {
        bool kör = true;
        while (kör)
        {
            Console.Clear();
            Console.WriteLine("=== Rumsmeny ===");
            Console.WriteLine("1. Lista alla rum");
            Console.WriteLine("2. Lägg till nytt rum");
            Console.WriteLine("3. Uppdatera rum");
            Console.WriteLine("4. Ta bort rum");
            Console.WriteLine("0. Tillbaka till huvudmenyn");
            Console.Write("Ditt val: ");
            var val = Console.ReadLine();

            switch (val)
            {
                case "1":
                    ListaRum(context);
                    break;
                case "2":
                    LäggTillRum(context);
                    break;
                case "3":
                    UppdateraRum(context);
                    break;
                case "4":
                    TaBortRum(context);
                    break;
                case "0":
                    kör = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    break;
            }
        }
    }


  
        static void HanteraKunder(HotellContext context)
        {
            bool kör = true;
            while (kör)
            {
                Console.Clear();
                Console.WriteLine("=== Kundmeny ===");
                Console.WriteLine("1. Lista alla kunder");
                Console.WriteLine("2. Lägg till ny kund");
                Console.WriteLine("3. Uppdatera kund");
                Console.WriteLine("4. Ta bort kund");
                Console.WriteLine("0. Tillbaka till huvudmenyn");
                Console.Write("Ditt val: ");
                var val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        ListaKunder(context);
                        break;
                    case "2":
                        LäggTillKund(context);
                        break;
                    case "3":
                        UppdateraKund(context);
                        break;
                    case "4":
                        TaBortKund(context);
                        break;
                    case "0":
                        kör = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val!");
                        Console.ReadKey();
                        break;
                }
            }
        }

    

    static void HanteraBokningar(HotellContext context)
    {
        bool kör = true;
        while (kör)
        {
            Console.Clear();
            Console.WriteLine("=== Bokningsmeny ===");
            Console.WriteLine("1. Lista alla bokningar");
            Console.WriteLine("2. Lägg till ny bokning");
            Console.WriteLine("3. Uppdatera bokning");
            Console.WriteLine("4. Ta bort bokning");
            Console.WriteLine("0. Tillbaka till huvudmenyn");
            Console.Write("Ditt val: ");
            var val = Console.ReadLine();

            switch (val)
            {
                case "1":
                    ListaBokningar(context);
                    break;
                case "2":
                    LäggTillBokning(context);
                    break;
                case "3":
                    UppdateraBokning(context);
                    break;
                case "4":
                    TaBortBokning(context);
                    break;
                case "0":
                    kör = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    break;
            }
        }
    }


    switch (val)
    {
        case "1":
            HanteraRum(context);
            break;
        case "2":
            HanteraKunder(context);
            break;
        case "3":
            HanteraBokningar(context);
            break;
        case "0":
            kör = false;
            break;
        default:
            Console.WriteLine("Ogiltigt val, tryck på valfri knapp för att fortsätta.");
            Console.ReadKey();
            break;
    }
}

static void ListaRum(HotellContext context)
{
    Console.Clear();
    var rumLista = context.Rum.ToList();

    Console.WriteLine("=== Lista över rum ===");
    foreach (var rum in rumLista)
    {
        Console.WriteLine($"ID: {rum.Id}, Nummer: {rum.Rumsnummer}, Typ: {rum.Typ}, Pris per natt: {rum.PrisPerNatt} kr");
    }

    Console.WriteLine("\nTryck valfri tangent för att gå tillbaka...");
    Console.ReadKey();
}
static void LäggTillRum(HotellContext context)
{
    Console.Clear();
    Console.WriteLine("=== Lägg till nytt rum ===");

    Console.Write("Rumsnummer: ");
    string nummer = Console.ReadLine();

    Console.Write("Typ (t.ex. Enkel, Dubbel, Svit): ");
    string typ = Console.ReadLine();

    Console.Write("Pris per natt: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal pris))
    {
        Console.WriteLine("Felaktigt pris. Tryck för att återgå.");
        Console.ReadKey();
        return;
    }

    var nyttRum = new Rum
    {
        Rumsnummer = nummer,
        Typ = typ,
        PrisPerNatt = pris
    };

    context.Rum.Add(nyttRum);
    context.SaveChanges();

    Console.WriteLine("Rummet har lagts till!");
    Console.ReadKey();
}
static void UppdateraRum(HotellContext context)
{
    Console.Clear();
    Console.WriteLine("=== Uppdatera rum ===");

    ListaRum(context);
    Console.Write("\nAnge ID på rummet som ska uppdateras: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Felaktigt ID.");
        Console.ReadKey();
        return;
    }

    var rum = context.Rum.Find(id);
    if (rum == null)
    {
        Console.WriteLine("Rummet hittades inte.");
        Console.ReadKey();
        return;
    }

    Console.Write($"Nytt rumsnummer ({rum.Rumsnummer}): ");
    string nummer = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(nummer)) rum.Rumsnummer = nummer;

    Console.Write($"Ny typ ({rum.Typ}): ");
    string typ = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(typ)) rum.Typ = typ;

    Console.Write($"Nytt pris ({rum.PrisPerNatt}): ");
    string prisStr = Console.ReadLine();
    if (decimal.TryParse(prisStr, out decimal pris)) rum.PrisPerNatt = pris;

    context.SaveChanges();
    Console.WriteLine("Rummet har uppdaterats!");
    Console.ReadKey();
}
static void TaBortRum(HotellContext context)
{
    Console.Clear();
    Console.WriteLine("=== Ta bort rum ===");

    ListaRum(context);
    Console.Write("\nAnge ID på rummet som ska tas bort: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Felaktigt ID.");
        Console.ReadKey();
        return;
    }

    var rum = context.Rum.Find(id);
    if (rum == null)
    {
        Console.WriteLine("Rummet hittades inte.");
        Console.ReadKey();
        return;
    }

    context.Rum.Remove(rum);
    context.SaveChanges();
    Console.WriteLine("Rummet har tagits bort.");
    Console.ReadKey();
}

static void ListaKunder(HotellContext context)
{
    Console.Clear();
    var kunder = context.Kunder.ToList();

    Console.WriteLine("=== Lista över kunder ===");
    foreach (var kund in kunder)
    {
        Console.WriteLine($"ID: {kund.Id}, Namn: {kund.Namn}, E-post: {kund.Epost}");
    }

    Console.WriteLine("\nTryck valfri tangent för att gå tillbaka...");
    Console.ReadKey();
}
static void LäggTillKund(HotellContext context)
{
    Console.Clear();
    Console.WriteLine("=== Lägg till ny kund ===");

    Console.Write("Namn: ");
    string namn = Console.ReadLine();

    Console.Write("E-post: ");
    string epost = Console.ReadLine();

    var nyKund = new Kund
    {
        Namn = namn,
        Epost = epost
    };

    context.Kunder.Add(nyKund);
    context.SaveChanges();

    Console.WriteLine("Kunden har lagts till!");
    Console.ReadKey();
}
static void UppdateraKund(HotellContext context)
{
    Console.Clear();
    Console.WriteLine("=== Uppdatera kund ===");

    ListaKunder(context);
    Console.Write("\nAnge ID på kunden som ska uppdateras: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Felaktigt ID.");
        Console.ReadKey();
        return;
    }

    var kund = context.Kunder.Find(id);
    if (kund == null)
    {
        Console.WriteLine("Kunden hittades inte.");
        Console.ReadKey();
        return;
    }

    Console.Write($"Nytt namn ({kund.Namn}): ");
    string namn = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(namn)) kund.Namn = namn;

    Console.Write($"Ny e-post ({kund.Epost}): ");
    string epost = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(epost)) kund.Epost = epost;

    context.SaveChanges();
    Console.WriteLine("Kunden har uppdaterats!");
    Console.ReadKey();
}
static void TaBortKund(HotellContext context)
{
    Console.Clear();
    Console.WriteLine("=== Ta bort kund ===");

    ListaKunder(context);
    Console.Write("\nAnge ID på kunden som ska tas bort: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Felaktigt ID.");
        Console.ReadKey();
        return;
    }

    var kund = context.Kunder.Find(id);
    if (kund == null)
    {
        Console.WriteLine("Kunden hittades inte.");
        Console.ReadKey();
        return;
    }

    context.Kunder.Remove(kund);
    context.SaveChanges();
    Console.WriteLine("Kunden har tagits bort.");
    Console.ReadKey();
}
static void ListaBokningar(HotellContext context)
{
    Console.Clear();
    var bokningar = context.Bokningar
        .Include(b => b.Kund)
        .Include(b => b.Rum)
        .ToList();

    Console.WriteLine("=== Lista över bokningar ===");
    foreach (var b in bokningar)
    {
        Console.WriteLine($"ID: {b.Id}, Kund: {b.Kund.Namn}, Rum: {b.Rum.Rumsnummer}, " +
            $"Från: {b.Incheckning:yyyy-MM-dd} Till: {b.Utcheckning:yyyy-MM-dd}");
    }

    Console.WriteLine("\nTryck valfri tangent för att gå tillbaka...");
    Console.ReadKey();
}
static void LäggTillBokning(HotellContext context)
{
    Console.Clear();
    Console.WriteLine("=== Lägg till ny bokning ===");

    ListaKunder(context);
    Console.Write("Ange kund-ID: ");
    if (!int.TryParse(Console.ReadLine(), out int kundId) || !context.Kunder.Any(k => k.Id == kundId))
    {
        Console.WriteLine("Ogiltigt kund-ID.");
        Console.ReadKey();
        return;
    }

    ListaRum(context);
    Console.Write("Ange rum-ID: ");
    if (!int.TryParse(Console.ReadLine(), out int rumId) || !context.Rum.Any(r => r.Id == rumId))
    {
        Console.WriteLine("Ogiltigt rum-ID.");
        Console.ReadKey();
        return;
    }

    Console.Write("Incheckningsdatum (ÅÅÅÅ-MM-DD): ");
if (!DateTime.TryParse(Console.ReadLine(), out DateTime incheckning) || incheckning < DateTime.Now.Date)
{
    Console.WriteLine("Incheckningsdatum kan inte vara i det förflutna.");
    Console.ReadKey();
    return;
}

    }

    Console.Write("Utcheckningsdatum (ÅÅÅÅ-MM-DD): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime utcheckning))
    {
        Console.WriteLine("Ogiltigt datum.");
        Console.ReadKey();
        return;
    }

    var bokning = new Bokning
    {
        KundId = kundId,
        RumId = rumId,
        Incheckning = incheckning,
        Utcheckning = utcheckning
    };

var överlappandeBokning = context.Bokningar
    .Any(b => b.RumId == rumId && b.Incheckning < utcheckning && b.Utcheckning > incheckning);

if (överlappandeBokning)
{
    Console.WriteLine("Rummet är redan bokat under denna period.");
    Console.ReadKey();
    return;
}


    context.Bokningar.Add(bokning);
    context.SaveChanges();

    Console.WriteLine("Bokningen har lagts till!");
    Console.ReadKey();
}
static void UppdateraBokning(HotellContext context)
{
    Console.Clear();
    Console.WriteLine("=== Uppdatera bokning ===");

    ListaBokningar(context);
    Console.Write("\nAnge ID på bokningen som ska uppdateras: ");
    if (!int.TryParse(Console.ReadLine(), out int id)) return;

    var bokning = context.Bokningar.Find(id);
    if (bokning == null)
    {
        Console.WriteLine("Bokningen hittades inte.");
        Console.ReadKey();
        return;
    }

    Console.Write("Ny incheckning (ÅÅÅÅ-MM-DD): ");
    if (DateTime.TryParse(Console.ReadLine(), out DateTime nyIn)) bokning.Incheckning = nyIn;

    Console.Write("Ny utcheckning (ÅÅÅÅ-MM-DD): ");
    if (DateTime.TryParse(Console.ReadLine(), out DateTime nyUt)) bokning.Utcheckning = nyUt;

    context.SaveChanges();
    Console.WriteLine("Bokningen har uppdaterats!");
    Console.ReadKey();
}
static void TaBortBokning(HotellContext context)
{
    Console.Clear();
    Console.WriteLine("=== Ta bort bokning ===");

    ListaBokningar(context);
    Console.Write("\nAnge ID på bokningen som ska tas bort: ");
    if (!int.TryParse(Console.ReadLine(), out int id)) return;

    var bokning = context.Bokningar.Find(id);
    if (bokning == null)
    {
        Console.WriteLine("Bokningen hittades inte.");
        Console.ReadKey();
        return;
    }

    context.Bokningar.Remove(bokning);
    context.SaveChanges();

    Console.WriteLine("Bokningen har tagits bort.");
    Console.ReadKey();
}
