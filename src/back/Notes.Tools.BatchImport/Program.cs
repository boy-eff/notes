// See https://aka.ms/new-console-template for more information

using System.Net.Http.Headers;
using System.Text.Json;

var path = GetArgument(args, "--path");
var baseUrl = GetArgument(args, "--base-url");
var noteType = GetArgument(args, "--note-type");

// Keycloak parameters
var keycloakUrl = GetArgument(args, "--keycloak-url");
var keycloakRealm = GetArgument(args, "--keycloak-realm");
var keycloakClientId = GetArgument(args, "--keycloak-client-id");
var keycloakClientSecret = GetArgument(args, "--keycloak-client-secret");

if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(noteType))
{
    Console.WriteLine("Необходимо указать обязательные параметры.");
    Console.WriteLine("Использование: dotnet run -- --path <путь> --base-url <url> --note-type <тип>");
    Console.WriteLine("Для авторизации добавьте параметры Keycloak: --keycloak-url <url> --keycloak-realm <realm> --keycloak-client-id <id> --keycloak-client-secret <secret>");
    return;
}

if (!Directory.Exists(path))
{
    Console.WriteLine($"Ошибка: Директория не найдена '{path}'");
    return;
}

using var client = new HttpClient();
var useAuth = !string.IsNullOrEmpty(keycloakUrl);

if (useAuth)
{
    if (string.IsNullOrEmpty(keycloakRealm) || string.IsNullOrEmpty(keycloakClientId) || string.IsNullOrEmpty(keycloakClientSecret))
    {
        Console.WriteLine("Для авторизации через Keycloak необходимо указать все параметры: --keycloak-url, --keycloak-realm, --keycloak-client-id, --keycloak-client-secret");
        return;
    }

    Console.WriteLine("Попытка получения токена доступа от Keycloak...");
    var accessToken = await GetAccessTokenAsync(keycloakUrl, keycloakRealm, keycloakClientId, keycloakClientSecret);
    if (string.IsNullOrEmpty(accessToken))
    {
        Console.WriteLine("Ошибка: Не удалось получить токен доступа.");
        return;
    }
    Console.WriteLine("Токен доступа успешно получен.");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
}

var files = Directory.GetFiles(path);

Console.WriteLine($"Найдено {files.Length} файлов для импорта в директории '{path}'.");

foreach (var filePath in files)
{
    await ImportFileAsync(client, filePath, baseUrl, noteType);
}

static async Task<string?> GetAccessTokenAsync(string keycloakUrl, string realm, string clientId, string clientSecret)
{
    try
    {
        using var authClient = new HttpClient();
        var tokenEndpoint = $"{keycloakUrl.TrimEnd('/')}/realms/{realm}/protocol/openid-connect/token";

        var requestData = new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "grant_type", "client_credentials" }
        };

        using var requestContent = new FormUrlEncodedContent(requestData);
        var response = await authClient.PostAsync(tokenEndpoint, requestContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(responseContent);
            return jsonDoc.RootElement.GetProperty("access_token").GetString();
        }

        var errorContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"\nОшибка при получении токена от Keycloak. Статус: {response.StatusCode}. Ответ: {errorContent}");
        return null;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\nОшибка при запросе токена: {ex.Message}");
        return null;
    }
}

static async Task ImportFileAsync(HttpClient client, string filePath, string baseUrl, string noteType)
{
    try
    {
        var fileName = Path.GetFileName(filePath);
        Console.Write($"Импорт файла '{fileName}'...");
        
        var title = Path.GetFileNameWithoutExtension(filePath);

        await using var stream = File.OpenRead(filePath);
        using var content = new MultipartFormDataContent();
        
        var streamContent = new StreamContent(stream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        
        content.Add(streamContent, "file", fileName);
        content.Add(new StringContent(title), "title");

        var requestUri = $"{baseUrl}/api/notes/import/{noteType}";
        var response = await client.PostAsync(requestUri, content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(" Успешно.");
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($" Ошибка. Статус: {response.StatusCode}. Ответ: {error}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Ошибка. Исключение: {ex.Message}");
    }
}

static string? GetArgument(string[] args, string argumentName)
{
    for (var i = 0; i < args.Length - 1; i++)
    {
        if (args[i].Equals(argumentName, StringComparison.OrdinalIgnoreCase))
        {
            return args[i + 1];
        }
    }
    return null;
}