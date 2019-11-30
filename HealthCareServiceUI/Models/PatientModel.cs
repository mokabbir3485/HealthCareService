using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class PatientModel
    {
       
        public int Patient_id { get; set; }
        [Display(Name = "Full Name")]
        public string PatientFullName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        [Display(Name = "Is Married")]
        public string IsMarried { get; set; }
        public string Mobile { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}