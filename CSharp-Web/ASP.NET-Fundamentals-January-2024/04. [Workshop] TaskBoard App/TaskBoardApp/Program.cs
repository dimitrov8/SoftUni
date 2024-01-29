namespace TaskBoardApp;

using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
		                          throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		builder.Services.AddDbContext<TaskBoardAppDbContext>(options =>
			options.UseSqlServer(connectionString));

		builder.Services.AddDatabaseDeveloperPageExceptionFilter();

		builder.Services.AddDefaultIdentity<IdentityUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = builder.Configuration
					.GetValue<bool>("RequireConfirmedAccount");

				options.Password.RequireLowercase = builder.Configuration
					.GetValue<bool>("RequireLowercase");

				options.Password.RequireUppercase = builder.Configuration
					.GetValue<bool>("RequireUppercase");

				options.Password.RequireNonAlphanumeric = builder.Configuration
					.GetValue<bool>("RequireNonAlphanumeric");

				options.Password.RequiredLength = builder.Configuration
					.GetValue<int>("RequiredLength");
			})
			.AddEntityFrameworkStores<TaskBoardAppDbContext>();

		builder.Services.AddScoped<IBoardService, BoardService>();
		builder.Services.AddScoped<ITaskService, TaskService>();

		builder.Services.AddControllersWithViews();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
	}
}