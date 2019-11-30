using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthCareServiceUI.Models;

namespace HealthCareServiceUI.Controllers
{
    public class DrugJoinTablesController : ApiController
    {
        public IHttpActionResult getjointables()
        {
            HealthCareDBEntities1 hd = new HealthCareDBEntities1();
            IList<DrugJoinTablesClass> djt = hd.DrugJoinTables().Select(x => new DrugJoinTablesClass()
            {
                Name = x.Name,
                GenericName = x.GenericName,
                BrandName = x.BrandName,
                Strength = x.Strength,
                Type = x.Type

            }).ToList();
            return Ok(djt);
        }
    }
}
