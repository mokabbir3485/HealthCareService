using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HealthCareServiceUI.Models;

namespace HealthCareServiceUI.Controllers
{
    [Authorize]
    public class Patient_PController : ApiController
    {
        public int Patient_id;
        public string PatientFullName;
        public string Gender;
        public string Age;
        public string Height;
        public string Weight;
        public string IsMarried;
        public string Mobile;
        public string Email;


        // GET: api/Patient_P
        //HealthCareDBEntities1 hd = new HealthCareDBEntities1();
        [Authorize]
        [HttpGet]
        public IHttpActionResult getpatient_pdetails()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<Patient_PClass> patobj = hd.displayPatient_P().Select(x => new Patient_PClass()
            {
                Patient_id = x.Patient_id,
                PatientFullName = x.PatientFullName,
                Gender = x.Gender,
                Age = x.Age,
                Height = x.Height,
                Weight = x.Weight,
                IsMarried = x.IsMarried,
                Mobile = x.Mobile,
                Email = x.Email
            }).ToList<Patient_PClass>();
            return Ok(patobj);
        }

        [HttpPost]
        public IHttpActionResult addPatient(Patient_P p)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            hd.AddPatient_P(p.PatientFullName, p.Gender, p.Age, p.Height, p.Weight, p.IsMarried, p.Mobile, p.Email);
            hd.SaveChanges();
            return Ok();
        }

        public IHttpActionResult PutPatient(Patient_PClass pc)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var up = hd.Patient_P.Where(x => x.Patient_id == pc.Patient_id).FirstOrDefault<Patient_P>();
            if(up != null)
            {
                up.Patient_id = pc.Patient_id;
                up.Gender = pc.Gender;
                up.Age = pc.Age;
                up.Height = pc.Height;
                up.Weight = pc.Weight;
                up.IsMarried = pc.IsMarried;
                up.Mobile = pc.Mobile;
                up.Email = pc.Email;
                hd.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        public IHttpActionResult GetPatientdetails(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            Patient_PClass Patientdetails = null;
            Patientdetails = hd.Patient_P.Where(x => x.Patient_id == id).Select(x => new Patient_PClass()
            {
                Patient_id = x.Patient_id,
                PatientFullName = x.PatientFullName,
                Gender = x.Gender,
                Age = x.Age,
                Height = x.Height,
                Weight = x.Weight,
                IsMarried = x.IsMarried,
                Mobile = x.Mobile,
                Email = x.Email

            }).FirstOrDefault<Patient_PClass>();
            if (Patientdetails == null)
            {
                return NotFound();
            }
            return Ok(Patientdetails);
        }

        public IHttpActionResult Delete(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var Patientdel = hd.Patient_P.Where(x => x.Patient_id == id).FirstOrDefault();
            hd.Entry(Patientdel).State = EntityState.Deleted;
            hd.SaveChanges();
            return Ok();
        }

    }
}
