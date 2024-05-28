//using Kino.Data;
//using Kino.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Kino.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly KinoContext _context;

//        public AccountController(KinoContext context,
//                UserManager<ApplicationUser> userManager,
//                SignInManager<ApplicationUser> signInManager,
//                RoleManager<IdentityRole> roleManager)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _roleManager = roleManager;
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new ApplicationUser
//                {
//                    Imie = model.Imie,
//                    Nazwisko = model.Nazwisko,
//                };

//                Save the user using UserManager
//               var result = await _userManager.CreateAsync(user, model.Haslo);
//                if (result.Succeeded)
//                {
//                    Ensure roles exist
//                    if (!await _roleManager.RoleExistsAsync("Klient"))
//                    {
//                        await _roleManager.CreateAsync(new IdentityRole("Klient"));
//                    }
//                    if (!await _roleManager.RoleExistsAsync("Pracownik"))
//                    {
//                        await _roleManager.CreateAsync(new IdentityRole("Pracownik"));
//                    }

//                    Assign the user to the "Klient" role
//                   await _userManager.AddToRoleAsync(user, "Klient");

//                    Save additional user information to Klient table
//                   var klient = new Klient
//                   {
//                       Imie = model.Imie,
//                       Nazwisko = model.Nazwisko,
//                       Login = model.Login,
//                       Haslo = model.Haslo // Store hashed password if necessary
//                   };

//                    _context.Klients.Add(klient);
//                    await _context.SaveChangesAsync();

//                    Optionally sign in the user
//                    await _signInManager.SignInAsync(user, isPersistent: false);

//                    return RedirectToAction("Index", "Home");
//                }

//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//            }

//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var pracownik = await _context.Pracowniks.FirstOrDefaultAsync(p => p.KlientLoginNavigation.Login == model.Login && p.KlientLoginNavigation.Haslo == model.Haslo);
//                if (pracownik != null)
//                {
//                    Handle successful login for Pracownik
//                    return RedirectToAction("Index", "Home");
//                }

//                var klient = await _context.Klients.FirstOrDefaultAsync(p => p.Login == model.Login && p.Haslo == model.Haslo);
//                if (klient != null)
//                {
//                    Handle successful login for Klient
//                    return RedirectToAction("Index", "Home");
//                }

//                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//            }

//            return View(model);
//        }
//    }
//}
