using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Notes.Domain.Constants;

namespace Notes.Domain.Entities;

/// <summary>
/// Заметка о фильме.
/// </summary>
[BsonDiscriminator(NoteTypeNames.Movie)]
public class MovieNote : NoteBase
{
    /// <summary>
    /// Заметка о фильме.
    /// </summary>
    /// <param name="id">ID заметки.</param>
    /// <param name="synopsis">Краткое описание фильма.</param>
    /// <param name="opinion">Мнение о фильме.</param>
    /// <param name="info">Дополнительная информация о фильме.</param>
    /// <param name="attachments">Список ID прикрепленных файлов.</param>
    public MovieNote(ObjectId id, string title, string synopsis, string opinion, string info, ICollection<string>? attachments = null) : base(id, title, attachments)
    {
        Synopsis = synopsis;
        Opinion = opinion;
        Info = info;
        Type = NoteTypeNames.Movie;
    }

    /// <summary>
    /// Краткое описание фильма.
    /// </summary>
    public string Synopsis { get; set; }

    /// <summary>
    /// Мнение о фильме.
    /// </summary>
    public string Opinion { get; set; }

    /// <summary>
    /// Дополнительная информация о фильме.
    /// </summary>
    public string Info { get; set; }
}