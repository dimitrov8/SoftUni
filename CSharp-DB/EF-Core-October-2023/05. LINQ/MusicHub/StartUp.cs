﻿namespace MusicHub;

using Data;
using Initializer;
using System.Globalization;
using System.Text;

public class StartUp
{
    public static void Main()
    {
        var context =
            new MusicHubDbContext();

        DbInitializer.ResetDatabase(context);

        //Test your solutions here
        string result = ExportSongsAboveDuration(context, 4);
        Console.WriteLine(result);
    }

    public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
    {
        var albums = context.Albums
            .Where(a => a.ProducerId.HasValue && a.ProducerId.Value == producerId)
            .ToArray() // This is because of bug in EF
            .OrderByDescending(a => a.Price)
            .Select(a => new
            {
                a.Name,
                ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ProducerName = a.Producer.Name,
                AlbumPrice = a.Price.ToString("F2"),
                Songs = a.Songs
                    .Select(s => new
                    {
                        s.Name,
                        Price = s.Price.ToString("F2"),
                        WriterName = s.Writer.Name
                    })
                    .OrderByDescending(s => s.Name)
                    .ThenBy(s => s.WriterName)
                    .ToArray()
            })
            .ToArray();

        var sb = new StringBuilder();

        foreach (var a in albums)
        {
            sb.AppendLine($"-AlbumName: {a.Name}")
                .AppendLine($"-ReleaseDate: {a.ReleaseDate}")
                .AppendLine($"-ProducerName: {a.ProducerName}")
                .AppendLine("-Songs:");

            int songNumber = 0;

            foreach (var s in a.Songs)
            {
                sb.AppendLine($"---#{++songNumber}")
                    .AppendLine($"---SongName: {s.Name}")
                    .AppendLine($"---Price: {s.Price}")
                    .AppendLine($"---Writer: {s.WriterName}");
            }

            sb.AppendLine($"-AlbumPrice: {a.AlbumPrice}");
        }

        return sb.ToString().TrimEnd();
    }

    public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
    {
        var songs = context.Songs
            .AsEnumerable()
            .Where(s => s.Duration.TotalSeconds > duration)
            .Select(s => new
            {
                s.Name,
                WriterName = s.Writer.Name,

                Performers = s.SongPerformers
                    .Select(sp => new
                    {
                        PerformerFullName = $"{sp.Performer.FirstName} {sp.Performer.LastName}"
                    })
                    .OrderBy(p => p.PerformerFullName)
                    .ToArray(),
                AlbumProducerFullName = s.Album.Producer.Name,
                Duration = s.Duration.ToString("c")
            })
            .OrderBy(s => s.Name)
            .ThenBy(s => s.WriterName)
            .ToArray();

        var sb = new StringBuilder();

        int songNumber = 0;

        foreach (var s in songs)
        {
            sb.AppendLine($"-Song #{++songNumber}")
                .AppendLine($"---SongName: {s.Name}")
                .AppendLine($"---Writer: {s.WriterName}");

            foreach (var p in s.Performers)
            {
                sb.AppendLine($"---Performer: {p.PerformerFullName}");
            }

            sb.AppendLine($"---AlbumProducer: {s.AlbumProducerFullName}")
                .AppendLine($"---Duration: {s.Duration}");
        }

        return sb.ToString().TrimEnd();
    }
}