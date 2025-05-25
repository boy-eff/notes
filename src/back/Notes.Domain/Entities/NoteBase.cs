using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Notes.Domain.Constants;

namespace Notes.Domain.Entities;

/// <summary>
/// Сущность записки.
/// </summary>
[BsonDiscriminator("type", RootClass = true)]
[BsonKnownTypes(typeof(MovieNote), typeof(GeneralNote))]
public abstract class NoteBase
{
    /// <summary>
    /// Создает новый экземпляр <see cref="NoteBase"/>.
    /// </summary>
    /// <param name="title">Заголовок записки.</param>
    /// <param name="id">ID записки.</param>
    /// <param name="attachments">Список ID прикрепленных файлов.</param>
    protected NoteBase(ObjectId id, string title, ICollection<string>? attachments = null)
    {
        Id = id;
        Title = title;
        Attachments = attachments ?? [];
    }

    /// <summary>
    /// ID записки.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    /// <summary>
    /// Заголовок записки.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Список файлов, прикрепленных к записке.
    /// </summary>
    public ICollection<string> Attachments { get; set; }

    /// <summary>
    /// Дискриминатор
    /// </summary>
    public string Type { get; set; }
}