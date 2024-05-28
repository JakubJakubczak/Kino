using Microsoft.AspNetCore.Identity;

namespace Kino.Areas.Identity.Pages.Account;
public class ApplicationUser : IdentityUser
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
}

