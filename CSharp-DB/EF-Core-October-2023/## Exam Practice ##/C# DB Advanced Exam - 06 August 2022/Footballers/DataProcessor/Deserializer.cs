namespace Footballers.DataProcessor;

using Data;
using Data.Models;
using Data.Models.Enums;
using ImportDto;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using Utilities;

public class Deserializer
{
    private const string ERROR_MESSAGE = "Invalid data!";

    private const string SUCCESSFULLY_IMPORTED_COACH
        = "Successfully imported coach - {0} with {1} footballers.";

    private const string SUCCESSFULLY_IMPORTED_TEAM
        = "Successfully imported team - {0} with {1} footballers.";

    public static string ImportCoaches(FootballersContext context, string xmlString)
    {
        var sb = new StringBuilder();
        var xmlHelper = new XmlHelper();

        ImportCoachDto[]? coachDtos = xmlHelper.Deserialize<ImportCoachDto[]>(xmlString, "Coaches");

        var validCoaches = new HashSet<Coach>();

        foreach (var coachDto in coachDtos)
        {
            if (!IsValid(coachDto))
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            var coach = new Coach
            {
                Name = coachDto.Name,
                Nationality = coachDto.Nationality
            };

            var validFootballers = new HashSet<Footballer>();

            foreach (var footballerDto in coachDto.Footballers)
            {
                if (!IsValid(footballerDto))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var contractStartDate = DateTime.ParseExact(footballerDto.ContractStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var contractEndDate = DateTime.ParseExact(footballerDto.ContractEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (contractStartDate > contractEndDate)
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var footballer = new Footballer
                {
                    Name = footballerDto.Name,
                    ContractStartDate = contractStartDate,
                    ContractEndDate = contractEndDate,
                    PositionType = (PositionType)footballerDto.PositionType,
                    BestSkillType = (BestSkillType)footballerDto.BestSkillType
                };

                validFootballers.Add(footballer);
            }

            coach.Footballers = validFootballers;
            validCoaches.Add(coach);

            sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_COACH, coach.Name, coach.Footballers.Count));
        }

        context.Coaches.AddRange(validCoaches);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    public static string ImportTeams(FootballersContext context, string jsonString)
    {
        var sb = new StringBuilder();
        ImportTeamDto[] teamDtos = JsonConvert.DeserializeObject<ImportTeamDto[]>(jsonString);
        int[] existingFootballerIds = context.Footballers
            .Select(f => f.Id)
            .ToArray();

        var validTeams = new HashSet<Team>();

        foreach (var teamDto in teamDtos)
        {
            if (!IsValid(teamDto) || teamDto.Trophies < 1)
            {
                sb.AppendLine(ERROR_MESSAGE);
                continue;
            }

            var team = new Team
            {
                Name = teamDto.Name,
                Nationality = teamDto.Nationality,
                Trophies = teamDto.Trophies
            };

            foreach (int footballerId in teamDto.Footballers.Distinct())
            {
                if (!existingFootballerIds.Contains(footballerId))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                team.TeamsFootballers.Add(new TeamFootballer
                {
                    Team = team,
                    Footballer = context.Footballers.First(f => f.Id == footballerId)
                });
            }

            validTeams.Add(team);
            sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_TEAM, team.Name, team.TeamsFootballers.Count));
        }

        context.Teams.AddRange(validTeams);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    private static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
}