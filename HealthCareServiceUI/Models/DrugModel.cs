using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class DrugModel
    {
        [Key]
        public int Drug_id { get; set; }
        [Required(ErrorMessage = "Please Enter The Drug Name")]
        [Display(Name = "Drug Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter The Strength")]
        [Display(Name = "Strength")]
        public string Strength { get; set; }
        [Required(ErrorMessage = "Please Enter The Type")]
        [Display(Name = "Type")]
        public string Type { get; set; }

        public int Generic_id { get; set; }
        [Display(Name = "Generic Name")]
        public string GenericName { get; set; }

        public int Brand_id { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}