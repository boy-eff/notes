using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notes.Domain.Entities;

/// <summary>
/// Сущность записки.
/// </summary>
public class Note(ObjectId id, string content, ICollection<string> tags, ICollection<string> attachments)
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } = id;

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