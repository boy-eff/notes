using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Files.Dto;

namespace Notes.Application.Features.Files.Commands.UploadFile;

/// <summary>
/// Команда загрузки файла.
/// </summary>
public record UploadFileCommand : IRequest<FileDto>
{
    /// <summary>
    /// Файл для загрузки.
    /// </summary>
    public IFormFile File { get; init; } = null!;
    
    /// <summary>
    /// Обработчик команды загрузки файла.
    /// </summary>
    public class Handler : IRequestHandler<UploadFileCommand, FileDto>
    {
        private readonly IFileStorageService _fileStorageService;
        
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Handler"/>.
        /// </summary>
        /// <param name="fileStorageService">Сервис для работы с файловым хранилищем.</param>
        public Handler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        
        /// <inheritdoc />
        public async Task<FileDto> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            await using var stream = request.File.OpenReadStream();
            var url = await _fileStorageService.UploadFileAsync(
                request.File.FileName,
                stream,
                request.File.ContentType);
                
            return new FileDto(
                request.File.FileName,
                request.File.FileName,
                request.File.ContentType,
                url,
                request.File.Length);
        }
    }
} 