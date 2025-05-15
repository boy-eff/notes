using MongoDB.Bson;

namespace Notes.Application.Features.Notes.Dto;

/// <summary>
/// DTO записки.
/// </summary>
public record NoteDto(string Id, string Content, ICollection<string> Tags, ICollection<string> Attachments);

/// <summary>
/// DTO для создания записки.
/// </summary>
public record CreateNoteDto(string Content, ICollection<string> Tags);

/// <summary>
/// DTO для обновления записки.
/// </summary>
public record UpdateNoteDto(string Content, ICollection<string> Tags);