using Notes.Application.Services.Interfaces;
using Notes.Domain.Entities;

namespace Notes.Application.Services;

/// <summary>
/// Парсер заметок о фильмах.
/// </summary>
public class MovieNoteParser : INoteParser
{
    private const string MovieInfoSeparator = "## Movie Info";
    private const string SynopsisSeparator = "## Synopsis";
    private const string OpinionSeparator = "## Opinion";

    /// <inheritdoc />
    public NoteBase Parse(string title, string content)
    {
        var movieInfoSection = ExtractSection(content, MovieInfoSeparator, SynopsisSeparator);
        var synopsisSection = ExtractSection(content, SynopsisSeparator, OpinionSeparator);
        var opinionSection = ExtractSection(content, OpinionSeparator, null);

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

        return content[startIndex..endIndex].Trim();
    }
}