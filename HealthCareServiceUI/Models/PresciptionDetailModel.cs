using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class PresciptionDetailModel
    {
        [Key]
        public int PresDetail_id { get; set; }
        [Required(ErrorMessage = "Please Enter The Doze")]
        [Display(Name = "Doze")]
        public string Doze { get; set; }
        [Required(ErrorMessage = "Please Enter The Duration")]
        [Display(Name = "Duration")]
        public string Duration { get; set; }
        
        [Display(Name = "Presciption ID")]
        public int Prescip_id { get; set; }
        public string Indication { get; set; }
        public string PatientFullName { get; set; }
        public int Drug_id { get; set; }
        public string Name { get; set; }


    }
}