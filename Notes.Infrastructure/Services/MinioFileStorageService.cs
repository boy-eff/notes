using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Notes.Application.Common.Interfaces;
using Notes.Infrastructure.Configuration;

namespace Notes.Infrastructure.Services;

/// <summary>
/// Сервис для работы с файловым хранилищем MinIO.
/// </summary>
public class MinioFileStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="MinioFileStorageService"/>.
    /// </summary>
    /// <param name="minioConfiguration">Конфигурация приложения.</param>
    public MinioFileStorageService(IOptions<MinioConfiguration> minioConfiguration)
    {
        _minioClient = new MinioClient()
            .WithEndpoint(minioConfiguration.Value.Endpoint)
            .WithCredentials(minioConfiguration.Value.AccessKey, minioConfiguration.Value.SecretKey)
            .Build();
            
        _bucketName = minioConfiguration.Value.BucketName;
    }
    
    /// <inheritdoc />
    public async Task<string> UploadFileAsync(string fileName, Stream content, string contentType)
    {
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName)
            .WithStreamData(content)
            .WithObjectSize(content.Length)
            .WithContentType(contentType);
            
        await _minioClient.PutObjectAsync(putObjectArgs);
        
        return $"/{_bucketName}/{fileName}";
    }
    
    /// <inheritdoc />
    public async Task<Stream> DownloadFileAsync(string fileName)
    {
        var memoryStream = new MemoryStream();
        var getObjectArgs = new GetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream => stream.CopyTo(memoryStream));
            
        await _minioClient.GetObjectAsync(getObjectArgs);
        memoryStream.Position = 0;
        return memoryStream;
    }
    
    /// <inheritdoc />
    public async Task DeleteFileAsync(string fileName)
    {
        var removeArgs = new RemoveObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName);
            
        await _minioClient.RemoveObjectAsync(removeArgs);
    }
    
    /// <inheritdoc />
    public async Task<bool> FileExistsAsync(string fileName)
    {
        try
        {
            var statArgs = new StatObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName);
                
            await _minioClient.StatObjectAsync(statArgs);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
} 