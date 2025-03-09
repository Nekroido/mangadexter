using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Mangadexter.Core;
using Mangadexter.Gui.Helpers;

namespace Mangadexter.Gui.Models;

public record Manga(string Id, string Title, string Description, Uri? Cover, MangaStatus Status, uint? Year, IEnumerable<Author> Authors, decimal? LatestChapter, uint? LatestVolume)
{
    public static Manga FromDomain(Core.Manga manga)
    {
        return new(
            manga.id.Item,
            manga.title.Item,
            manga.description.Item,
            manga.cover?.Value?.Item,
            manga.status.FromDomain(),
            manga.year?.Value,
            manga.authors.Select(Author.FromDomain),
            manga.latestChapter?.Value?.Item,
            manga.latestVolume?.Value
        ); ;
    }

    public Task<Bitmap?> CoverAsBitmap => Cover != null ? ImageHelper.LoadFromWeb(Cover) : Task.FromResult<Bitmap?>(ImageHelper.LoadFromResource(new("avares://Mangadexter.Gui/Assets/cover.jpg")));
}

public record Author(string Name, AuthorKind Kind)
{
    public static Author FromDomain(Core.Author author)
    {
        if (author.TryGetArtist() is { } artist)
        {
            return new(artist.Value.Item, AuthorKind.Artist);
        }
        else if (author.TryGetWriter() is { } writer)
        {
            return new(writer.Value.Item, AuthorKind.Writer);
        }
        else
        {
            throw new Exception($"Not supported Author kind {author.Tag}");
        }
    }
}

public enum MangaStatus
{
    Ongoing,
    Finished,
    Canceled,
    Hiatus
}

public enum AuthorKind
{
    Artist,
    Writer
}

public static class StatusMethods
{
    public static string GetDescription(this Status status) => status switch
    {
        _ when status.IsOngoing => "The task is ongoing.",
        _ when status.IsFinished => "The task is finished.",
        _ when status.IsCanceled => "The task is canceled.",
        _ when status.IsHiatus => "The task is on hiatus.",
        _ => throw new ArgumentException("Invalid status.")
    };

    public static MangaStatus FromDomain(this Status status) => status switch
    {
        _ when status.IsOngoing => MangaStatus.Ongoing,
        _ when status.IsFinished => MangaStatus.Finished,
        _ when status.IsCanceled => MangaStatus.Canceled,
        _ when status.IsHiatus => MangaStatus.Hiatus,
        _ => throw new ArgumentException("Invalid status.")
    };
}

public record Chapter(string Id, decimal? Number, uint? Volume, string? Title, string? Description, DateTimeOffset PublishedAt, uint TotalPages)
{
    public static Chapter FromDomain(Core.Chapter chapter) =>
        new(
            chapter.id.Item,
            chapter.number?.Value?.Item,
            chapter.volume?.Value,
            chapter.title?.Value?.Item,
            chapter.description?.Value?.Item,
            chapter.publishedAt,
            chapter.totalPages
            );

    public string GetChapterLabel() => string.Format("Chapter %s", Number?.ToString("000.###", CultureInfo.InvariantCulture) ?? "-");
    public string GetVolumeLabel() => string.Format("Volume %s", Volume?.ToString("00") ?? "-");
}

public record Publisher(string Id, string Name, string? ExternalId = null)
{
    public static Publisher FromDomain(Core.Publisher publisher) => new(publisher.id.Item, publisher.name, publisher.externalId.Value?.ToString());
}

public record Publication(
      Publisher PublishedBy,
      SupportedLanguage originalLanguage,
      SupportedLanguage? translatedLanguage = null)
{
    public static Publication FromDomain(Core.Publication publication) => new(Publisher.FromDomain(publication.publishedBy), publication.originalLanguage.FromDomain(), publication.translatedLanguage.Value.FromDomain());
}

public enum SupportedLanguage
{
    English,
    Japanese
}
public static class SupportedLanguageMethods
{
    public static SupportedLanguage FromDomain(this Core.Language language) => language switch
    {
        _ when language.IsEnglish => SupportedLanguage.English,
        _ when language.IsJapanese => SupportedLanguage.Japanese,
        _ => throw new ArgumentException($"Unsupported language {language}.")
    };
}