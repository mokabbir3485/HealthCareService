using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class ProfessionalDetailModel
    {
        [Key]
        public int Pro_id { get; set; }
        [Required(ErrorMessage = "Please Enter The Specialization")]
        [Display(Name = "Specialization")]
        public string Specialization { get; set; }
        public int Doctor_id { get; set; }
        public string DoctorFullName { get; set; }
    }
}