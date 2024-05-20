using System;
using System.Collections.Generic;

namespace Kino.Models;

public partial class Karnet
{
    public int IdKarnet { get; set; }

    public int ZarezerwowaneMiejsca { get; set; }

    public string KlientLogin { get; set; } = null!;

    public virtual Klient KlientLoginNavigation { get; set; } = null!;
}
