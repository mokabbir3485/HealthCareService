using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HealthCareServiceUI.Models
{
    public class Doctor_PClass
    {
        [Key]
        public int Doctor_id { get; set; }
        [Required(ErrorMessage ="Please Enter The Name")]
        [Display(Name = "Doctor Full Name")]
        public string DoctorFullName { get; set; }
        [Display(Name = "Profile Photo")]
        public byte[] ProfilePhoto { get; set; }
        [Required(ErrorMessage = "Please Select The Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Enter The Date Of Birth")]
        [Display(Name = "Date Of Birth")]
        public System.DateTime DOB { get; set; }
        [Required(ErrorMessage = "Please Enter The Mobile")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please Enter The Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}