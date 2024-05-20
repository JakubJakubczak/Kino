using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kino.Data;
using Kino.Models;

namespace Kino.Controllers
{
    public class SeansController : Controller
    {
        private readonly KinoContext _context;

        public SeansController(KinoContext context)
        {
            _context = context;
        }

        // GET: Seans
        public async Task<IActionResult> Index()
        {
            var kinoContext = _context.Seans.Include(s => s.FilmIdFilmNavigation);
            return View(await kinoContext.ToListAsync());
        }

        // GET: Seans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sean = await _context.Seans
                .Include(s => s.FilmIdFilmNavigation)
                .FirstOrDefaultAsync(m => m.IdSeans == id);
            if (sean == null)
            {
                return NotFound();
            }

            return View(sean);
        }

        // GET: Seans/Create
        public IActionResult Create()
        {
            ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "IdFilm");
            return View();
        }

        // POST: Seans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSeans,TerminRozpoczecia,TerminZakonczenia,WolneMiejsca,FilmIdFilm,SalaNumerSali")] Sean sean)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sean);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "IdFilm", sean.FilmIdFilm);
            return View(sean);
        }

        // GET: Seans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sean = await _context.Seans.FindAsync(id);
            if (sean == null)
            {
                return NotFound();
            }
            ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "IdFilm", sean.FilmIdFilm);
            return View(sean);
        }

        // POST: Seans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSeans,TerminRozpoczecia,TerminZakonczenia,WolneMiejsca,FilmIdFilm,SalaNumerSali")] Sean sean)
        {
            if (id != sean.IdSeans)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sean);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeanExists(sean.IdSeans))
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
            ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "IdFilm", sean.FilmIdFilm);
            return View(sean);
        }

        // GET: Seans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sean = await _context.Seans
                .Include(s => s.FilmIdFilmNavigation)
                .FirstOrDefaultAsync(m => m.IdSeans == id);
            if (sean == null)
            {
                return NotFound();
            }

            return View(sean);
        }

        // POST: Seans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sean = await _context.Seans.FindAsync(id);
            if (sean != null)
            {
                _context.Seans.Remove(sean);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeanExists(int id)
        {
            return _context.Seans.Any(e => e.IdSeans == id);
        }
    }
}
