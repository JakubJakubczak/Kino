﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Kino.Data;
using Kino.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Kino.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly KinoContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeletePersonalDataModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            KinoContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
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

            public IEnumerable<string> AllRoles { get; set; }
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
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

            var user = await _userManager.FindByIdAsync(Input.Login);
            if (user == null)
            {
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
