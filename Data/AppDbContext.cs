using Microsoft.EntityFrameworkCore;
using ConsoleAppCRUD.Models;

namespace ConsoleAppCRUD.Data;

public class AppDbContext: DbContext
{
    public DbSet<FileEntity> Files { get; init; } // Tabla de archivos

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database.db"); // Establece la conexión con la base de datos
    }
    
    public void Initialize()
    {
        this.Database.EnsureDeleted(); // Este método eliminará la base de datos y las tablas si existen
        this.Database.EnsureCreated(); // Este método creará la base de datos y las tablas si no existen
    }
}