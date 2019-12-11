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
    public class DocEducationController : ApiController
    {
        private HealthCareDBEntities1 db = new HealthCareDBEntities1();
        public int Edu_id;
        public string InstituteName;
        public string Degree;
        public string PassingYear;

        [HttpGet]
        public IHttpActionResult getDocEducationDetails()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<DocEducationModel> DocEobj = hd.displayDocEducation().Select(x => new DocEducationModel()
            {
                Edu_id = x.Edu_id,
                InstituteName = x.InstituteName,
                Degree = x.Degree,
                PassingYear = x.PassingYear,
                Doctor_id = x.Doctor_id

            }).ToList<DocEducationModel>();
            return Ok(DocEobj);
        }




        public IHttpActionResult Put(DocEducationModel dec)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var ude = hd.DocEducations.Where(x => x.Edu_id == dec.Edu_id).FirstOrDefault<DocEducation>();
            if (ude != null)
            {
                ude.Edu_id = dec.Edu_id;
                ude.InstituteName = dec.InstituteName;
                ude.Degree = dec.Degree;
                ude.PassingYear = dec.PassingYear;
                hd.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }


       

        // POST: api/DocEducation
        [ResponseType(typeof(DocEducation))]
        public IHttpActionResult PostDocEducation(DocEducation docEducation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DocEducations.Add(docEducation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = docEducation.Edu_id }, docEducation);
        }

        public IHttpActionResult GetdocEdudetails(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            DocEducationModel docEdudetails = null;
            docEdudetails = hd.DocEducations.Where(x => x.Edu_id == id).Select(x => new DocEducationModel()
            {
                Edu_id = x.Edu_id,
                InstituteName = x.InstituteName,
                Degree = x.Degree,
                PassingYear = x.PassingYear,
                Certificate = x.Certificate

            }).FirstOrDefault<DocEducationModel>();
            if (docEdudetails == null)
            {
                return NotFound();
            }
            return Ok(docEdudetails);
        }

        // DELETE: api/DocEducation/5
        //[ResponseType(typeof(DocEducation))]
        //public IHttpActionResult DeleteDocEducation(int id)
        //{
        //    DocEducation docEducation = db.DocEducations.Find(id);
        //    if (docEducation == null)
        //    {
        //        return NotFound();
        //    }

        //    db.DocEducations.Remove(docEducation);
        //    db.SaveChanges();

        //    return Ok(docEducation);
        //}

        public IHttpActionResult Delete(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var Edudel = hd.DocEducations.Where(x => x.Edu_id == id).FirstOrDefault();
            hd.Entry(Edudel).State = EntityState.Deleted;
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

        private bool DocEducationExists(int id)
        {
            return db.DocEducations.Count(e => e.Edu_id == id) > 0;
        }
    }
}