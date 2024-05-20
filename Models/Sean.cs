using System;
using System.Collections.Generic;

namespace Kino.Models;

public partial class Sean
{
    public int IdSeans { get; set; }

    public DateTime TerminRozpoczecia { get; set; }

    public DateTime TerminZakonczenia { get; set; }

    public int WolneMiejsca { get; set; }

    public int FilmIdFilm { get; set; }

    public int SalaNumerSali { get; set; }

    public virtual Film FilmIdFilmNavigation { get; set; } = null!;
}
