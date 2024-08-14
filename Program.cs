using Microsoft.Extensions.DependencyInjection;
using ConsoleAppCRUD.Data;
using ConsoleAppCRUD.Repositories;
using ConsoleAppCRUD.Services;

namespace ConsoleAppCRUD
{
    internal static class Program
    {
        internal static void Main()
        {
            // Configuración de servicios
            var serviceProvider = new ServiceCollection() // Inyección de dependencias
                .AddDbContext<AppDbContext>() // Agregar el contexto de la base de datos
                .AddScoped<IFileRepository, FileRepository>() // Agregar el repositorio de archivos
                .AddScoped<IFileService, FileService>() // Agregar el servicio de archivos
                .BuildServiceProvider(); // Construir el proveedor de servicios
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>(); // Obtener el contexto de la base de datos
            dbContext.Initialize();
            var fileService = serviceProvider.GetRequiredService<IFileService>(); // Obtener el servicio de archivos
            
            // Inserción de un archivo
            string filePathToUpload = "C:\\Users\\ASUS\\OneDrive\\Documentos\\ROADMAP C# .NET.docx";
            fileService.UploadFile(filePathToUpload);
            Console.WriteLine("Archivo subido exitosamente.");
            
            // Listado de archivos
            var files = fileService.ListFiles();
            foreach (var file in files)
            {
                Console.WriteLine($"ID: {file.Id}, Nombre: {file.FileName}");
            }

            // Lectura de un archivo (Descarga)
            int fileIdToDownload = 1; // ID del archivo a descargar
            string outputPath = "C:\\Users\\ASUS\\OneDrive\\Documentos\\ROADMAP C# .NET2.docx";
            fileService.DownloadFile(fileIdToDownload, outputPath);
            Console.WriteLine("Archivo descargado exitosamente.");

            // Edición de un archivo
            int fileIdToUpdate = 1; // ID del archivo a actualizar
            string newFilePath = "C:\\Users\\ASUS\\OneDrive\\Documentos\\ROADMAP C# .NET2.docx";
            fileService.UpdateFile(fileIdToUpdate, newFilePath);
            Console.WriteLine("Archivo actualizado exitosamente.");

            // Eliminación de un archivo
            int fileIdToDelete = 1; // ID del archivo a eliminar
            fileService.DeleteFile(fileIdToDelete);
            Console.WriteLine("Archivo eliminado exitosamente.");
        }
    }
}
