using System;
using System.Collections.Generic;

namespace Kino.Models;

public partial class Film
{
    public int IdFilm { get; set; }

    public string Tytul { get; set; } = null!;

    public string? ImieNazwiskoRezysera { get; set; }

    public string? RokWydania { get; set; }

    public string? KrajProdukcji { get; set; }

    public string? Opis { get; set; }

    public int CzasTrwania { get; set; }

    public virtual ICollection<Sean> Seans { get; set; } = new List<Sean>();
}
