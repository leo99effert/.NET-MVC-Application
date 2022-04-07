#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NextHome.Data;
using NextHome.Models;
using System.Security.Claims;

namespace NextHome.Controllers
{
    [Authorize(Roles = "Admin, Realtor")]
    public class EstatesController : Controller
    {
        private readonly NextHomeContext _context;

        public EstatesController(NextHomeContext context)
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            estates = estates.Where(x => x.RealtorId == userId);

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

        // GET: Estates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address,Price,Description,TypeOfEstate,TypeOfOwnership,NrOfRooms,Size,Year,ViewDate,RealtorId")] Estate estate)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                estate.RealtorId = userId;
                _context.Add(estate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estate);
        }

        // GET: Estates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estate = await _context.Estate.FindAsync(id);
            if (estate == null)
            {
                return NotFound();
            }
            return View(estate);
        }

        // POST: Estates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,Price,Description,TypeOfEstate,TypeOfOwnership,NrOfRooms,Size,Year,ViewDate,RealtorId")] Estate estate)
        {
            if (id != estate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    estate.RealtorId = userId;
                    _context.Update(estate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstateExists(estate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(estate);
        }

        // GET: Estates/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Estates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estate = await _context.Estate.FindAsync(id);
            _context.Estate.Remove(estate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstateExists(int id)
        {
            return _context.Estate.Any(e => e.Id == id);
        }
    }
}
