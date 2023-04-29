using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		WebAppContext _context;

		public ValuesController(WebAppContext context)
		{
			_context = context;
		}

		// GET: api/Values
		[HttpGet]
		public IEnumerable<Cat> Get()
		{
			//Return all cats
			try 
			{
				return _context.Cats;
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		// GET api/Values/5
		[HttpGet("{id}")]
		public Cat Get(int id)
		{
			//Return specific cat
			try 
			{
				foreach(Cat cat in _context.Cats)
				{
					if(cat.Id == id)
					{
						return cat;
					}
				}
			}
			catch(Exception ex)
			{
				
			}
			return null;
		}

		// POST api/Values
		[HttpPost]
		public ActionResult Post([FromBody] Cat cat)
		{
			//Add new cat
			if (cat == null)
				return BadRequest("Don't send a null cat");
			foreach (Cat catCheck in _context.Cats)
			{
				if (catCheck.Id == cat.Id)
				{
					return BadRequest("A cat with that id already exists.");
				}
			}
			try
			{
				_context.Cats.Add(cat); //Add(new Cat(etc))?
				return StatusCode(200);
			}
			catch (Exception ex)
			{
				return BadRequest("Couldn't add cat.");
			}
			return StatusCode(404);
		}

		// PUT api/Values/5
		[HttpPut("{id}")]
		public ActionResult Put(int id, [FromBody] Cat cat)
		{
			//Edit cat by Id
			if (cat == null)
				return BadRequest("Don't send a null cat");
			try
			{
				//_context.Cats.Find(x => x.Id == cat.Id); >-<
				foreach(Cat catCheck in _context.Cats)
				{
					if(cat.Id == catCheck.Id)
					{
						catCheck.Age = cat.Age;
						catCheck.Name = cat.Name;
						catCheck.OwnerName = cat.OwnerName;
						catCheck.Color= cat.Color;
						catCheck.ImageUrl = cat.ImageUrl;
						_context.SaveChanges();
						return StatusCode(200);
					}
				}
			}
			catch (Exception ex)
			{
				return BadRequest("No cat with that id.");
			}
			return StatusCode(404);
		}

		// DELETE api/Values/5
		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			//Delete specific cat
			//_context.Cats.Remove(_context.Cats.Find(x => x.Id == id));
			//Antagligen för att _context.Cats är en "brygga" till databasen och inte object förrens de hämtas.

			if(id <= 0)
				return BadRequest("Not a valid cat id.");
			try
			{
				foreach (Cat cat in _context.Cats)
				{
					if (cat.Id == id)
					{
						_context.Cats.Remove(cat);
						_context.SaveChanges();
						return StatusCode(200);
					}
				}
			}
			catch (Exception ex) 
			{
				return BadRequest("No cat with that id.");
			}
			return StatusCode(404);
		}
	}
}
