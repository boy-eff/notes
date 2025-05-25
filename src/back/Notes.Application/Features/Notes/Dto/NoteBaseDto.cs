using System.Text.Json.Serialization;

namespace Notes.Application.Features.Notes.Dto;

/// <summary>
/// DTO записки.
/// </summary>
[JsonDerivedType(typeof(GeneralNoteDto))]
[JsonDerivedType(typeof(MovieNoteDto))]
public record NoteBaseDto(string Id, string Title, ICollection<string> Attachments, string Type);

/// <summary>
/// DTO записки к фильму.
/// </summary>
public record MovieNoteDto(string Id, string Title, ICollection<string> Attachments, string Synopsis, string Opinion, string Info, string Type) : NoteBaseDto(Id, Title, Attachments, Type);

/// <summary>
/// DTO общей записки.
/// </summary>
public record GeneralNoteDto(string Id, string Title, ICollection<string> Attachments, string Content, string Type) : NoteBaseDto(Id, Title, Attachments, Type);

/// <summary>
/// DTO для создания записки.
/// </summary>
public record CreateNoteDto(string Content, ICollection<string> Tags);

/// <summary>
/// DTO для обновления записки.
/// </summary>
public record UpdateNoteDto(string Content, ICollection<string> Tags);