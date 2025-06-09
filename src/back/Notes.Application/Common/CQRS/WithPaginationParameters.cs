using MongoDB.Bson;

namespace Notes.Application.Common.CQRS;

/// <summary>
/// Параметры запроса для пагинации.
/// </summary>
/// <param name="PageSize">Размер страницы.</param>
/// <param name="PageNumber">Номер страницы.</param>
/// <param name="LastId">ID последнего элемента на предыдущей странице для курсорной пагинации.</param>
public record WithPaginationParameters(int PageSize, int? PageNumber, ObjectId? LastId);