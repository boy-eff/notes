namespace Notes.Persistance.Configuration;

/// <summary>
/// Конфигурация подключения к MongoDB.
/// </summary>
public class MongoDbConfiguration
{
    /// <summary>
    /// Название секции в конфигурации.
    /// </summary>
    public const string SectionName = "Mongo";
    
    /// <summary>
    /// Строка подключения к MongoDB.
    /// </summary>
    public string ConnectionString { get; set; } = null!;

    /// <summary>
    /// Название базы данных.
    /// </summary>
    public string DatabaseName { get; set; } = null!;
} 