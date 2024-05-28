using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Kino.Models;

public partial class Klient{
    public string Login { get; set; } = null!;

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string Haslo { get; set; } = null!;

    public virtual ICollection<Bilet> Bilets { get; set; } = new List<Bilet>();

    public virtual ICollection<Karnet> Karnets { get; set; } = new List<Karnet>();

    public virtual ICollection<Pracownik> Pracowniks { get; set; } = new List<Pracownik>();
}
