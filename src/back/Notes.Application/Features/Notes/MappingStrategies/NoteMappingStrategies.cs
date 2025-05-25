using Notes.Application.Common.Mapping;
using Notes.Application.Features.Notes.Dto;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.MappingStrategies;

public class NoteMappingStrategies
{
    public class ToDto : IMappingStrategy<NoteBase, NoteBaseDto>
    {
        public NoteBaseDto Map(NoteBase source, NoteBaseDto? target = default)
        {
            if (target is not null)
            {
                throw new ArgumentException("DTO является имутабельным. Невозможно замапить на текущую сущность");
            }

            return source switch
            {
                GeneralNote generalNote => new GeneralNoteDto(generalNote.Id.ToString(), generalNote.Title, generalNote.Attachments,
                    generalNote.Content, generalNote.Type),
                MovieNote movieNote => new MovieNoteDto(movieNote.Id.ToString(), movieNote.Title, movieNote.Attachments,
                    movieNote.Synopsis, movieNote.Opinion, movieNote.Info, movieNote.Type),
                _ => new NoteBaseDto(source.Id.ToString(), source.Title, source.Attachments, source.Type)
            };
        }
    }
}