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
    public class DrugController : ApiController
    {
        private HealthCareDBEntities1 db = new HealthCareDBEntities1();
        public int Drug_id;
        public string Name;
        public string Strength;
        public string Type;

        [HttpGet]
        public IHttpActionResult getDrugDetails()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<DrugModel> drugobj = hd.displayDrug().Select(x => new DrugModel()
            {
                Drug_id = x.Drug_id,
                Name = x.Name,
                Strength = x.Strength,
                Type = x.Type,
                
                Generic_id = x.Generic_id,
                Brand_id = x.Brand_id

            }).ToList<DrugModel>();
            return Ok(drugobj);
        }

       
        public IHttpActionResult Put(DrugModel dc)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var ud = hd.Drugs.Where(x => x.Drug_id == dc.Drug_id).FirstOrDefault<Drug>();
            if (ud != null)
            {
                ud.Drug_id = dc.Drug_id;
                ud.Name = dc.Name;
                ud.Strength = dc.Strength;
                ud.Type = dc.Type;
                hd.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }


        

        // POST: api/Drug
        [ResponseType(typeof(Drug))]
        public IHttpActionResult PostDrug(Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drugs.Add(drug);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drug.Drug_id }, drug);
        }

        public IHttpActionResult Getdrugdetails(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            DrugModel drugdetails = null;
            drugdetails = hd.Drugs.Where(x => x.Drug_id == id).Select(x => new DrugModel()
            {
                Drug_id = x.Drug_id,
                Name = x.Name,
                Strength = x.Strength,
                Type = x.Type

            }).FirstOrDefault<DrugModel>();
            if (drugdetails == null)
            {
                return NotFound();
            }
            return Ok(drugdetails);
        }

        

        public IHttpActionResult Delete(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var Drugdel = hd.Drugs.Where(x => x.Drug_id == id).FirstOrDefault();
            hd.Entry(Drugdel).State = EntityState.Deleted;
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

        private bool DrugExists(int id)
        {
            return db.Drugs.Count(e => e.Drug_id == id) > 0;
        }
    }
}