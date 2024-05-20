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
    public class BiletsController : Controller
    {
        private readonly KinoContext _context;

        public BiletsController(KinoContext context)
        {
            _context = context;
        }

        // GET: Bilets
        public async Task<IActionResult> Index()
        {
            var kinoContext = _context.Bilets.Include(b => b.KlientLoginNavigation);
            return View(await kinoContext.ToListAsync());
        }

        // GET: Bilets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilet = await _context.Bilets
                .Include(b => b.KlientLoginNavigation)
                .FirstOrDefaultAsync(m => m.IdBilet == id);
            if (bilet == null)
            {
                return NotFound();
            }

            return View(bilet);
        }

        // GET: Bilets/Create
        public IActionResult Create()
        {
            ViewData["KlientLogin"] = new SelectList(_context.Klients, "Login", "Login");
            return View();
        }

        // POST: Bilets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBilet,ZarezerwowaneMiejsca,SeansIdSeans,KlientLogin")] Bilet bilet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bilet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlientLogin"] = new SelectList(_context.Klients, "Login", "Login", bilet.KlientLogin);
            return View(bilet);
        }

        // GET: Bilets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilet = await _context.Bilets.FindAsync(id);
            if (bilet == null)
            {
                return NotFound();
            }
            ViewData["KlientLogin"] = new SelectList(_context.Klients, "Login", "Login", bilet.KlientLogin);
            return View(bilet);
        }

        // POST: Bilets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBilet,ZarezerwowaneMiejsca,SeansIdSeans,KlientLogin")] Bilet bilet)
        {
            if (id != bilet.IdBilet)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bilet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiletExists(bilet.IdBilet))
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
            ViewData["KlientLogin"] = new SelectList(_context.Klients, "Login", "Login", bilet.KlientLogin);
            return View(bilet);
        }

        // GET: Bilets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bilet = await _context.Bilets
                .Include(b => b.KlientLoginNavigation)
                .FirstOrDefaultAsync(m => m.IdBilet == id);
            if (bilet == null)
            {
                return NotFound();
            }

            return View(bilet);
        }

        // POST: Bilets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bilet = await _context.Bilets.FindAsync(id);
            if (bilet != null)
            {
                _context.Bilets.Remove(bilet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BiletExists(int id)
        {
            return _context.Bilets.Any(e => e.IdBilet == id);
        }
    }
}
