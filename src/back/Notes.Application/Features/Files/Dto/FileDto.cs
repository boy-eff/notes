namespace Notes.Application.Features.Files.Dto;

/// <summary>
/// DTO файла.
/// </summary>
public record FileDto(string Id, string Name, string ContentType, string Url, long Size); 