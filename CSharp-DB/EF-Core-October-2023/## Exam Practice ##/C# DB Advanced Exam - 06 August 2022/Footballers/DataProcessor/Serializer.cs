namespace Footballers.DataProcessor;

using Data;
using ExportDto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using Utilities;

public class Serializer
{
    public static string ExportCoachesWithTheirFootballers(FootballersContext context)
    {
        var xmlHelper = new XmlHelper();

        ExportCoachDto[] coaches = context.Coaches
            .Where(c => c.Footballers.Any())
            .Select(c => new ExportCoachDto
            {
                CoachName = c.Name,
                FootballersCount = c.Footballers.Count,
                Footballers = c.Footballers
                    .Select(f => new ExportFootballerDto
                    {
                        Name = f.Name,
                        Position = f.PositionType.ToString()
                    })
                    .OrderBy(f => f.Name)
                    .ToArray()
            })
            .AsNoTracking()
            .OrderByDescending(c => c.FootballersCount)
            .ThenBy(c => c.CoachName)
            .ToArray();

        return xmlHelper.Serialize(coaches, "Coaches");
    }

    public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
    {
        var teams = context.Teams
            .Include(db => db.TeamsFootballers)
            .Where(t => t.TeamsFootballers.Any(tf => tf.Footballer.ContractEndDate >= date))
            .Select(t => new
            {
                t.Name,
                Footballers = t.TeamsFootballers
                    .Where(tf => tf.Footballer.ContractStartDate >= date)
                    .ToArray()
                    .OrderByDescending(tf => tf.Footballer.ContractEndDate)
                    .ThenBy(tf => tf.Footballer.Name)
                    .Select(tf => new
                    {
                        FootballerName = tf.Footballer.Name,
                        ContractStartDate = tf.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                        ContractEndDate = tf.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                        BestSkillType = tf.Footballer.BestSkillType.ToString(),
                        PositionType = tf.Footballer.PositionType.ToString()
                    })
            })
            .AsNoTracking()
            .OrderByDescending(t => t.Footballers.Count())
            .ThenBy(t => t.Name)
            .Take(5)
            .ToArray();

        return JsonConvert.SerializeObject(teams, Formatting.Indented);
    }
}