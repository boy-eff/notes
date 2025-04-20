using Microsoft.AspNetCore.Http;
using Notes.Application.Features.Files.Dto;

namespace Notes.WebApi.Extensions;

/// <summary>
/// Расширения для работы с <see cref="IFormFile"/>.
/// </summary>
public static class FormFileExtensions
{
    /// <summary>
    /// Конвертирует <see cref="IFormFile"/> в <see cref="FileDto"/>.
    /// </summary>
    /// <param name="file">Файл для конвертации.</param>
    /// <returns>DTO с данными файла.</returns>
    public static FileDto ToFileDto(this IFormFile file)
    {
        return new FileDto
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            Data = file.OpenReadStream()
        };
    }
} 