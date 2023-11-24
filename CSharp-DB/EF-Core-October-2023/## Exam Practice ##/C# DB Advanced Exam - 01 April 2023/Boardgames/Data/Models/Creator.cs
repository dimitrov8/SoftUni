namespace Boardgames.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Creator
{
    public Creator()
    {
        this.Boardgames = new HashSet<Boardgame>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(ValidationConstants.MIN_CREATOR_FIRST_NAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_CREATOR_FIRST_NAME_LENGTH)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MinLength(ValidationConstants.MIN_CREATOR_LAST_NAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_CREATOR_LAST_NAME_LENGTH)]
    public string LastName { get; set; } = null!;

    public virtual ICollection<Boardgame> Boardgames { get; set; }
}