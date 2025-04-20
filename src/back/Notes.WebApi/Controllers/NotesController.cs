using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Features.Files.Commands.AttachFileToNote;
using Notes.Application.Features.Notes.Commands.CreateNote;
using Notes.Application.Features.Notes.Commands.DeleteNote;
using Notes.Application.Features.Notes.Commands.UpdateNote;
using Notes.Application.Features.Notes.Dto;
using Notes.Application.Features.Notes.Queries.GetNoteById;
using Notes.Application.Features.Notes.Queries.GetNotes;

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
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список записок.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetAll(CancellationToken cancellationToken)
    {
        var notes = await _mediator.Send(new GetAllNotesQuery(), cancellationToken);
        return Ok(notes);
    }

    /// <summary>
    /// Получает записку по ID.
    /// </summary>
    /// <param name="id">ID записки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Записка.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<NoteDto>> Get(int id, CancellationToken cancellationToken)
    {
        var note = await _mediator.Send(new GetNoteByIdQuery { Id = id }, cancellationToken);
        if (note == null)
        {
            return NotFound();
        }

        return Ok(note);
    }

    /// <summary>
    /// Создает новую записку.
    /// </summary>
    /// <param name="command">Команда создания записки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданная записка.</returns>
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateNoteCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Created();
    }

    /// <summary>
    /// Обновляет существующую записку.
    /// </summary>
    /// <param name="id">ID записки.</param>
    /// <param name="command">Команда обновления записки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленная записка.</returns>
    [HttpPut]
    public async Task<ActionResult<NoteDto>> Update(
        UpdateNoteCommand command,
        CancellationToken cancellationToken)
    {
        var note = await _mediator.Send(command, cancellationToken);

        return Ok(note);
    }

    /// <summary>
    /// Удаляет записку.
    /// </summary>
    /// <param name="id">ID записки.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат удаления.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
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
    [HttpPost("{noteId:int}/attach")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AttachFileToNote(int noteId, IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var command = new AttachFileToNoteCommand
        {
            NoteId = noteId,
            File = file
        };
        
        var fileUrl = await _mediator.Send(command);
        return Ok(fileUrl);
    }
} 