using MediatR;
using Notes.Application.Common.Interfaces;
using Notes.Application.Features.Files.Dto;

namespace Notes.Application.Features.Files.Queries.GetFileByName;

/// <summary>
/// Запрос на получение файла по имени.
/// </summary>
public record GetFileByNameQuery : IRequest<(FileDto? File, Stream? Content)>
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; init; } = null!;
    
    /// <summary>
    /// Обработчик запроса на получение файла по имени.
    /// </summary>
    public class Handler : IRequestHandler<GetFileByNameQuery, (FileDto? File, Stream? Content)>
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
        public async Task<(FileDto? File, Stream? Content)> Handle(GetFileByNameQuery request, CancellationToken cancellationToken)
        {
            var exists = await _fileStorageService.FileExistsAsync(request.FileName);
            if (!exists)
            {
                return (null, null);
            }
            
            var stream = await _fileStorageService.DownloadFileAsync(request.FileName);
            
            // В реальном приложении здесь нужно определить тип содержимого
            // Для простоты используем фиктивное значение
            var fileDto = new FileDto
            {
                FileName = request.FileName,
                ContentType = "application/octet-stream",
                Data = stream
            };
                
            return (fileDto, stream);
        }
    }
} 