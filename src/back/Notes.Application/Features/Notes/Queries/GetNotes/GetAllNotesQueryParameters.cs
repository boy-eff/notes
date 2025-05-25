using Notes.Domain.Constants;

namespace Notes.Application.Features.Notes.Queries.GetNotes;

public class GetAllNotesQueryParameters
{
    /// <summary>
    /// Тип записки.
    /// </summary>
    public string? NoteType { get; set; }
}