using MediatR;
using Notes.Application.Common.Interfaces;

namespace Notes.Application.Features.Files.Commands.DeleteFile;

/// <summary>
/// Команда удаления файла.
/// </summary>
public record DeleteFileCommand : IRequest<bool>
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; init; } = null!;
    
    /// <summary>
    /// Обработчик команды удаления файла.
    /// </summary>
    public class Handler : IRequestHandler<DeleteFileCommand, bool>
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
        public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var exists = await _fileStorageService.FileExistsAsync(request.FileName);
            if (!exists)
            {
                return false;
            }
            
            await _fileStorageService.DeleteFileAsync(request.FileName);
            return true;
        }
    }
} 