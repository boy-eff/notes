namespace Notes.Infrastructure.Configuration;

/// <summary>
/// Настройки для интеграции с MinIO.
/// </summary>
public class MinioConfiguration
{
    /// <summary>
    /// Имя секции в конфигурации.
    /// </summary>
    public const string SectionName = "Minio";

    /// <summary>
    /// URL сервера MinIO.
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// Ключ доступа.
    /// </summary>
    public string AccessKey { get; set; } = string.Empty;

    /// <summary>
    /// Секретный ключ.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Имя бакета.
    /// </summary>
    public string BucketName { get; set; } = string.Empty;
} 