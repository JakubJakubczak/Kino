
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
}

