using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HealthCareServiceUI.Models;

namespace HealthCareServiceUI.Models
{
    public class DocEducationModel
    {
        [Key]
        public int Edu_id { get; set; }
        [Required(ErrorMessage = "Please Enter The Institute Name")]
        [Display(Name = "Institute Name")]
        public string InstituteName { get; set; }
        [Required(ErrorMessage = "Please Enter The Degree")]
        [Display(Name = "Degree")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "Please Enter The PassingYear")]
        [Display(Name = "Passing Year")]
        public string PassingYear { get; set; }
        public byte[] Certificate { get; set; }

        public int Doctor_id { get; set; }
        public string DoctorFullName { get; set; }
    }
}