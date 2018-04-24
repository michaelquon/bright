using System;
using System.ComponentModel.DataAnnotations;

namespace bright.Models
{
    public class BrightViewModel
    {
        [Required(ErrorMessage="This Field Cannot be left blank")]
        public string post { get; set; }

    }
}