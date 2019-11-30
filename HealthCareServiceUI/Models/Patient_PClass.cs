using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class Patient_PClass
    {
        [Key]
        public int Patient_id { get; set; }
        [Required(ErrorMessage = "Please Enter The Name")]
        [Display(Name = "Patient Full Name")]
        public string PatientFullName { get; set; }

        [Required(ErrorMessage = "Please Select The Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Enter The Age")]
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please Enter The Height")]
        [Display(Name = "Height")]
        public string Height { get; set; }
        [Required(ErrorMessage = "Please Enter The Weight")]
        [Display(Name = "Weight")]
        public string Weight { get; set; }
        [Required(ErrorMessage = "Please Select Is Married?")]
        [Display(Name = "Married Status")]
        public string IsMarried { get; set; }
        [Required(ErrorMessage = "Please Enter The Mobile")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}