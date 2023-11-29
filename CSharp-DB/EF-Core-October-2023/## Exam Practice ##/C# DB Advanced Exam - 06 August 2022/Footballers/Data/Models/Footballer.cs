namespace Footballers.Data.Models;

using Common;
using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Footballer
{
    public Footballer()
    {
        this.TeamsFootballers = new HashSet<TeamFootballer>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(ValidationConstants.FOOTBALLER_NAME_MAX_LENGTH)]
    public string Name { get; set; }

    [Required]
    public DateTime ContractStartDate { get; set; }

    [Required]
    public DateTime ContractEndDate { get; set; }

    [Required]
    public PositionType PositionType { get; set; }

    [Required]
    public PositionType BestSkillType { get; set; }

    [Required]
    [ForeignKey(nameof(Coach))]
    public int CoachId { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; }
}