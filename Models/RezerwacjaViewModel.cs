using System.ComponentModel.DataAnnotations;

namespace Kino.Models
{
    public class RezerwacjaViewModel
    {
        public int SeansId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wprowadzić poprawną liczbę biletów.")]
        public int LiczbaBiletow { get; set; }
    }
}
