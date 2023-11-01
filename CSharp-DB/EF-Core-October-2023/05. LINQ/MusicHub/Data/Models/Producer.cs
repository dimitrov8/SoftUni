namespace MusicHub.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Producer
{
    public Producer()
    {
        this.Albums = new HashSet<Album>();
    }

    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.MAX_PRODUCER_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    public string? Pseudonym { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Album> Albums { get; set; }
}