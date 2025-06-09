using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Notes.Application.Features.Files.Commands.AttachFileToNote;
using Notes.Application.Features.Notes.Commands.DeleteNote;
using Notes.Application.Features.Notes.Commands.ImportNoteFromFile;
using Notes.Application.Features.Notes.Dto;
using Notes.Application.Features.Notes.Queries.GetNoteById;
using Notes.Application.Features.Notes.Queries.GetNotes;
using Notes.WebApi.Extensions;
using Notes.WebApi.Models;

namespace Notes.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с записками.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NotesController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор для работы с командами.</param>
    public NotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получает все записки.
    /// </summary>
    /// <param name="parameters">Параметры запроса</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список записок.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteBaseDto>>> GetAll([FromQuery] GetAllNotesQueryParameters parameters, CancellationToken cancellationToken)
    {
        var notes = await _mediator.Send(new GetAllNotesQuery(parameters), cancellationToken);
        return Ok(notes);
    }

    /// <summary>
    /// Получает записку по ID.
    /// </summary>
    /// <param name="id">ID записки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Записка.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteBaseDto>> Get(ObjectId id, CancellationToken cancellationToken)
    {
        var note = await _mediator.Send(new GetNoteByIdQuery { Id = id }, cancellationToken);
        if (note == null)
        {
            return NotFound();
        }

        return Ok(note);
    }

    /// <summary>
    /// Удаляет записку.
    /// </summary>
    /// <param name="id">ID записки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат удаления.</returns>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(ObjectId id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteNoteCommand() { Id = id }, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Прикрепляет файл к заметке.
    /// </summary>
    /// <param name="noteId">ID заметки.</param>
    /// <param name="file">Файл для прикрепления.</param>
    /// <returns>URL прикрепленного файла.</returns>
    [Authorize]
    [HttpPost("{noteId}/attach")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AttachFileToNote(ObjectId noteId, IFormFile file)
    {
        var command = new AttachFileToNoteCommand
        {
            NoteId = noteId,
            File = file.ToFileDto()
        };
        
        var fileUrl = await _mediator.Send(command);
        return Ok(fileUrl);
    }

    /// <summary>
    /// Импортирует записку из файла.
    /// </summary>
    /// <param name="noteTypeId">Идентификатор типа записки.</param>
    /// <param name="model">Модель с файлом и заголовком.</param>
    /// <returns>Результат импорта.</returns>
    [Authorize]
    [HttpPost("import/{noteTypeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportFromFile([FromRoute] string noteTypeId, [FromForm] ImportNoteFromFileDto model)
    {
        var command = new ImportNoteFromFileCommand(model.File.ToFileDto(), noteTypeId, model.Title);
        
        await _mediator.Send(command);
        return Ok();
    }
} 