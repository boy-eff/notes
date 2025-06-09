using Notes.Application.Services.Interfaces;
using Notes.Domain.Entities;
using System;
using System.Text.RegularExpressions;

namespace Notes.Application.Services;

/// <summary>
/// Парсер заметок о фильмах.
/// </summary>
public partial class MovieNoteParser : INoteParser
{
    private static readonly Regex TagRemovalRegex = MyRegex();
    private const string MovieInfoSeparator = "## Movie Info";
    private const string SynopsisSeparator = "## Synopsis";
    private const string OpinionSeparator = "## Opinion";

    /// <inheritdoc />
    public NoteBase Parse(string title, string content)
    {
        var movieInfoSection = ExtractSection(content, MovieInfoSeparator, SynopsisSeparator);
        var synopsisSection = ExtractSection(content, SynopsisSeparator, OpinionSeparator);
        var opinionSection = ExtractSection(content, OpinionSeparator, null);

        synopsisSection = TagRemovalRegex.Replace(synopsisSection, string.Empty).Trim();
        opinionSection = TagRemovalRegex.Replace(opinionSection, string.Empty).Trim();
        
        return new MovieNote(
            default,
            title,
            synopsisSection,
            opinionSection,
            movieInfoSection);
    }

    private string ExtractSection(string content, string startSeparator, string? endSeparator)
    {
        var startIndex = content.IndexOf(startSeparator, StringComparison.Ordinal);
        if (startIndex == -1)
        {
            throw new ArgumentException($"Не найдена секция {startSeparator}");
        }

        startIndex += startSeparator.Length;
        var endIndex = endSeparator != null 
            ? content.IndexOf(endSeparator, startIndex, StringComparison.Ordinal)
            : content.Length;

        if (endIndex == -1 && endSeparator != null)
        {
            throw new ArgumentException($"Не найдена секция {endSeparator}");
        }

        return content[startIndex..endIndex];
    }

    [GeneratedRegex(@"\s*\[.*?::.*?\]\s*", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}