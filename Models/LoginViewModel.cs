using System;
using System.ComponentModel.DataAnnotations;

namespace bright.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage="This Field Cannot be left blank")]
        [EmailAddress]
        public string logEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage="This Field Cannot be left blank")]
        [DataType(DataType.Password)]
        public string logPassword { get; set; }
    }
}