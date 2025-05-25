using Notes.Application.Common.Interfaces;
using Notes.Domain.Constants;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Specifications;

public class NoteSpecification : IRepositorySpecification<NoteBase>
{
    /// <summary>
    /// Тип заметки.
    /// </summary>
    public NoteType? NoteType { get; set; }
    
    public IQueryable<NoteBase> BuildFilters(IQueryable<NoteBase> entities)
    {
        if (NoteType != null)
        {
            entities = entities.Where(x => x.Type == NoteType.Name);
        }
        
        return entities;
    }
}