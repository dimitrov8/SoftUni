namespace Footballers.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Team
{
    public Team()
    {
        this.TeamsFootballers = new HashSet<TeamFootballer>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [RegularExpression(ValidationConstants.TEAM_NAME_REGEX)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(ValidationConstants.TEAM_NATIONALITY_MAX_LENGTH)]
    public string Nationality { get; set; } = null!;

    [Required]
    public int Trophies { get; set; }

    public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; }
}