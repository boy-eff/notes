using System;

namespace Notes.WebApi.Configuration;

/// <summary>
/// Настройки для интеграции с Keycloak.
/// </summary>
public class KeycloakConfiguration
{
    /// <summary>
    /// URL сервера Keycloak.
    /// </summary>
    public string Authority { get; set; } = string.Empty;

    /// <summary>
    /// Получатель токена.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// ID клиента в Keycloak.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Секрет клиента в Keycloak.
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// Реалм в Keycloak.
    /// </summary>
    public string Realm { get; set; } = string.Empty;
} 