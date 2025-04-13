using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Features.Files.Commands.DeleteFile;
using Notes.Application.Features.Files.Commands.UploadFile;
using Notes.Application.Features.Files.Dto;
using Notes.Application.Features.Files.Queries.GetFileByName;

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
    /// <param name="mediator">Медиатор.</param>
    public FilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Загружает файл.
    /// </summary>
    /// <param name="file">Файл для загрузки.</param>
    /// <returns>Информация о загруженном файле.</returns>
    [HttpPost("upload")]
    [ProducesResponseType(typeof(FileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var command = new UploadFileCommand
        {
            File = file
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Скачивает файл по имени.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Файл.</returns>
    [HttpGet("{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var query = new GetFileByNameQuery
        {
            FileName = fileName
        };

        var (file, content) = await _mediator.Send(query);
        
        if (file == null || content == null)
        {
            return NotFound($"Файл с именем {fileName} не найден.");
        }

        return File(content, file.ContentType, file.Name);
    }

    /// <summary>
    /// Удаляет файл по имени.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Результат операции.</returns>
    [HttpDelete("{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFile(string fileName)
    {
        var command = new DeleteFileCommand
        {
            FileName = fileName
        };

        var result = await _mediator.Send(command);
        
        if (!result)
        {
            return NotFound($"Файл с именем {fileName} не найден.");
        }

        return Ok();
    }
} 