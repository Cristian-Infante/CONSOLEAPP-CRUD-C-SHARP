using ConsoleAppCRUD.Data;
using ConsoleAppCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppCRUD.Repositories;

public class FileRepository(AppDbContext context) : IFileRepository
{
    private readonly AppDbContext _context = context; // Inyectar el contexto de la base de datos
    
    public void Add(FileEntity file)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.Files.Add(file);
            Save(); // Guardar cambios en la base de datos
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine($"Error al agregar el archivo: {ex.Message}");
            throw;
        }
    }

    public FileEntity? Get(int id)
    {
        try
        {
            return _context.Files.Find(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener el archivo con ID {id}: {ex.Message}");
            return null;
        }
    }

    public IEnumerable<FileEntity> GetAll()
    {
        try
        {
            return _context.Files.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener todos los archivos: {ex.Message}");
            return new List<FileEntity>();
        }
    }

    public void Update(FileEntity file)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.Files.Update(file);
            Save(); // Guardar cambios en la base de datos
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine($"Error al actualizar el archivo con ID {file.Id}: {ex.Message}");
            throw;
        }
    }

    public void Delete(int id)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var file = _context.Files.Find(id);
            if (file != null)
            {
                _context.Files.Remove(file);
                Save(); // Guardar cambios en la base de datos
                transaction.Commit();
            }
            else
            {
                Console.WriteLine($"No se encontró el archivo con ID {id} para eliminar.");
            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine($"Error al eliminar el archivo con ID {id}: {ex.Message}");
            throw;
        }
    }

    public void Save()
    {
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Error al guardar los cambios en la base de datos: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado al guardar los cambios: {ex.Message}");
            throw;
        }
    }
}