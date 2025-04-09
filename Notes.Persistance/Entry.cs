using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Common.Interfaces;
using Notes.Persistance.Configuration;
using Notes.Persistance.Repositories;

namespace Notes.Persistance;

/// <summary>
/// Точка входа для конфигурации Persistence слоя.
/// </summary>
public static class Entry
{
    /// <summary>
    /// Добавляет сервисы Persistence слоя.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
        
        return services;
    }
} 