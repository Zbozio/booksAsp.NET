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
    public class KsiazkasController : Controller
    {
        private readonly BazowaneContext _context;

        public KsiazkasController(BazowaneContext context)
        {
            _context = context;
        }




        // GET: Ksiazkas
        public async Task<IActionResult> Index()
        {
            var bazowaneContext = _context.Ksiazkas.Include(k => k.Ocenas).Include(k => k.IdGatunkus);


            return View(await bazowaneContext.ToListAsync());
        }

        // GET: Ksiazkas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Ksiazkas == null)
            {
                return NotFound();
            }

            var ksiazka = await _context.Ksiazkas
                .Include(k => k.IdStatusuNavigation)
                .FirstOrDefaultAsync(m => m.IdKsiazka == id);
            if (ksiazka == null)
            {
                return NotFound();
            }

            return View(ksiazka);
        }

        // GET: Ksiazkas/Create
        public IActionResult Create()
        {
            ViewData["IdStatusu"] = new SelectList(_context.Statuses, "IdStatusu", "IdStatusu");
            return View();
        }

        // POST: Ksiazkas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKsiazka,IdStatusu,Tytul,Opis,RokWydania,SredniaOcena,LiczbaOcen,LiczbaRecenzji,IloscStron")] Ksiazka ksiazka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ksiazka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStatusu"] = new SelectList(_context.Statuses, "IdStatusu", "IdStatusu", ksiazka.IdStatusu);
            return View(ksiazka);
        }

        // GET: Ksiazkas/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Ksiazkas == null)
            {
                return NotFound();
            }

            var ksiazka = await _context.Ksiazkas.FindAsync(id);
            if (ksiazka == null)
            {
                return NotFound();
            }
            ViewData["IdStatusu"] = new SelectList(_context.Statuses, "IdStatusu", "IdStatusu", ksiazka.IdStatusu);
            return View(ksiazka);
        }

        // POST: Ksiazkas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdKsiazka,IdStatusu,Tytul,Opis,RokWydania,SredniaOcena,LiczbaOcen,LiczbaRecenzji,IloscStron")] Ksiazka ksiazka)
        {
            if (id != ksiazka.IdKsiazka)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ksiazka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KsiazkaExists(ksiazka.IdKsiazka))
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
            ViewData["IdStatusu"] = new SelectList(_context.Statuses, "IdStatusu", "IdStatusu", ksiazka.IdStatusu);
            return View(ksiazka);
        }

        // GET: Ksiazkas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Ksiazkas == null)
            {
                return NotFound();
            }

            var ksiazka = await _context.Ksiazkas
                .Include(k => k.IdStatusuNavigation)
                .FirstOrDefaultAsync(m => m.IdKsiazka == id);
            if (ksiazka == null)
            {
                return NotFound();
            }

            return View(ksiazka);
        }

        // POST: Ksiazkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Ksiazkas == null)
            {
                return Problem("Entity set 'BazowaneContext.Ksiazkas'  is null.");
            }
            var ksiazka = await _context.Ksiazkas.FindAsync(id);
            if (ksiazka != null)
            {
                _context.Ksiazkas.Remove(ksiazka);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KsiazkaExists(decimal id)
        {
          return (_context.Ksiazkas?.Any(e => e.IdKsiazka == id)).GetValueOrDefault();
        }
    }
}
