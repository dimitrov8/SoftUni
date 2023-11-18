namespace ProductShop.Models;

public class User
{
    public User()
    {
        this.ProductsSold = new HashSet<Product>();
        this.ProductsBought = new HashSet<Product>();
    }

    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? Age { get; set; }

    public virtual ICollection<Product> ProductsSold { get; set; }
    public virtual ICollection<Product> ProductsBought { get; set; }
}