using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Features.Files.Commands.AttachFileToNote;
using Notes.Application.Features.Files.Commands.DeleteFile;
using Notes.Application.Features.Files.Commands.UploadFile;
using Notes.Application.Features.Files.Queries.DownloadFile;
using Notes.WebApi.Extensions;

namespace Notes.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с файлами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="FilesController"/>.
    /// </summary>
    /// <param name="mediator">Медиатор для отправки команд и запросов.</param>
    public FilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Загружает файл.
    /// </summary>
    /// <param name="file">Файл для загрузки.</param>
    /// <returns>URL загруженного файла.</returns>
    [Authorize]
    [HttpPost("upload")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var command = new UploadFileCommand
        {
            File = file.ToFileDto()
        };
        
        var fileUrl = await _mediator.Send(command);
        return Ok(fileUrl);
    }

    /// <summary>
    /// Скачивает файл.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Файл для скачивания.</returns>
    [HttpGet("download/{fileName}")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var query = new DownloadFileQuery { FileName = fileName };
        var fileStream = await _mediator.Send(query);
        return File(fileStream, "application/octet-stream", fileName);
    }

    /// <summary>
    /// Удаляет файл по имени.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Результат операции.</returns>
    [Authorize]
    [HttpDelete("{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFile(string fileName)
    {
        var command = new DeleteFileCommand
        {
            FileName = fileName
        };
        
        await _mediator.Send(command);
        return Ok();
    }
} 