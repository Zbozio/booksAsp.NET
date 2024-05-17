using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bazowane.Models;

namespace Bazowane.Controllers
{
    public class OcenasController : Controller
    {
        private readonly BazowaneContext _context;

        public OcenasController(BazowaneContext context)
        {
            _context = context;
        }

        // GET: Ocenas
        public async Task<IActionResult> Index()
        {
            var bazowaneContext = _context.Ocenas.Include(o => o.IdKsiazkaNavigation).Include(o => o.IdUzytkownikNavigation);
            return View(await bazowaneContext.ToListAsync());
        }

        // GET: Ocenas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Ocenas == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocenas
                .Include(o => o.IdKsiazkaNavigation)
                .Include(o => o.IdUzytkownikNavigation)
                .FirstOrDefaultAsync(m => m.IdOceny == id);
            if (ocena == null)
            {
                return NotFound();
            }

            return View(ocena);
        }

        // GET: Ocenas/Create
        public IActionResult Create()
        {
            ViewData["IdKsiazka"] = new SelectList(_context.Ksiazkas, "IdKsiazka", "Tytul");
            ViewData["IdUzytkownik"] = new SelectList(_context.Uzytkowniks, "IdUzytkownik", "Imie");
            return View();
        }

        // POST: Ocenas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOceny,IdKsiazka,IdUzytkownik,Ocena1,Opinia,DataOceny")] Ocena ocena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKsiazka"] = new SelectList(_context.Ksiazkas, "IdKsiazka", "IdKsiazka", ocena.IdKsiazka);
            ViewData["IdUzytkownik"] = new SelectList(_context.Uzytkowniks, "IdUzytkownik", "IdUzytkownik", ocena.IdUzytkownik);
            return View(ocena);
        }

        // GET: Ocenas/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Ocenas == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocenas.FindAsync(id);
            if (ocena == null)
            {
                return NotFound();
            }
            ViewData["IdKsiazka"] = new SelectList(_context.Ksiazkas, "IdKsiazka", "IdKsiazka", ocena.IdKsiazka);
            ViewData["IdUzytkownik"] = new SelectList(_context.Uzytkowniks, "IdUzytkownik", "IdUzytkownik", ocena.IdUzytkownik);
            return View(ocena);
        }

        // POST: Ocenas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdOceny,IdKsiazka,IdUzytkownik,Ocena1,Opinia,DataOceny")] Ocena ocena)
        {
            if (id != ocena.IdOceny)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcenaExists(ocena.IdOceny))
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
            ViewData["IdKsiazka"] = new SelectList(_context.Ksiazkas, "IdKsiazka", "IdKsiazka", ocena.IdKsiazka);
            ViewData["IdUzytkownik"] = new SelectList(_context.Uzytkowniks, "IdUzytkownik", "IdUzytkownik", ocena.IdUzytkownik);
            return View(ocena);
        }

        // GET: Ocenas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Ocenas == null)
            {
                return NotFound();
            }

            var ocena = await _context.Ocenas
                .Include(o => o.IdKsiazkaNavigation)
                .Include(o => o.IdUzytkownikNavigation)
                .FirstOrDefaultAsync(m => m.IdOceny == id);
            if (ocena == null)
            {
                return NotFound();
            }

            return View(ocena);
        }

        // POST: Ocenas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Ocenas == null)
            {
                return Problem("Entity set 'BazowaneContext.Ocenas'  is null.");
            }
            var ocena = await _context.Ocenas.FindAsync(id);
            if (ocena != null)
            {
                _context.Ocenas.Remove(ocena);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OcenaExists(decimal id)
        {
          return (_context.Ocenas?.Any(e => e.IdOceny == id)).GetValueOrDefault();
        }
    }
}
