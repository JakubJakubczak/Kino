//using System.ComponentModel.DataAnnotations;

//namespace Kino.Models
//{
//    public class RegisterViewModel
//    {
//        public class RegisterViewModel
//        {
//            [Required]
//            [Display(Name = "Login")]
//            public string Login { get; set; }

//            [Required]
//            [Display(Name = "Imie")]
//            public string Imie { get; set; }

//            [Required]
//            [Display(Name = "Nazwisko")]
//            public string Nazwisko { get; set; }

//            [Required]
//            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
//            [DataType(DataType.Password)]
//            [Display(Name = "Haslo")]
//            public string Haslo { get; set; }

//            [DataType(DataType.Password)]
//            [Display(Name = "Potwierdz haslo")]
//            [Compare("Haslo", ErrorMessage = "The password and confirmation password do not match.")]
//            public string ConfirmHaslo { get; set; }
//        }
//    }
//}
//}
