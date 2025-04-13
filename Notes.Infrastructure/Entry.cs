using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Common.Interfaces;
using Notes.Infrastructure.Configuration;
using Notes.Infrastructure.Services;

namespace Notes.Infrastructure;

/// <summary>
/// Расширения для регистрации сервисов инфраструктурного слоя.
/// </summary>
public static class Entry
{
    /// <summary>
    /// Добавляет сервисы инфраструктурного слоя в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <returns>Коллекция сервисов.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioConfiguration>(configuration.GetSection(MinioConfiguration.SectionName));

        services.AddScoped<IFileStorageService, MinioFileStorageService>();

        return services;
    }
} 