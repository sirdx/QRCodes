using Microsoft.EntityFrameworkCore;
using QRCodes.Models;

namespace QRCodes.Data;

public class DatabaseContext : DbContext
{
    public DbSet<QrCode> QrCodes { get; set; }

    public DatabaseContext()
    {
        SQLitePCL.Batteries_V2.Init();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "qrcodes.db3");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
}
