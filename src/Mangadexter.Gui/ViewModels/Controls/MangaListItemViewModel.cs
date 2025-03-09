using Mangadexter.Gui.Models;
using System;
using System.Collections.Generic;

namespace Mangadexter.Gui.ViewModels.Controls;

public record MangaListItemViewModel
{
    public Guid Id => Guid.Parse("42caa178-b6dc-4ed1-bcbf-18f457bbd121");
    public string Title => "DARLING in the FRANXX";
    public string Description => "The story is set in the distant future. The land is ruined, and humanity establishes the mobile fort city Plantation. Pilots produced inside Plantation live in Mistilteinn, also know as the \"birdcage.\" Children live there knowing nothing of the outside world or the freedom of the sky. Their lives consist of battling to carry out missions. Their enemies are mysterious giant lifeforms known as Kyouryuu, and the children pilot robots called FranXX to face off against them. For the children, riding the FranXX proves their existence.\r\n\r\nA boy named Hiro is called Code:016, and he was once known as a prodigy. However, he has fallen behind, and his existence seems unnecessary. Not piloting a FranXX is the same as ceasing to exist. One day, a mysterious girl known as \"Zero Two\" appears before him. Two horns grow out of her head.";
    public Uri? Cover => new Uri("https://mangadex.org/covers/42caa178-b6dc-4ed1-bcbf-18f457bbd121/91b60d5f-4f71-40a1-aca1-acca5389ae5c.jpg");
    public MangaStatus Status => MangaStatus.Finished;
    public uint Year => 2018;
    public IEnumerable<Author> Authors => [new("Code 000", AuthorKind.Writer), new("Yabuki Kentarou", AuthorKind.Artist)];
    public decimal? LatestChapter => 60;
    public uint LatestVolume => 8u;
}
