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
    public class Doctor_PController : ApiController
    {
        HealthCareDBEntities1 hd = new HealthCareDBEntities1();
        public int Doctor_id;
        public string DoctorFullName;
        public string Gender;
        public string DOB;
        public string Mobile;
        public string Email;

        //[Authorize]
        [HttpGet]
        public IHttpActionResult getdoctor_pdetails()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<Doctor_PClass> docobj = hd.displayDoctor_P().Select(x => new Doctor_PClass()
            {
                Doctor_id = x.Doctor_id,
                DoctorFullName = x.DoctorFullName,
                Gender = x.Gender,
                DOB = x.DOB,
                Mobile = x.Mobile,
                Email = x.Email
            }).ToList<Doctor_PClass>();
            return Ok(docobj);
        }
        [HttpPost]
        public IHttpActionResult addDoctor(Doctor_PClass d)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            hd.AddDoctor_P(d.DoctorFullName,d.ProfilePhoto, d.Gender, d.DOB, d.Mobile, d.Email);
            hd.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Put(Doctor_PClass dc)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var ud = hd.Doctor_P.Where(x => x.Doctor_id == dc.Doctor_id).FirstOrDefault<Doctor_P>();
            if (ud != null)
            {
                ud.Doctor_id = dc.Doctor_id;
                ud.DoctorFullName = dc.DoctorFullName;
                ud.Gender = dc.Gender;
                ud.DOB = dc.DOB;
                ud.Mobile = dc.Mobile;
                ud.Email = dc.Email;
                hd.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }


        public IHttpActionResult Getdoctordetails(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            Doctor_PClass doctordetails = null;
            doctordetails = hd.Doctor_P.Where(x => x.Doctor_id == id).Select(x => new Doctor_PClass()
            {
                Doctor_id = x.Doctor_id,
                DoctorFullName = x.DoctorFullName,
                ProfilePhoto = x.ProfilePhoto,
                Gender = x.Gender,
                DOB = x.DOB,
                Mobile = x.Mobile,
                Email = x.Email

            }).FirstOrDefault<Doctor_PClass>();
            if (doctordetails == null)
            {
                return NotFound();
            }
            return Ok(doctordetails);
        }



        public IHttpActionResult Delete(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var Docdel = hd.Doctor_P.Where(x => x.Doctor_id == id).FirstOrDefault();
            hd.Entry(Docdel).State = EntityState.Deleted;
            hd.SaveChanges();
            return Ok();
        }

    }
}
