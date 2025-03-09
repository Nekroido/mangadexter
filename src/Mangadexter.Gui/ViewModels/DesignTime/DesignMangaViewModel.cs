using System;
using System.Collections.Generic;

using Mangadexter.Gui.Models;

namespace Mangadexter.Gui.ViewModels.DesignTime;
internal class DesignMangaViewModel
{
    public Manga? Manga => new(
                "42caa178-b6dc-4ed1-bcbf-18f457bbd121",
                "DARLING in the FRANXX",
                "The story is set in the distant future. The land is ruined, and humanity establishes the mobile fort city Plantation. Pilots produced inside Plantation live in Mistilteinn, also know as the \"birdcage.\" Children live there knowing nothing of the outside world or the freedom of the sky. Their lives consist of battling to carry out missions. Their enemies are mysterious giant lifeforms known as Kyouryuu, and the children pilot robots called FranXX to face off against them. For the children, riding the FranXX proves their existence.\r\n\r\nA boy named Hiro is called Code:016, and he was once known as a prodigy. However, he has fallen behind, and his existence seems unnecessary. Not piloting a FranXX is the same as ceasing to exist. One day, a mysterious girl known as \"Zero Two\" appears before him. Two horns grow out of her head.",
                new Uri("https://mangadex.org/covers/42caa178-b6dc-4ed1-bcbf-18f457bbd121/91b60d5f-4f71-40a1-aca1-acca5389ae5c.jpg.512.jpg"),
                MangaStatus.Finished,
                2018,
                [new("Code 000", AuthorKind.Writer), new("Yabuki Kentarou", AuthorKind.Artist)],
                60,
                8
            );

    public List<Chapter> Chapters => [
        new(
            "b9d10b86-c956-4191-b05b-6cce5143cee4",
            1,
            1,
            "",
            "",
            DateTimeOffset.Now,
            66)
        ];

    public bool IsLoading => false;
}
