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
    public class GenericController : ApiController
    {
        public int Generic_id;
        public string GenericName;

        [HttpGet]
        public IHttpActionResult getGenericdetails()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<GenericClass> genericobj = hd.displayGeneric().Select(x => new GenericClass()
            {
                Generic_id = x.Generic_id,
                GenericName = x.GenericName
            }).ToList<GenericClass>();
            return Ok(genericobj);
        }

        [HttpPost]
        public IHttpActionResult addGeneric(Generic g)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            hd.AddGeneric(g.GenericName);
            hd.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Put(GenericClass gc)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var ug = hd.Generics.Where(x => x.Generic_id == gc.Generic_id).FirstOrDefault<Generic>();
            if (ug != null)
            {
                ug.Generic_id = gc.Generic_id;
                ug.GenericName = gc.GenericName;
                hd.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }


        public IHttpActionResult GetGenericdetails(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            GenericClass Genericdetails = null;
            Genericdetails = hd.Generics.Where(x => x.Generic_id == id).Select(x => new GenericClass()
            {
                Generic_id = x.Generic_id,
                GenericName = x.GenericName

            }).FirstOrDefault<GenericClass>();
            if (Genericdetails == null)
            {
                return NotFound();
            }
            return Ok(Genericdetails);
        }

        public IHttpActionResult Delete(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var Genericdel = hd.Generics.Where(x => x.Generic_id == id).FirstOrDefault();
            hd.Entry(Genericdel).State = EntityState.Deleted;
            hd.SaveChanges();
            return Ok();
        }
    }
}
