namespace P02_FootballBetting.Data.Models;

using Common;
using System.ComponentModel.DataAnnotations;

public class User
{
    public User()
    {
        this.Bets = new HashSet<Bet>();
    }

    [Key]
    public int UserId { get; set; }

    [MaxLength(ValidationConstants.USER_MAX_USERNAME_LENGTH)]
    public string Username { get; set; } = null!;

    [MaxLength(ValidationConstants.USER_MAX_PASSWORD_LENGTH)]
    public string Password { get; set; } = null!;

    [MaxLength(ValidationConstants.USER_MAX_EMAIL_LENGTH)]
    public string Email { get; set; } = null!;

    [MaxLength(ValidationConstants.USER_MAX_NAME_LENGTH)]
    public string? Name { get; set; }

    public decimal Balance { get; set; }

    public virtual ICollection<Bet> Bets { get; set; }
}