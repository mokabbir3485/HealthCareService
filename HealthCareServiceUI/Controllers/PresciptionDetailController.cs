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
    public class PresciptionDetailController : ApiController
    {
        private HealthCareDBEntities1 db = new HealthCareDBEntities1();

        [HttpGet]
        public IHttpActionResult getPresciptionDetailDetails()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<PresciptionDetailModel> preDobj = hd.displayPresciptionDetail().Select(x => new PresciptionDetailModel()
            {
                PresDetail_id = x.PresDetail_id,
                Doze = x.Doze,
                Duration = x.Duration,

                Prescip_id = x.Prescip_id,
                Drug_id = x.Drug_id

            }).ToList<PresciptionDetailModel>();
            return Ok(preDobj);
        }


        public IHttpActionResult Put(PresciptionDetailModel pdm)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var updm = hd.PresciptionDetails.Where(x => x.PresDetail_id == pdm.PresDetail_id).FirstOrDefault<PresciptionDetail>();
            if (updm != null)
            {
                updm.PresDetail_id = pdm.PresDetail_id;
                updm.Doze = pdm.Doze;
                updm.Duration = pdm.Duration;
                hd.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }


       

        // POST: api/PresciptionDetail
        [ResponseType(typeof(PresciptionDetail))]
        public IHttpActionResult PostPresciptionDetail(PresciptionDetail presciptionDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PresciptionDetails.Add(presciptionDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = presciptionDetail.PresDetail_id }, presciptionDetail);
        }

        public IHttpActionResult GetdocEdudetails(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            PresciptionDetailModel PrescipDdetails = null;
            PrescipDdetails = hd.PresciptionDetails.Where(x => x.PresDetail_id == id).Select(x => new PresciptionDetailModel()
            {
                PresDetail_id = x.PresDetail_id,
                Doze = x.Doze,
                Duration = x.Duration

            }).FirstOrDefault<PresciptionDetailModel>();
            if (PrescipDdetails == null)
            {
                return NotFound();
            }
            return Ok(PrescipDdetails);
        }


        public IHttpActionResult Delete(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var PreDdel = hd.PresciptionDetails.Where(x => x.Prescip_id == id).FirstOrDefault();
            hd.Entry(PreDdel).State = EntityState.Deleted;
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

        private bool PresciptionDetailExists(int id)
        {
            return db.PresciptionDetails.Count(e => e.PresDetail_id == id) > 0;
        }
    }
}