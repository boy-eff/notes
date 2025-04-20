using System;
using System.Collections.Generic;
using System.Reflection;
using Notes.Application.Common.Interfaces;

namespace Notes.Application.Common.Mapping;

/// <summary>
/// Сервис для маппинга объектов с использованием стратегий.
/// </summary>
public class MapperService : IMapperService
{
    private readonly Dictionary<(Type sourceType, Type targetType), object> _strategies;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="MapperService"/>.
    /// </summary>
    public MapperService()
    {
        _strategies = new Dictionary<(Type sourceType, Type targetType), object>();
    }

    /// <summary>
    /// Регистрирует все стратегии маппинга из указанной сборки.
    /// </summary>
    /// <param name="assembly">Сборка для поиска стратегий.</param>
    public void RegisterStrategiesFromAssembly(Assembly assembly)
    {
        var strategyTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && 
                          i.GetGenericTypeDefinition() == typeof(IMappingStrategy<,>)));

        foreach (var strategyType in strategyTypes)
        {
            var mappingInterface = strategyType.GetInterfaces()
                .First(i => i.IsGenericType && 
                           i.GetGenericTypeDefinition() == typeof(IMappingStrategy<,>));

            var genericArgs = mappingInterface.GetGenericArguments();
            var strategy = Activator.CreateInstance(strategyType);
            
            var method = typeof(MapperService)
                .GetMethod(nameof(RegisterStrategy))!
                .MakeGenericMethod(genericArgs[0], genericArgs[1]);
            
            method.Invoke(this, new[] { strategy });
        }
    }

    /// <summary>
    /// Регистрирует стратегию маппинга для указанных типов.
    /// </summary>
    /// <typeparam name="TSource">Тип исходного объекта.</typeparam>
    /// <typeparam name="TTarget">Тип целевого объекта.</typeparam>
    /// <param name="strategy">Стратегия маппинга.</param>
    public void RegisterStrategy<TSource, TTarget>(IMappingStrategy<TSource, TTarget> strategy)
    {
        var key = (typeof(TSource), typeof(TTarget));
        _strategies[key] = strategy;
    }

    /// <summary>
    /// Выполняет маппинг объекта с использованием зарегистрированной стратегии.
    /// </summary>
    /// <typeparam name="TSource">Тип исходного объекта.</typeparam>
    /// <typeparam name="TTarget">Тип целевого объекта.</typeparam>
    /// <param name="source">Исходный объект.</param>
    /// <returns>Результат маппинга.</returns>
    /// <exception cref="InvalidOperationException">Стратегия маппинга не найдена.</exception>
    public TTarget Map<TSource, TTarget>(TSource source, TTarget? target = default)
    {
        var key = (typeof(TSource), typeof(TTarget));
        
        if (!_strategies.TryGetValue(key, out var strategy))
        {
            throw new InvalidOperationException($"Стратегия маппинга для типов {typeof(TSource).Name} -> {typeof(TTarget).Name} не найдена.");
        }

        return ((IMappingStrategy<TSource, TTarget>)strategy).Map(source, target);
    }
}