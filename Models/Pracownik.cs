using System;
using System.Collections.Generic;

namespace Kino.Models;

public partial class Pracownik
{
    public string PracownikId { get; set; } = null!;

    public string KlientLogin { get; set; } = null!;

    public virtual Klient KlientLoginNavigation { get; set; } = null!;
}
