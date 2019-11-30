using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class DoctorJoinTablesClass
    {
        public int Doctor_id { get; set; }
        public string DoctorFullName { get; set; }
        public string Specialization { get; set; }
        public string InstituteName { get; set; }
        public string Degree { get; set; }
        public string PassingYear { get; set; }
        public string Mobile { get; set; }
    }
}