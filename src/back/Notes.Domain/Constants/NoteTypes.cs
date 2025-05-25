namespace Notes.Domain.Constants;

/// <summary>
/// Строковые дискриминаторы для заметок.
/// </summary>
public static class NoteTypeNames
{
    /// <summary>
    /// Дискриминатор для заметок о фильмах.
    /// </summary>
    public const string Movie = "movie";
    
    /// <summary>
    /// Дискриминатор для общих заметок.
    /// </summary>
    public const string General = "general";
}

/// <summary>
/// Тип заметки.
/// </summary>
public sealed record NoteType(int Id, string Name)
{
    /// <summary>
    /// Тип заметки о фильме.
    /// </summary>
    public static readonly NoteType Movie = new(1, NoteTypeNames.Movie);
    
    /// <summary>
    /// Тип заметки о фильме.
    /// </summary>
    public static readonly NoteType General = new(2, NoteTypeNames.General);
    
    /// <summary>
    /// Все возможные типы.
    /// </summary>
    public static readonly IReadOnlyList<NoteType> All = [Movie, General];

    /// <summary>
    /// Получает тип заметки по ID.
    /// </summary>
    /// <param name="id">ID типа заметки.</param>
    /// <returns>Тип заметки.</returns>
    /// <exception cref="ArgumentException">Если тип с указанным ID не найден.</exception>
    public static NoteType GetById(int id)
    {
        return All.FirstOrDefault(t => t.Id == id) 
            ?? throw new ArgumentException($"Тип заметки с ID {id} не найден.");
    }

    /// <summary>
    /// Получает тип заметки по имени.
    /// </summary>
    /// <param name="name">Название типа заметки.</param>
    /// <returns>Тип заметки.</returns>
    /// <exception cref="ArgumentException">Если тип с указанным ID не найден.</exception>
    public static NoteType GetByName(string name)
    {
        return All.FirstOrDefault(t => t.Name == name) 
               ?? throw new ArgumentException($"Тип заметки с именем {name} не найден.");
    }
}