namespace Forum.App;

using Data;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		string? connectionString = builder
			.Configuration
			.GetConnectionString("DefaultConnection");

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		// Adding DbContext allows us to take instance of DbContext  in the entire application
		builder.Services.AddDbContext<ForumAppDbContext>(options => { options.UseSqlServer(connectionString); });

		// Add custom services
		builder.Services.AddScoped<IPostService, PostService>();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			//app.UseDeveloperExceptionPage();
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.MapControllerRoute(
			"default",
			"{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}