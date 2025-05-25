using Notes.Domain.Constants;

namespace Notes.Application.Services.Interfaces;

/// <summary>
/// Фабрика для создания парсеров заметок.
/// </summary>
public interface INoteParserFactory
{
    /// <summary>
    /// Получает парсер для указанного типа заметки.
    /// </summary>
    /// <param name="noteType">Тип заметки.</param>
    /// <returns>Парсер заметок.</returns>
    INoteParser GetParser(string noteType);
}