using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthCareServiceUI.Models;

namespace HealthCareServiceUI.Controllers
{
    [Authorize]
    public class PresciptionJoinTablesController : ApiController
    {
        public IHttpActionResult getjointables()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<PresciptionJoinTablesClass> pjt = hd.PresciptionJoinTables().Select(x => new PresciptionJoinTablesClass()
            {
                PatientFullName = x.PatientFullName,
                Gender = x.Gender,
                Age = x.Age,
                DoctorFullName = x.DoctorFullName,
                Date = x.Date,

                BP = x.BP,
                Temp = x.Temp,
                Weight = x.Weight,
                Indication = x.Indication,
                Type = x.Type,

                Name = x.Name,
                Strength = x.Strength,
                Doze = x.Doze,
                Duration = x.Duration,
                Visit_no = x.Visit_no,
                NextDate = x.NextDate


            }).ToList();
            return Ok(pjt);
        }
    }
}
