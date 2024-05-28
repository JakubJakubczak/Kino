// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Kino.Data;
using Kino.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Kino.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly KinoContext _context;
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            KinoContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {

            [Required]
            [Display(Name = "Login")]
            public string Login { get; set; }

            [Required]
            [Display(Name = "Imie")]
            public string Imie { get; set; }

            [Required]
            [Display(Name = "Nazwisko")]
            public string Nazwisko { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Haslo")]
            public string Haslo { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potwierdz haslo")]
            [Compare("Haslo", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmHaslo { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Login,
                    Imie = Input.Imie,
                    Nazwisko = Input.Nazwisko
                };

                var result = await _userManager.CreateAsync(user, Input.Haslo);

                if (result.Succeeded)
                {
                    // Ensure roles exist
                    if (!await _roleManager.RoleExistsAsync("Klient"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Klient"));
                    }
                    if (!await _roleManager.RoleExistsAsync("Pracownik"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Pracownik"));
                    }


                    // Save additional user information to Klient table
                    var klient = new Klient{
                        Imie = Input.Imie,
                        Nazwisko = Input.Nazwisko,
                        Login = Input.Login,
                        Haslo = Input.Haslo // Store hashed password if necessary
                    };

                    _context.Klients.Add(klient);
                    // Assign the user to the "Klient" role
                    if (await _context.Pracowniks.AnyAsync())
                    {
                        await _userManager.AddToRoleAsync(user, "Klient");
                    }
                    else
                    {
                        var pracownik = new Pracownik
                        {

                        KlientLogin = Input.Login
                        };
                        _context.Pracowniks.Add(pracownik);
                        await _userManager.AddToRoleAsync(user, "Pracownik");
                    }
                    await _context.SaveChangesAsync();

                    // Optionally sign in the user
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

    }

    //private ApplicationUser CreateUser()
    //{
    //    try
    //    {
    //        return Activator.CreateInstance<ApplicationUser>();
    //    }
    //    catch
    //    {
    //        throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
    //            $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
    //            $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
    //    }
    //}

    //private IUserEmailStore<ApplicationUser> GetEmailStore()
    //{
    //    if (!_userManager.SupportsUserEmail)
    //    {
    //        throw new NotSupportedException("The default UI requires a user store with email support.");
    //    }
    //    return (IUserEmailStore<ApplicationUser>)_userStore;
    //}        
}
