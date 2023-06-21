using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using eiendomsverdi_atomreaktoren.Models;
using Microsoft.EntityFrameworkCore;

namespace eiendomsverdi_atomreaktoren.Db;


public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options)
       : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("ReactorDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PressureHistoryDataPoint>().Property(x => x.Id).ValueGeneratedOnAdd();
    }

    public DbSet<PressureHistoryDataPoint> PressureHistoryDataPoints { get; set; }
}
