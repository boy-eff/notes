using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Services.Interfaces;

namespace Notes.Application.Services;

/// <summary>
/// Фабрика для создания парсеров заметок.
/// </summary>
public class NoteParserFactory : INoteParserFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NoteParserFactory"/>.
    /// </summary>
    /// <param name="serviceProvider">Провайдер сервисов.</param>
    public NoteParserFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public INoteParser GetParser(string noteType)
    {
        return _serviceProvider.GetRequiredKeyedService<INoteParser>(noteType) 
               ?? throw new ArgumentException($"Неизвестный тип парсера: {noteType}");
    }
}