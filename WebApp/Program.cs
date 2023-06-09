﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using WebApp.Models;

namespace WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<WebAppContext>(options =>
			    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppContext") ?? throw new InvalidOperationException("Connection string 'WebAppContext' not found.")));

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			//Add SeedData
			builder.Services.AddScoped<SeedData>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			SeedDatabase(); //

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();

			void SeedDatabase()
			{
				//Create a new serviceprovider and call Initialize from SeedData.
				using (var scope = app.Services.CreateScope())
				{
					var services = scope.ServiceProvider;
					try
					{
						SeedData.Initialize(services);
					}
					catch (Exception ex)
					{
						var logger = services.GetRequiredService<ILogger<Program>>();
						logger.LogError(ex, "An error occured while trying to seed the database.");
					}
				}
			}
		}
	}
}