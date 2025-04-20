using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Common.Interfaces;

namespace Notes.Application.Features.Files.Queries.DownloadFile;

/// <summary>
/// Запрос для скачивания файла.
/// </summary>
public class DownloadFileQuery : IRequest<Stream>
{
    /// <summary>
    /// Имя файла.
    /// </summary>
    public string FileName { get; set; }
}

/// <summary>
/// Обработчик запроса скачивания файла.
/// </summary>
public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, Stream>
{
    private readonly IFileStorageService _fileStorageService;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DownloadFileQueryHandler"/>.
    /// </summary>
    /// <param name="fileStorageService">Сервис для работы с файлами.</param>
    public DownloadFileQueryHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }
    
    /// <inheritdoc />
    public async Task<Stream> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        return await _fileStorageService.DownloadFileAsync(request.FileName);
    }
} 