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
    public class BrandController : ApiController
    {

        // GET: api/Brand
        HealthCareDBEntities1 hd = new HealthCareDBEntities1();
        public string BrandName;
        public string Brand_id;

        [HttpGet]
        public IHttpActionResult getBranddetails()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<BrandClass> brandobj = hd.displayBrand().Select(x => new BrandClass()
            {
                Brand_id = x.Brand_id,
                BrandName = x.BrandName
            }).ToList<BrandClass>();
            return Ok(brandobj);
        }

        [HttpPost]
        public IHttpActionResult addBrand(Brand b)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            hd.AddBrand(b.BrandName);
            hd.SaveChanges();
            return Ok();
        }


        public IHttpActionResult PutBrand(BrandClass bc)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var ub = hd.Brands.Where(x => x.Brand_id == bc.Brand_id).FirstOrDefault<Brand>();
            if (ub != null)
            {
                ub.Brand_id = bc.Brand_id;
                ub.BrandName = bc.BrandName;
                hd.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }



        //Details
        public IHttpActionResult GetBrand(int id)
        {
            BrandClass branddetails = null;
            branddetails = hd.Brands.Where(x => x.Brand_id == id).Select(x => new BrandClass()
            {
                Brand_id = x.Brand_id,
                BrandName = x.BrandName,
            }).FirstOrDefault<BrandClass>();
            if(branddetails == null)
            {
                return NotFound();
            }
            return Ok(branddetails);
        }

        
        public IHttpActionResult Delete(int id)
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            var brandDdel = hd.Brands.Where(x => x.Brand_id == id).FirstOrDefault();
            hd.Entry(brandDdel).State = EntityState.Deleted;
            hd.SaveChanges();
            return Ok();
        }

    }
}
