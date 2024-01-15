namespace MVCIntroDemo.Controllers;

using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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

	public IActionResult ById(int id)
	{
		var product = this.products
			.FirstOrDefault(p => p.Id == id);

		if (product == null)
		{
			return this.BadRequest();
		}

		return this.View(product);
	}

	public IActionResult AllAsJson()
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		return this.Json(this.products, options);
	}

	public IActionResult AllAsText()
	{
		IEnumerable<string> productLines = this.products
			.Select(product => $"Product {product.Id}: {product.Name} - {product.Price} lv.");

		string result = string.Join(Environment.NewLine, productLines);

		this.Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");

		return this.File(Encoding.UTF8.GetBytes(result), "text/plain");
	}
}