#nullable disable


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kino.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kino.Areas.Identity.Pages.Account;
using Kino.Data;
using System.ComponentModel.DataAnnotations;

namespace Kino.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Pracownik")]
    public class ChangeRoleModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly KinoContext _context;

        public ChangeRoleModel(UserManager<IdentityUser> userManager,
                               RoleManager<IdentityRole> roleManager,
                               KinoContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public IEnumerable<string> AllRoles { get; set; }

        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [Display(Name = "Login")]
            public string Login { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [Display(Name = "Nowa rola")]
            public string Role { get; set; }

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            //Input = new InputModel
            //{
            //    Login = user.UserName,
            //    AllRoles = allRoles
            //};
            //var userRoles = await _userManager.GetRolesAsync(user);
            //var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            //Input = new ChangeRoleModel
            //{
            //    Login = user.Id,
            //    Role = 

            //    UserName = user.UserName,
            //    CurrentRoles = userRoles,
            //    AllRoles = allRoles
            //};

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.FindByNameAsync(Input.Login);
            if (user == null)
            {
                ModelState.AddModelError("", "Failed to remove use");
                return NotFound();
               
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (!removeRolesResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles.");
                return Page();
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, Input.Role);
            if (!addRoleResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user to new role.");
                return Page();
            }

            if (Input.Role == "Pracownik") 
            {
                var pracownik = _context.Pracowniks.FirstOrDefault(s => s.KlientLogin == Input.Login);
                if (pracownik == null) // Nie ma pracownika, wiec dodajemy go
                {
                    var pracownikDodawany = new Pracownik
                    {
                        KlientLogin = Input.Login
                    };
                    _context.Pracowniks.Add(pracownikDodawany);
                }
            }
            else // jezeli chcemy zmienic na klienta
            {
                var pracownik = _context.Pracowniks.FirstOrDefault(s => s.KlientLogin == Input.Login);
                if (pracownik != null) // Pracownik jest, wiec go usuniemy
                {
                    _context.Pracowniks.Remove(pracownik);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
