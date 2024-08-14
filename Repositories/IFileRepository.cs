using ConsoleAppCRUD.Models;

namespace ConsoleAppCRUD.Repositories;

public interface IFileRepository
{
    void Add(FileEntity file); // Agregar un archivo a la base de datos
    FileEntity? Get(int id); // Obtener un archivo por ID, FileEntity? significa que puede devolver null
    IEnumerable<FileEntity> GetAll(); // Obtener todos los archivos
    void Update(FileEntity file); // Actualizar un archivo
    void Delete(int id); // Eliminar un archivo por ID
    void Save(); // Guardar cambios en la base de datos
}