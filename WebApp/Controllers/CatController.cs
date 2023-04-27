using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Controllers
{
	public class CatController : Controller
	{
		private readonly WebAppContext _context;

		public CatController(WebAppContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return _context.Cats != null ?
						View(await _context.Cats.ToListAsync()) :
						Problem("Entity set 'WebAppContext.Cats'  is null.");
		}
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Cats == null)
			{
				return NotFound();
			}

			var cat = await _context.Cats
				.FirstOrDefaultAsync(m => m.Id == id);
			if (cat == null)
			{
				return NotFound();
			}

			return View(cat);
		}
	}
}
