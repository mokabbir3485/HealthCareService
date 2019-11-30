using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class BrandClass
    {

        [Key]
        public int Brand_id { get; set; }
        [Required(ErrorMessage = "Please Enter The Brand Name")]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}