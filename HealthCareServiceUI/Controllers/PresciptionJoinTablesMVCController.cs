using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthCareServiceUI.Models;
using System.Net.Http;

namespace HealthCareServiceUI.Controllers
{
    public class PresciptionJoinTablesMVCController : Controller
    {
        // GET: PresciptionJoinTablesMVC
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<PresciptionJoinTablesClass> pjt = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/PresciptionJoinTables");

                var consumeapi = hc.GetAsync("PresciptionJoinTables");
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<IList<PresciptionJoinTablesClass>>();
                    displaydata.Wait();
                    pjt = displaydata.Result;
                }
                return View(pjt);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }
    }
}