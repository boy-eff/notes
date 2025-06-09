using Microsoft.AspNetCore.Http;

namespace Notes.WebApi.Models;

/// <summary>
/// Модель для импорта записки из файла.
/// </summary>
/// <param name="Title">Заголовок.</param>
/// <param name="File">Файл для импорта.</param>
public record ImportNoteFromFileDto(string Title, IFormFile File); 