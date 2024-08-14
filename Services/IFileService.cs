using ConsoleAppCRUD.Models;

namespace ConsoleAppCRUD.Services;

public interface IFileService
{
    void UploadFile(string filePath); // Subir un archivo a la base de datos
    FileEntity DownloadFile(int id, string outputPath); // Descargar un archivo por ID, FileEntity? significa que puede devolver null
    IEnumerable<FileEntity> ListFiles(); // Listar todos los archivos
    void UpdateFile(int id, string newFilePath); // Actualizar un archivo
    void DeleteFile(int id); // Eliminar un archivo por ID
}