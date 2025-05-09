using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelApp.Models;
using Microsoft.EntityFrameworkCore;


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Kund>().HasData(
        new Kund { Id = 1, Namn = "Anna Karlsson", Epost = "anna@example.com" },
        new Kund { Id = 2, Namn = "Olle Olsson", Epost = "olle@example.com" }
    );

    modelBuilder.Entity<Rum>().HasData(
        new Rum { Id = 1, Rumsnummer = "101", Typ = "Enkel", PrisPerNatt = 500 },
        new Rum { Id = 2, Rumsnummer = "102", Typ = "Dubbel", PrisPerNatt = 800 }
    );
}


namespace HotelApp.Data;

public class HotellContext : DbContext
{
    public DbSet<Kund> Kunder { get; set; }
    public DbSet<Rum> Rum { get; set; }
    public DbSet<Bokning> Bokningar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=HotellDatabas;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
