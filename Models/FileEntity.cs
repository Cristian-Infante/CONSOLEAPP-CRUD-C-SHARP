using System.ComponentModel.DataAnnotations;

namespace ConsoleAppCRUD.Models
{
    public class FileEntity
    {
        [Key]
        public int Id { get; init; } // ID del archivo
        [MaxLength(255)]
        public string FileName { get; set; } = string.Empty; // Nombre del archivo
        public byte[] Data { get; set; } = Array.Empty<byte>(); // Binarios del archivo
    }
}