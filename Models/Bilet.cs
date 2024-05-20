using System;
using System.Collections.Generic;

namespace Kino.Models;

public partial class Bilet
{
    public int IdBilet { get; set; }

    public int ZarezerwowaneMiejsca { get; set; }

    public int SeansIdSeans { get; set; }

    public string KlientLogin { get; set; } = null!;

    public virtual Klient KlientLoginNavigation { get; set; } = null!;
}
