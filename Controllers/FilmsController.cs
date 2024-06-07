using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kino.Data;
using Kino.Models;
using Humanizer;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Authorization;

namespace Kino.Controllers
{
   
    public class FilmsController : Controller
    {
        private readonly KinoContext _context;

        public FilmsController(KinoContext context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            return View(await _context.Films.ToListAsync());
        }

        // GET: Films
        public async Task<IActionResult> IndexRepertuar()
        {
            return View(await _context.Films.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.IdFilm == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        public async Task<IActionResult> WyswietlSeanse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                 .Include(f => f.Seans)
                .FirstOrDefaultAsync(m => m.IdFilm == id);
            if (film == null)
            {
                return NotFound();
            }

           // Filter the seans to only include those with the matching FilmId
        // var seans = await _context.Seans
        //.Where(s => s.FilmIdFilm == id)
        //.ToListAsync();

        //    // Create a ViewModel to pass both Film and its related Seans
        //    var viewModel = new FilmSeansViewModel
        //    {
        //        Film = film,
        //        SeansList = seans
        //    };

            return View(film);
        }

        // GET: Films/Create
        [Authorize(Roles = "Pracownik")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pracownik")]
        public async Task<IActionResult> Create([Bind("IdFilm,Tytul,ImieNazwiskoRezysera,RokWydania,KrajProdukcji,Opis,CzasTrwania")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Edit/5
        [Authorize(Roles = "Pracownik")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pracownik")]
        public async Task<IActionResult> Edit(int id, [Bind("IdFilm,Tytul,ImieNazwiskoRezysera,RokWydania,KrajProdukcji,Opis,CzasTrwania")] Film film)
        {
            if (id != film.IdFilm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.IdFilm))
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
            return View(film);
        }

        // GET: Films/Delete/5
        [Authorize(Roles = "Pracownik")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.IdFilm == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pracownik")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.IdFilm == id);
        }
    }
}
