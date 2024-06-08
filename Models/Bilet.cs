using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Kino.Models;

public partial class Bilet
{
    public int IdBilet { get; set; }
    [Display(Name = "Zarezerwowane miejsca")]
    public int ZarezerwowaneMiejsca { get; set; }

    [Display(Name = "Seans Id")]
    public int SeansIdSeans { get; set; }
    [Display(Name = "Login klienta")]
    public string KlientLogin { get; set; } = null!;

    [Display(Name = "Login klienta")]
    public virtual Klient? KlientLoginNavigation { get; set; } = null!;
    public virtual Sean? SeansIdSeansNavigation { get; set; } = null!;
}
