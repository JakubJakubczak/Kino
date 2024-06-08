using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kino.Models;

public partial class Sean
{
    public int IdSeans { get; set; }

    [Display(Name = "Termin rozpoczęcia")]
    public DateTime TerminRozpoczecia { get; set; }

    [Display(Name = "Termin zakończenia")]
    public DateTime TerminZakonczenia { get; set; }

    [Display(Name = "Wolne miejsca")]
    public int WolneMiejsca { get; set; }

    [Display(Name = "Film")]
    public int FilmIdFilm { get; set; }

    [Display(Name = "Numer sali")]
    public int SalaNumerSali { get; set; }

    public virtual Film? FilmIdFilmNavigation { get; set; } = null!;

    public virtual ICollection<Bilet> Bilets { get; set; } = new List<Bilet>();
}
