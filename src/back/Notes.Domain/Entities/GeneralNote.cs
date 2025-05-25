using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Notes.Domain.Constants;

namespace Notes.Domain.Entities;

/// <summary>
/// Заметка о фильме.
/// </summary>
[BsonDiscriminator(NoteTypeNames.General)]
public class GeneralNote : NoteBase
{
    /// <summary>
    /// Заметка о фильме.
    /// </summary>
    /// <param name="id">ID заметки.</param>
    /// <param name="title">Заголовок.</param>
    /// <param name="content">Основной контент.</param>
    /// <param name="attachments">Список ID прикрепленных файлов.</param>
    public GeneralNote(ObjectId id, string title, string content, ICollection<string>? attachments = null) : base(id, title, attachments)
    {
        Content = content;
        Type = NoteTypeNames.General;
    }

    /// <summary>
    /// Основной контент.
    /// </summary>
    public string Content { get; set; }
}