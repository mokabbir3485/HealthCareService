using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthCareServiceUI.Models;
using System.Net.Http;

namespace HealthCareServiceUI.Controllers
{
    public class DrugJoinTablesMVCController : Controller
    {
        // GET: DrugJoinTablesMVC
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<DrugJoinTablesClass> djt = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/DrugJoinTables");

                var consumeapi = hc.GetAsync("DrugJoinTables");
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<DrugJoinTablesClass>>();
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