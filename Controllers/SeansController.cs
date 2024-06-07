using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kino.Data;
using Kino.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;

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
            ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "Tytul");
            ViewData["SalaNumerSali"] = new SelectList(_context.Salas, "NumerSali", "NumerSali"); // Assuming "NumerSali" is the property you want to display
            return View();
        }

        // POST: Seans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TerminRozpoczecia,TerminZakonczenia,FilmIdFilm,SalaNumerSali")] Sean sean)
        {
            if (ModelState.IsValid)
            {
                var sala = _context.Salas.FirstOrDefault(s => s.NumerSali == sean.SalaNumerSali);
                var film = await _context.Films.FindAsync(sean.FilmIdFilm);
                if (sala != null)
                {
                    // Check if the room is available
                    bool isRoomAvailable = !_context.Seans.Any(s =>
                        s.SalaNumerSali == sean.SalaNumerSali &&
                        ((sean.TerminRozpoczecia >= s.TerminRozpoczecia && sean.TerminRozpoczecia < s.TerminZakonczenia) ||
                         (sean.TerminZakonczenia > s.TerminRozpoczecia && sean.TerminZakonczenia <= s.TerminZakonczenia) ||
                         (sean.TerminRozpoczecia < s.TerminRozpoczecia && sean.TerminZakonczenia > s.TerminZakonczenia)));

                    if (!isRoomAvailable)
                    {
                        ModelState.AddModelError("", "W tym czasie wybrana sala jest niedostępna");
                        ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "Tytul", sean.FilmIdFilm);
                        ViewData["SalaNumerSali"] = new SelectList(_context.Salas, "NumerSali", "NumerSali", sean.SalaNumerSali);
                        return View(sean);
                    }
                    sean.WolneMiejsca = sala.LiczbaMiejsc;
                }
                if (film != null)
                {
                    sean.FilmIdFilmNavigation = film;
                }
                _context.Add(sean);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            // Inspect the validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            
            ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "Tytul", sean.FilmIdFilm);
            ViewData["SalaNumerSali"] = new SelectList(_context.Salas, "NumerSali", "NumerSali", sean.SalaNumerSali); // Assuming "NumerSali" is the property you want to display
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
            ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "Tytul", sean.FilmIdFilm);
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
                    var sala = _context.Salas.FirstOrDefault(s => s.NumerSali == sean.SalaNumerSali);
                    var film = await _context.Films.FindAsync(sean.FilmIdFilm);
                    if (sala != null)
                    {
                        // Check if the room is available
                        bool isRoomAvailable = !_context.Seans.Any(s =>
                            s.SalaNumerSali == sean.SalaNumerSali &&
                            ((sean.TerminRozpoczecia >= s.TerminRozpoczecia && sean.TerminRozpoczecia < s.TerminZakonczenia) ||
                             (sean.TerminZakonczenia > s.TerminRozpoczecia && sean.TerminZakonczenia <= s.TerminZakonczenia) ||
                             (sean.TerminRozpoczecia < s.TerminRozpoczecia && sean.TerminZakonczenia > s.TerminZakonczenia)));

                        if (!isRoomAvailable)
                        {
                            ModelState.AddModelError("", "W tym czasie wybrana sala jest niedostępna");
                            ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "Tytul", sean.FilmIdFilm);
                            ViewData["SalaNumerSali"] = new SelectList(_context.Salas, "NumerSali", "NumerSali", sean.SalaNumerSali);
                            return View(sean);
                        }
                        sean.WolneMiejsca = sala.LiczbaMiejsc;
                    }
                    if (film != null)
                    {
                        sean.FilmIdFilmNavigation = film;
                    }
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
            ViewData["FilmIdFilm"] = new SelectList(_context.Films, "IdFilm", "Tytul", sean.FilmIdFilm);
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
