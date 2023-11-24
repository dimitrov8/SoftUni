namespace Boardgames.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class Seller
{
    public Seller()
    {
        this.BoardgamesSellers = new HashSet<BoardgameSeller>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(ValidationConstants.MIN_SELLER_NAME_LENGTH)]
    [MaxLength(ValidationConstants.MAX_SELLER_NAME_LENGTH)]
    public string Name { get; set; } = null!;

    [Required]
    [MinLength(ValidationConstants.MIN_SELLER_ADDRESS_LENGTH)]
    [MaxLength(ValidationConstants.MAX_SELLER_ADDRESS_LENGTH)]
    public string Address { get; set; } = null!;

    [Required]
    public string Country { get; set; } = null!;

    [Required]
    [RegularExpression(ValidationConstants.REGEX_SELLER_WEBSITE)]
    public string Website { get; set; } = null!;

    public virtual ICollection<BoardgameSeller> BoardgamesSellers { get; set; }
}