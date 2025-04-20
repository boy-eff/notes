using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
    public FileDto File { get; init; } = null!;
    
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
            var url = await _fileStorageService.UploadFileAsync(
                request.File.FileName,
                request.File.Data,
                request.File.ContentType);
                
            return new FileDto
            {
                FileName = request.File.FileName,
                ContentType = request.File.ContentType,
                Data = request.File.Data
            };
        }
    }
} 