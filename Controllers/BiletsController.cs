using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kino.Data;
using Kino.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MySqlX.XDevAPI;

namespace Kino.Controllers
{
   
    public class BiletsController : Controller
    {
        private readonly KinoContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BiletsController(KinoContext context,
                                UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bilets
        [Authorize(Roles = "Pracownik")]
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
        [Authorize(Roles = "Pracownik")]
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
        [Authorize(Roles = "Pracownik")]
        public async Task<IActionResult> Create([Bind("IdBilet,ZarezerwowaneMiejsca,SeansIdSeans,KlientLogin")] Bilet bilet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bilet);
                var seansId = bilet.SeansIdSeans;
                var zarezerwowaneMiejsca = bilet.ZarezerwowaneMiejsca;

                var seans = _context.Seans.FirstOrDefault(s => s.IdSeans == seansId);
                var wolneMiejsca = seans.WolneMiejsca;
                if(wolneMiejsca < zarezerwowaneMiejsca) 
                {
                    ModelState.AddModelError("", "Nie ma tylu wolnych miejsc");
                    return View(bilet);
                }
                else
                {
                    seans.WolneMiejsca = seans.WolneMiejsca - zarezerwowaneMiejsca;
                }
                await _context.SaveChangesAsync();
                // Redirect based on user role
                if (User.IsInRole("Pracownik"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (User.IsInRole("Klient"))
                {
                    return RedirectToAction(nameof(Rezerwacje));
                }
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
            ViewData["SeansIdSeans"] = new SelectList(_context.Seans, "IdSeans", "IdSeans", bilet.SeansIdSeans);
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
                    var biletPrzed = await _context.Bilets.FindAsync(id);
                    var zarezerwowaneMiejscaPrzed = biletPrzed.ZarezerwowaneMiejsca;
                    _context.Entry(biletPrzed).State = EntityState.Detached;
                    
                    var seansId = bilet.SeansIdSeans;
                    var zarezerwowaneMiejsca = bilet.ZarezerwowaneMiejsca;
                       
                    var seans = _context.Seans.FirstOrDefault(s => s.IdSeans == seansId);
                    var wolneMiejsca = seans.WolneMiejsca;
                    if (wolneMiejsca < zarezerwowaneMiejsca)
                    {
                        ModelState.AddModelError("", "Nie ma tylu wolnych miejsc");
                        return View(bilet);
                    }
                    else
                    {
                        seans.WolneMiejsca = seans.WolneMiejsca + zarezerwowaneMiejscaPrzed;
                        seans.WolneMiejsca = seans.WolneMiejsca - zarezerwowaneMiejsca;
                    }
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
                if (User.IsInRole("Pracownik"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (User.IsInRole("Klient"))
                {
                    return RedirectToAction(nameof(Rezerwacje));
                }
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
            var seansId = bilet.SeansIdSeans;
            var seans = _context.Seans.FirstOrDefault(s => s.IdSeans == seansId);
            if (bilet != null)
            {
                _context.Bilets.Remove(bilet);
                seans.WolneMiejsca = seans.WolneMiejsca + bilet.ZarezerwowaneMiejsca;

            }

            await _context.SaveChangesAsync();

            //var user = await _userManager.GetUserAsync(User);
            //var userRole = await _userManager.GetRolesAsync(user);


            if (User.IsInRole("Pracownik"))
            {
                return RedirectToAction(nameof(Index));
            }
            else if (User.IsInRole("Klient"))
            {
                return RedirectToAction(nameof(Rezerwacje));
            }

            return View(bilet);
        }

        public async Task<IActionResult> DeleteForUser(int? id)
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

            return View("Delete", bilet);
        }

        // POST: Bilets/Delete/5
        [HttpPost, ActionName("DeleteForUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedForUser(int id)
        {
            var bilet = await _context.Bilets.FindAsync(id);
            var seansId = bilet.SeansIdSeans;
            var seans = _context.Seans.FirstOrDefault(s => s.IdSeans == seansId);
            if (bilet != null)
            {
                _context.Bilets.Remove(bilet);
                seans.WolneMiejsca = seans.WolneMiejsca + bilet.ZarezerwowaneMiejsca;

            }

            await _context.SaveChangesAsync();

            //var user = await _userManager.GetUserAsync(User);
            //var userRole = await _userManager.GetRolesAsync(user);



            return RedirectToAction(nameof(Rezerwacje));
        }

        private bool BiletExists(int id)
        {
            return _context.Bilets.Any(e => e.IdBilet == id);
        }
        [Authorize(Roles = "Klient, Pracownik")]
        public async Task<IActionResult> Rezerwacje()
        {
            var user = await _userManager.GetUserAsync(User);
            var login = user.UserName;

            var kinoContext = await _context.Bilets.Where(b => b.KlientLogin == login)
                                             .Include(b => b.SeansIdSeansNavigation)
                                               .ThenInclude(s => s.FilmIdFilmNavigation)
                                             .ToListAsync();


            return View(kinoContext);
        }


        [HttpPost]
        public async Task<IActionResult> RezerwujForm(int SeansId, int LiczbaBiletow)
        {
            var seans = await _context.Seans.Include(s=> s.FilmIdFilmNavigation)
                                            .FirstOrDefaultAsync(m => m.IdSeans == SeansId);

            var user = await _userManager.GetUserAsync(User);

            if (seans == null)
            {
                return NotFound();
            }

            if (user == null)
            {
                return NotFound();
            }

            var seansId = seans.IdSeans;
            var login = user.UserName;

            var bilet = new Bilet
            {
                ZarezerwowaneMiejsca = LiczbaBiletow,
                SeansIdSeans = seansId,
                KlientLogin = login
                
            };


            // sprawdzenie czy ilosc biletow nie przekracza liczby dostepnych miejsc
            var wolneMiejsca = seans.WolneMiejsca;
            if (wolneMiejsca < LiczbaBiletow)
            {
                TempData["ZbytWieleBiletow"] = "Nie ma tylu biletów";
                return View(seans);
            }
            else
            {
                seans.WolneMiejsca = seans.WolneMiejsca - LiczbaBiletow;
            }
            _context.Bilets.Add(bilet);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Rezerwacje));
        }
    }
}
