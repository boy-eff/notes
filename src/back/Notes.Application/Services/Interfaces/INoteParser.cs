using Notes.Domain.Entities;

namespace Notes.Application.Services.Interfaces;

/// <summary>
/// Интерфейс парсера заметок.
/// </summary>
public interface INoteParser
{
    /// <summary>
    /// Парсит содержимое файла в заметку.
    /// </summary>
    /// <param name="title">Заголовок файла.</param>
    /// <param name="content">Содержимое файла.</param>
    /// <returns>Созданная заметка.</returns>
    NoteBase Parse(string title, string content);
} 