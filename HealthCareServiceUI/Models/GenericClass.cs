using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class GenericClass
    {
        [Key]
        public int Generic_id { get; set; }
        [Required(ErrorMessage = "Please Enter The Generic Name")]
        [Display(Name = "Generic Name")]
        public string GenericName { get; set; }
    }
}