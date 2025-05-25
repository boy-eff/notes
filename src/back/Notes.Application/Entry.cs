using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Common.CQRS.Commands.Create;
using Notes.Application.Common.Interfaces;
using Notes.Application.Common.Mapping;
using Notes.Application.Features.Notes.Commands;
using Notes.Application.Features.Notes.Dto;
using Notes.Application.Features.Notes.MappingStrategies;
using Notes.Application.Features.Notes.Queries;
using Notes.Application.Services;
using Notes.Application.Services.Interfaces;
using Notes.Domain.Constants;
using Notes.Domain.Entities;

namespace Notes.Application;

/// <summary>
/// Точка входа для конфигурации Application слоя.
/// </summary>
public static class Entry
{
    /// <summary>
    /// Добавляет сервисы Application слоя в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Коллекция сервисов с добавленными сервисами Application слоя.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Регистрация MediatR
        services.AddMediatR(cfg 
            => cfg.RegisterServicesFromAssemblies(typeof(CreateCommand<,>).Assembly));

        // Регистрация валидаторов
        services.AddValidatorsFromAssembly(typeof(Entry).Assembly);

        services.AddScoped<INoteParserFactory, NoteParserFactory>();
        
        services.AddKeyedScoped<INoteParser, MovieNoteParser>(NoteType.Movie.Name);
        services.AddKeyedScoped<INoteParser, GeneralNoteParser>(NoteType.General.Name);

        // Регистрация маппинга
        var mapper = new MapperService();
        mapper.RegisterStrategiesFromAssembly(typeof(NoteMappingStrategies).Assembly);
        services.AddSingleton<IMapperService>(mapper);
        
        
        return services;
    }
} 