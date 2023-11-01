namespace MusicHub.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Performer
{
    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.MAX_PERFORMER_FIRST_NAME_LENGTH)]
    public string FirstName { get; set; } = null!;

    [MaxLength(ValidationConstants.MAX_PERFORMER_LAST_NAME_LENGTH)]
    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public decimal NetWorth { get; set; }

    public  virtual ICollection<SongPerformer> PerformerSongs { get; set; } = null!;
}