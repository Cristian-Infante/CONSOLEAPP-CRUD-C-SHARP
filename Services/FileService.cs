using ConsoleAppCRUD.Models;
using ConsoleAppCRUD.Repositories;

namespace ConsoleAppCRUD.Services;

public class FileService(IFileRepository fileRepository) : IFileService
{
    private readonly IFileRepository _fileRepository = fileRepository;

    public void UploadFile(string filePath)
    {
        var fileData = File.ReadAllBytes(filePath);
        var fileEntity = new FileEntity
        {
            FileName = Path.GetFileName(filePath),
            Data = fileData
        };

        _fileRepository.Add(fileEntity);
        _fileRepository.Save();
    }

    public FileEntity DownloadFile(int id, string outputPath)
    {
        var file = _fileRepository.Get(id);
        if (file is null) throw new FileNotFoundException($"No se encontró el archivo con ID {id}.");
        if (file.Data is null) throw new InvalidOperationException("Los datos del archivo no están disponibles.");

        File.WriteAllBytes(outputPath, file.Data);
        return file;
    }

    public IEnumerable<FileEntity> ListFiles()
    {
        return _fileRepository.GetAll();
    }

    public void UpdateFile(int id, string newFilePath)
    {
        var file = _fileRepository.Get(id);
        if (file is null) return;

        file.FileName = Path.GetFileName(newFilePath);
        file.Data = File.ReadAllBytes(newFilePath);
        _fileRepository.Update(file);
        _fileRepository.Save();
    }

    public void DeleteFile(int id)
    {
        _fileRepository.Delete(id);
        _fileRepository.Save();
    }
}