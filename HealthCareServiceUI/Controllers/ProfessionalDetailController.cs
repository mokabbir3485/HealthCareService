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
    [Authorize]
    public class ProfessionalDetailController : ApiController
    {
        private HealthCareDBEntities1 db = new HealthCareDBEntities1();

        [HttpGet]
        public IHttpActionResult getProfessionalDetails()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<ProfessionalDetailModel> proDobj = hd.displayProfessionalDetail().Select(x => new ProfessionalDetailModel()
            {
                Pro_id = x.Pro_id,
                Specialization = x.Specialization,
                Doctor_id = x.Doctor_id
                
            }).ToList<ProfessionalDetailModel>();
            return Ok(proDobj);
        }

        public IHttpActionResult Put(ProfessionalDetailModel pdm)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var updm = hd.ProfessionalDetails.Where(x => x.Pro_id == pdm.Pro_id).FirstOrDefault<ProfessionalDetail>();
            if (updm != null)
            {
                updm.Pro_id = pdm.Pro_id;
                updm.Specialization = pdm.Specialization;
                hd.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }


        // POST: api/ProfessionalDetail
        [ResponseType(typeof(ProfessionalDetail))]
        public IHttpActionResult PostProfessionalDetail(ProfessionalDetail professionalDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProfessionalDetails.Add(professionalDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = professionalDetail.Pro_id }, professionalDetail);
        }

        public IHttpActionResult GetdocEdudetails(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            ProfessionalDetailModel Professionaldetails = null;
            Professionaldetails = hd.ProfessionalDetails.Where(x => x.Pro_id == id).Select(x => new ProfessionalDetailModel()
            {
                Pro_id = x.Pro_id,
                Specialization = x.Specialization

            }).FirstOrDefault<ProfessionalDetailModel>();
            if (Professionaldetails == null)
            {
                return NotFound();
            }
            return Ok(Professionaldetails);
        }

        // DELETE: api/ProfessionalDetail/5
        //[ResponseType(typeof(ProfessionalDetail))]
        //public IHttpActionResult DeleteProfessionalDetail(int id)
        //{
        //    ProfessionalDetail professionalDetail = db.ProfessionalDetails.Find(id);
        //    if (professionalDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    db.ProfessionalDetails.Remove(professionalDetail);
        //    db.SaveChanges();

        //    return Ok(professionalDetail);
        //}

        public IHttpActionResult Delete(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var Prodel = hd.ProfessionalDetails.Where(x => x.Pro_id == id).FirstOrDefault();
            hd.Entry(Prodel).State = EntityState.Deleted;
            hd.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfessionalDetailExists(int id)
        {
            return db.ProfessionalDetails.Count(e => e.Pro_id == id) > 0;
        }
    }
}