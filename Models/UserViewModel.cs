using System;
using System.ComponentModel.DataAnnotations;


namespace bright.Models
{
    public class UserViewModel
    {
        [Display(Name = "Name:")]
        [Required(ErrorMessage= "Name is required")]
        public string name { get; set; }

        [Display(Name = "Alias:")]
        [Required(ErrorMessage= "Alias is required")]
        public string alias { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage= "Email is Required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage= "Password is required")]
        [MinLength(8, ErrorMessage="Password must be 8 characters or more")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        
        [Display(Name = "Confirm PW:")]
        [Required(ErrorMessage= "Please confirm your password")]
        [Compare("password", ErrorMessage="Password is not matching")]
        [DataType(DataType.Password)]
        public string confirm { get; set; }

    }
}