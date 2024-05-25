using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kino.Models;

public partial class Sala
{
    [Display(Name = "Numer sali")]
    public int NumerSali { get; set; }

    [Display(Name = "Liczba miejsc")]
    public int LiczbaMiejsc { get; set; }
}
