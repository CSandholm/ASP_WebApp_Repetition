using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Models
{
	public class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			//Check database contains cats. If not, then add a range of cats.
			using var context = new WebAppContext(serviceProvider.GetRequiredService<DbContextOptions<WebAppContext>>());
			if (context.Cats.Any())
			{
				Console.WriteLine("There are already a database with cats!");
				return;
			}
			context.AddRange
				(
					new Cat
					{
						Name = "Nisse",
						Color = "Purple",
						OwnerName = "Karl Franz",
						Age = 1,
						ImageUrl = "/images/nisse.jpg"
					},
					new Cat
					{
						Name = "Ungor",
						Color = "Black",
						OwnerName = "Ikit Claw",
						Age = 3,
						ImageUrl = "/images/ungor.jpg"
					},
					new Cat
					{
						Name = "Turnip",
						Color = "White",
						OwnerName = "Greger Walnöt",
						Age = 4,
						ImageUrl = "/images/turnip.jpg"
					},
					new Cat
					{
						Name = "Stripes",
						Color = "Black and White",
						OwnerName = "Dennis Hotdog",
						Age = 6,
						ImageUrl = "/images/stripes.jpg"
					}
				);
			context.SaveChanges();
		}
	}
}
