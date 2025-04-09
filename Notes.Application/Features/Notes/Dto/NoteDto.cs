namespace Notes.Application.Features.Notes.Dto;

/// <summary>
/// DTO записки.
/// </summary>
public record NoteDto(int Id, string Content, ICollection<string> Tags);