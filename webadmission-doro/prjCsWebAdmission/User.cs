using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace prjCsWebAdmission
{
    public class User
    {
        [Required(ErrorMessage = "Le code utilisateur est requis.")]
        [StringLength(11, MinimumLength = 7, ErrorMessage = "Le code utilisateur doit être entre 7 et 11 caractères.")]
        public string UserCode { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; }

        [Compare("Email", ErrorMessage = "Les deux e-mails ne correspondent pas.")]
        public string EmailConfirm { get; set; }
    }
}