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
    public class PresciptionController : ApiController
    {
        private HealthCareDBEntities1 db = new HealthCareDBEntities1();
        public int Prescip_id;
        public string Date;
        public string BP;
        public string Temp;
        public string Weight;
        public string NextDate;
        public string Indication;

        [HttpGet]
        public IHttpActionResult getPresciptionDetails()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<PresciptionModel> preobj = hd.displayPresciption().Select(x => new PresciptionModel()
            {
                Prescip_id = x.Prescip_id,
                Date = x.Date,
                BP = x.BP,
                Temp = x.Temp,
                Weight = x.Weight,
                Visit_no = x.Visit_no,
                NextDate = x.NextDate,
                Indication = x.Indication,
                Patient_id = x.Patient_id,
                Doctor_id = x.Doctor_id

            }).ToList<PresciptionModel>();
            return Ok(preobj);
        }


        
        public IHttpActionResult Put(PresciptionModel pm)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var up = hd.Presciptions.Where(x => x.Prescip_id == pm.Prescip_id).FirstOrDefault<Presciption>();
            if (up != null)
            {
                up.Prescip_id = pm.Prescip_id;
                up.Date = pm.Date;
                up.BP = pm.BP;
                up.Temp = pm.Temp;
                up.Weight = pm.Weight;
                up.Visit_no = pm.Visit_no;
                up.NextDate = pm.NextDate;
                up.Indication = pm.Indication;
                hd.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }


        

        // POST: api/Presciption
        [ResponseType(typeof(Presciption))]
        public IHttpActionResult PostPresciption(Presciption presciption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Presciptions.Add(presciption);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = presciption.Prescip_id }, presciption);
        }

        public IHttpActionResult GetdocEdudetails(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            PresciptionModel Presciptiondetails = null;
            Presciptiondetails = hd.Presciptions.Where(x => x.Prescip_id == id).Select(x => new PresciptionModel()
            {
                Prescip_id = x.Prescip_id,
                Date = x.Date,
                BP = x.BP,
                Temp = x.Temp,
                Weight = x.Weight,
                Visit_no = x.Visit_no,
                NextDate = x.NextDate,
                Indication = x.Indication
                

            }).FirstOrDefault<PresciptionModel>();
            if (Presciptiondetails == null)
            {
                return NotFound();
            }
            return Ok(Presciptiondetails);
        }

        // DELETE: api/Presciption/5
        //[ResponseType(typeof(Presciption))]
        //public IHttpActionResult DeletePresciption(int id)
        //{
        //    Presciption presciption = db.Presciptions.Find(id);
        //    if (presciption == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Presciptions.Remove(presciption);
        //    db.SaveChanges();

        //    return Ok(presciption);
        //}
        public IHttpActionResult Delete(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var Prescipdel = hd.Presciptions.Where(x => x.Prescip_id == id).FirstOrDefault();
            hd.Entry(Prescipdel).State = EntityState.Deleted;
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

        private bool PresciptionExists(int id)
        {
            return db.Presciptions.Count(e => e.Prescip_id == id) > 0;
        }
    }
}