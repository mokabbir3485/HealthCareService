using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class PresciptionModel
    {
        [Key]
        public int Prescip_id { get; set; }
        [Required(ErrorMessage = "Please Enter The Date")]
        [Display(Name = "Date")]
        public System.DateTime Date { get; set; }
        [Required(ErrorMessage = "Please Enter The Blood Pressure")]
        [Display(Name = "Blood Pressure")]
        public string BP { get; set; }
        [Required(ErrorMessage = "Please Enter The Temperature")]
        [Display(Name = "Temperature(*F)")]
        public string Temp { get; set; }
        [Required(ErrorMessage = "Please Enter The Weight")]
        [Display(Name = "Weight")]
        public string Weight { get; set; }
        [Display(Name = "Number of Visit")]
        public Nullable<int> Visit_no { get; set; }
        [Display(Name = "Next Date")]
        public Nullable<System.DateTime> NextDate { get; set; }
        [Required(ErrorMessage = "Please Enter The Indication")]
        [Display(Name = "Indication")]
        public string Indication { get; set; }

        public int Patient_id { get; set; }
        [Display(Name = "Patient Full Name")]
        public string PatientFullName { get; set; }

        public int Doctor_id { get; set; }
        [Display(Name = "Doctor Full Name")]
        public string DoctorFullName { get; set; }
    }
}