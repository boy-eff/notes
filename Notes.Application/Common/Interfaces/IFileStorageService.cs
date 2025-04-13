using System.IO;
using System.Threading.Tasks;

namespace Notes.Application.Common.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с файловым хранилищем.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Загружает файл в хранилище.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="content">Содержимое файла.</param>
    /// <param name="contentType">Тип содержимого.</param>
    /// <returns>URL загруженного файла.</returns>
    Task<string> UploadFileAsync(string fileName, Stream content, string contentType);
    
    /// <summary>
    /// Скачивает файл из хранилища.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Поток с содержимым файла.</returns>
    Task<Stream> DownloadFileAsync(string fileName);
    
    /// <summary>
    /// Удаляет файл из хранилища.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    Task DeleteFileAsync(string fileName);
    
    /// <summary>
    /// Проверяет существование файла в хранилище.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns><see langword="true"/>, если файл существует, иначе <see langword="false"/>.</returns>
    Task<bool> FileExistsAsync(string fileName);
} 