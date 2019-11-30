using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthCareServiceUI.Models;

namespace HealthCareServiceUI.Controllers
{
    public class DoctorJoinTablesController : ApiController
    {
        public IHttpActionResult getjointables()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<DoctorJoinTablesClass> djt = hd.DoctorJoinTables().Select(x => new DoctorJoinTablesClass()
            {
                DoctorFullName = x.DoctorFullName,
                Specialization = x.Specialization,
                InstituteName = x.InstituteName,
                Degree = x.Degree,
                PassingYear = x.PassingYear,
                Mobile = x.Mobile

            }).ToList();
            return Ok(djt);
        }
    }
}
