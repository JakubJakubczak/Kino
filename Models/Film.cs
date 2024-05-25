using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kino.Models;

public partial class Film
{
    public int IdFilm { get; set; }

    public string Tytul { get; set; } = null!;

    [Display(Name = "Imie i nazwisko reżysera")]
    public string? ImieNazwiskoRezysera { get; set; }

    [Display(Name = "Rok wydania")]
    public string? RokWydania { get; set; }

    [Display(Name = "Kraj produkcji")]
    public string? KrajProdukcji { get; set; }

    public string? Opis { get; set; }

    [Display(Name = "Czas trwania(min)")]
    public int CzasTrwania { get; set; }

    public virtual ICollection<Sean> Seans { get; set; } = new List<Sean>();
}
