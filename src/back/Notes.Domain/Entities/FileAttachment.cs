using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notes.Domain.Entities;

/// <summary>
/// Сущность для хранения информации о файле в MinIO.
/// </summary>
/// <param name="id">ID файла.</param>
/// <param name="fileName">Имя файла в MinIO.</param>
/// <param name="originalFileName">Оригинальное имя файла.</param>
/// <param name="contentType">MIME-тип файла.</param>
/// <param name="size">Размер файла в байтах.</param>
public class FileAttachment(ObjectId id, string fileName, string originalFileName, string contentType, long size)
{
    /// <summary>
    /// ID файла.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } = id;

    /// <summary>
    /// Имя файла в MinIO.
    /// </summary>
    public string FileName { get; set; } = fileName;

    /// <summary>
    /// Оригинальное имя файла.
    /// </summary>
    public string OriginalFileName { get; set; } = originalFileName;

    /// <summary>
    /// MIME-тип файла.
    /// </summary>
    public string ContentType { get; set; } = contentType;

    /// <summary>
    /// Размер файла в байтах.
    /// </summary>
    public long Size { get; set; } = size;
}