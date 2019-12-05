using System;
using System.Collections.Generic;
using System.Data;
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
    public class DoctorNewController : ApiController
    {
        private HealthCareDBEntities1 db = new HealthCareDBEntities1();

        // GET: api/DoctorNew
        public IQueryable<DoctorNew> GetDoctorNews()
        {
            return db.DoctorNews;
        }

        // GET: api/DoctorNew/5
        [ResponseType(typeof(DoctorNew))]
        public IHttpActionResult GetDoctorNew(int id)
        {
            DoctorNew doctorNew = db.DoctorNews.Find(id);
            if (doctorNew == null)
            {
                return NotFound();
            }

            return Ok(doctorNew);
        }

        // PUT: api/DoctorNew/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDoctorNew(int id, DoctorNew doctorNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctorNew.Doctor_id)
            {
                return BadRequest();
            }

            db.Entry(doctorNew).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorNewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DoctorNew
        [ResponseType(typeof(DoctorNew))]
        public IHttpActionResult PostDoctorNew(DoctorNew doctorNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DoctorNews.Add(doctorNew);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = doctorNew.Doctor_id }, doctorNew);
        }

        // DELETE: api/DoctorNew/5
        [ResponseType(typeof(DoctorNew))]
        public IHttpActionResult DeleteDoctorNew(int id)
        {
            DoctorNew doctorNew = db.DoctorNews.Find(id);
            if (doctorNew == null)
            {
                return NotFound();
            }

            db.DoctorNews.Remove(doctorNew);
            db.SaveChanges();

            return Ok(doctorNew);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoctorNewExists(int id)
        {
            return db.DoctorNews.Count(e => e.Doctor_id == id) > 0;
        }
    }
}