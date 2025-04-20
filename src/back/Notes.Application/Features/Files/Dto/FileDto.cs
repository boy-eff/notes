namespace Notes.Application.Features.Files.Dto;

/// <summary>
/// DTO для работы с файлом.
/// </summary>
public record FileDto
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; init; } = null!;

    /// <summary>
    /// MIME-тип файла.
    /// </summary>
    public string ContentType { get; init; } = null!;

    /// <summary>
    /// Поток данных файла.
    /// </summary>
    public Stream Data { get; init; } = null!;
} 