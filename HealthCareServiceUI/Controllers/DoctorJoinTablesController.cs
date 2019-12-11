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


        //public IHttpActionResult gettables()
        //{
        //    HealthCareDBEntities1 hd = new HealthCareDBEntities1();
        //    List<Doctor_P> lDoctor_P = hd.Doctor_P.ToList();
        //    List<DocEducation> lDocEducation = hd.DocEducations.ToList();
        //    List<ProfessionalDetail> lProfessionalDetail = hd.ProfessionalDetails.ToList();

        //    var query = from d in lDoctor_P
        //                join de in lDocEducation on d.Doctor_id equals de.Doctor_id into table1
        //                from de in table1.DefaultIfEmpty()
        //                join pd in lProfessionalDetail on de.Doctor_id equals pd.Doctor_id into table2
        //                from pd in table2.DefaultIfEmpty()
        //                select new DoctorJoinClass { getDoctor_P = d, getDocEducation = de, getProfessionalDetail = pd };

        //    return Ok(query);
        //}

    }
}
