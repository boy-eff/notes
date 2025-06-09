using MongoDB.Bson;
using Notes.Application.Common.CQRS;

namespace Notes.Application.Features.Notes.Queries.GetNotes;

/// <summary>
/// 
/// </summary>
/// <param name="NoteType"></param>
/// <param name="PageSize"></param>
/// <param name="PageNumber"></param>
/// <param name="LastId"></param>
public record GetAllNotesQueryParameters(string? NoteType, int PageSize, int? PageNumber, ObjectId? LastId) 
    : WithPaginationParameters(PageSize, PageNumber, LastId)
{
}