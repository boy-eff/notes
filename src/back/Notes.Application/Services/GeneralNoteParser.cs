using Notes.Application.Services.Interfaces;
using Notes.Domain.Entities;

namespace Notes.Application.Services;

/// <summary>
/// Парсер базовых заметок.
/// </summary>
public class GeneralNoteParser : INoteParser
{
    /// <inheritdoc />
    public NoteBase Parse(string title, string content)
    {
        return new GeneralNote(default, title, content);
    }
}