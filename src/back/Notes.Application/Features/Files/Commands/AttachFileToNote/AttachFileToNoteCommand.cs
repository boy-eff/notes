using MediatR;
using MongoDB.Bson;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Files.Dto;
using Notes.Domain.Entities;

namespace Notes.Application.Features.Files.Commands.AttachFileToNote;

/// <summary>
/// Команда для прикрепления файла к заметке.
/// </summary>
public class AttachFileToNoteCommand : IRequest<string>
{
    /// <summary>
    /// ID заметки.
    /// </summary>
    public ObjectId NoteId { get; set; }
    
    /// <summary>
    /// Имя файла.
    /// </summary>
    public FileDto File { get; set; }
}

/// <summary>
/// Обработчик команды прикрепления файла к заметке.
/// </summary>
public class AttachFileToNoteCommandHandler(IFileStorageService fileStorageService, IRepository<NoteBase, ObjectId> noteRepository) : IRequestHandler<AttachFileToNoteCommand, string>
{
    
    /// <inheritdoc />
    public async Task<string> Handle(AttachFileToNoteCommand request, CancellationToken cancellationToken)
    {
        await using var stream = request.File.Data;
        // Загружаем файл в хранилище
        var fileName = await fileStorageService.UploadFileAsync(request.File.FileName, stream, request.File.ContentType);
        
        // Получаем заметку
        var note = await noteRepository.GetByIdAsync(request.NoteId);
        if (note == null)
        {
            throw new KeyNotFoundException($"Заметка с ID {request.NoteId} не найдена.");
        }
        
        // Добавляем файл в список вложений
        note.Attachments.Add(fileName);
        
        // Сохраняем изменения
        await noteRepository.UpdateAsync(note);
        
        return fileName;
    }
} 