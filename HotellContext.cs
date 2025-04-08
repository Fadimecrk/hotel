using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelApp.Models;
using Microsoft.EntityFrameworkCore;

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
