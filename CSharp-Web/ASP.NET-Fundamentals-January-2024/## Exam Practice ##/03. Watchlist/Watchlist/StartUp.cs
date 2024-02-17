using Microsoft.EntityFrameworkCore;
using Watchlist.Data;
using Watchlist.Data.Models;
using Watchlist.Services;
using Watchlist.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WatchlistDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
	{
		options.Password.RequireDigit = false;
		options.Password.RequireDigit = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireUppercase = false;
		options.Password.RequiredLength = 5;
		options.SignIn.RequireConfirmedAccount = false;
	})
	.AddEntityFrameworkStores<WatchlistDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	"default",
	"{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();