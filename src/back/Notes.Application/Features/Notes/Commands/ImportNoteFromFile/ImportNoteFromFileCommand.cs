using System.Text;
using MediatR;
using MongoDB.Bson;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Files.Dto;
using Notes.Application.Services;
using Notes.Application.Services.Interfaces;
using Notes.Domain.Constants;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Notes.Commands.ImportNoteFromFile;

/// <summary>
/// Команда для импорта записки из файла.
/// </summary>
public record ImportNoteFromFileCommand(FileDto File, string NoteTypeName, string NoteTitle) : IRequest
{
    /// <summary>
    /// Обработчик команды импорта записки из файла.
    /// </summary>
    /// <param name="repository">Репозиторий заметок.</param>
    /// <param name="parserFactory">Фабрика парсеров.</param>
    private class Handler(IRepository<NoteBase, ObjectId> repository, INoteParserFactory parserFactory) : IRequestHandler<ImportNoteFromFileCommand>
    {
        /// <inheritdoc />
        public async Task Handle(ImportNoteFromFileCommand request, CancellationToken cancellationToken)
        {
            using var reader = new StreamReader(request.File.Data);
            var content = await reader.ReadToEndAsync(cancellationToken);

            var noteType = NoteType.GetByName(request.NoteTypeName);
            var parser = parserFactory.GetParser(noteType.Name);
            var note = parser.Parse(request.NoteTitle, content);

            await repository.CreateAsync(note);
        }
    }
} 