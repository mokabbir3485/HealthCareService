using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthCareServiceUI.Models;
using System.Net.Http;

namespace HealthCareServiceUI.Controllers
{
    public class DoctorJointablesMVCController : Controller
    {
        // GET: DoctorJointablesMVC
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<DoctorJoinTablesClass> djt = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/DoctorJoinTables");

                var consumeapi = hc.GetAsync("DoctorJoinTables");
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<DoctorJoinTablesClass>>();
                    displaydata.Wait();
                    djt = displaydata.Result;
                }
                return View(djt);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        
    }
}