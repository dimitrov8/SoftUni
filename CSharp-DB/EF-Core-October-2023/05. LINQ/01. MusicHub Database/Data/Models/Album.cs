namespace MusicHub.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Album
{
    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.MAX_ALBUM_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public decimal Price
        => this.Songs.Sum(s => s.Price);

    [ForeignKey(nameof(Producer))]
    public int? ProducerId { get; set; }

    public virtual Producer? Producer { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = null!;
}