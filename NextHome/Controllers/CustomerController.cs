#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NextHome.Data;
using NextHome.Models;

namespace NextHome.Controllers
{
    public class CustomerController : Controller
    {
        private readonly NextHomeContext _context;

        public CustomerController(NextHomeContext context)
        {
            _context = context;
        }

        // GET: Estates
        public async Task<IActionResult> Index(string selectedType, string searchString, int minRooms, int minSize, int maxPrice = 2000000000)
        {
            IQueryable<string> filterQuery = from m in _context.Estate
                                             orderby m.TypeOfEstate
                                             select m.TypeOfEstate;
            var estates = from m in _context.Estate
                          select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                estates = estates.Where(s => s.Address!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(selectedType))
            {
                estates = estates.Where(x => x.TypeOfEstate == selectedType);
            }

            estates = estates.Where(x => x.NrOfRooms >= minRooms);
            estates = estates.Where(x => x.Size >= minSize);
            estates = estates.Where(x => x.Price <= maxPrice);

            var filteredEstates = new EstateFilter
            {
                TypesOfEstates = new SelectList(await filterQuery.Distinct().ToListAsync()),
                Estates = await estates.ToListAsync()
            };

            return View(filteredEstates);
        }

        // GET: Estates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estate = await _context.Estate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estate == null)
            {
                return NotFound();
            }

            return View(estate);
        }
    }
}
