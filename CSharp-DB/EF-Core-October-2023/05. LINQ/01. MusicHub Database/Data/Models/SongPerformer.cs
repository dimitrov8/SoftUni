namespace MusicHub.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SongPerformer
{
    [Key]
    public int SongId { get; set; }

    public virtual Song Song { get; set; } = null!;

    [ForeignKey(nameof(SongPerformer))]
    public int PerformerId { get; set; }

    public virtual Performer Performer { get; set; } = null!;
}