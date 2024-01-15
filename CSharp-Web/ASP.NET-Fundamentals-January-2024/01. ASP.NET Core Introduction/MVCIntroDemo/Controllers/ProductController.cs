namespace MVCIntroDemo.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;

public class ProductController : Controller
{
	private IEnumerable<ProductViewModel> products = new List<ProductViewModel>
	{
		new()
		{
			Id = 1,
			Name = "Cheese",
			Price = 7.00
		},
		new()
		{
			Id = 2,
			Name = "Ham",
			Price = 5.50
		},
		new()
		{
			Id = 3,
			Name = "Bread",
			Price = 1.50
		}
	};

	public IActionResult All()
	{
		return this.View(this.products);
	}
}