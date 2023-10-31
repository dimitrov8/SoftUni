namespace P02_FootballBetting.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Position
{
    public Position()
    {
        this.Players = new HashSet<Player>();
    }

    [Key]
    public int PositionId { get; set; }

    [MaxLength(ValidationConstants.MAX_POSITION_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; }
}