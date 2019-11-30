using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCareServiceUI.Models
{
    public class PresciptionJoinTablesClass
    {
        public string PatientFullName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string DoctorFullName { get; set; }
        public DateTime Date { get; set; }

        public string BP { get; set; }
        public string Temp { get; set; }
        public string Weight { get; set; }
        public string Indication { get; set; }
        public string Type { get; set; }

        public string Name { get; set; }
        public string Strength { get; set; }
        public string Doze { get; set; }
        public string Duration { get; set; }
        public int Visit_no { get; set; }
        public DateTime NextDate { get; set; }
    }
}