namespace Notes.Domain.Entities;

/// <summary>
/// Сущность записки.
/// </summary>
public class Note(int id, string content, ICollection<string> tags, ICollection<string> attachments)
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; } = id;

    /// <summary>
    /// Содержание
    /// </summary>
    public string Content { get; set; } = content;

    /// <summary>
    /// Тэги записки
    /// </summary>
    public ICollection<string> Tags { get; set; } = tags;

    /// <summary>
    /// Список файлов, прикрепленных к записке.
    /// </summary>
    public ICollection<string> Attachments { get; set; } = attachments;
}