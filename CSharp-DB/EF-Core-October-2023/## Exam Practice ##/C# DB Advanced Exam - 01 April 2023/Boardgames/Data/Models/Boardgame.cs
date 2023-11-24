namespace Boardgames.Data.Models;

using Common;
using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Boardgame
{
    public Boardgame()
    {
        this.BoardgamesSellers = new HashSet<BoardgameSeller>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(ValidationConstants.MIN_BOARD_GAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_BOARD_GAME_LENGTH)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(ValidationConstants.MIN_BOARD_GAME_RATING, ValidationConstants.MAX_BOARD_GAME_RATING)]
    public double Rating { get; set; }

    [Required]
    [Range(ValidationConstants.MIN_BOARD_GAME_YEAR, ValidationConstants.MAX_BOARD_GAME_YEAR)]
    public int YearPublished { get; set; }

    [Required]
    public virtual CategoryType CategoryType { get; set; }

    [Required]
    public string Mechanics { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Creator))]
    public int CreatorId { get; set; }

    public virtual Creator Creator { get; set; } = null!;

    public virtual ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
}