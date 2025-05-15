using Notes.Application.Common.Mapping;
using Notes.Application.Features.Notes.Dto;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.MappingStrategies;

public class NoteMappingStrategies
{
    public class ToDto : IMappingStrategy<Note, NoteDto>
    {
        public NoteDto Map(Note source, NoteDto? target = default)
        {
            if (target is not null)
            {
                throw new ArgumentException("DTO является имутабельным. Невозможно замапить на текущую сущность");
            }
        
            return new NoteDto(source.Id.ToString(), source.Content, source.Tags, source.Attachments);
        }
    }

    public class CreateDtoToEntity : IMappingStrategy<CreateNoteDto, Note>
    {
        public Note Map(CreateNoteDto source, Note? target = null)
        {
            if (target is not null)
            {
                target.Content = source.Content;
                target.Tags = source.Tags;
            }
        
            return new Note(default, source.Content, source.Tags, []);
        }
    }

    public class UpdateDtoToEntity : IMappingStrategy<UpdateNoteDto, Note>
    {
        public Note Map(UpdateNoteDto source, Note? target = null)
        {
            if (target is not null)
            {
                target.Content = source.Content;
                target.Tags = source.Tags;
            }
        
            return new Note(default, source.Content, source.Tags, []);
        }
    }
}