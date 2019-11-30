using HealthCareServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HealthCareServiceUI.Controllers
{
    public class DrugMVCController : Controller
    {
        // GET: DrugMVC
        public ActionResult IndexList()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<DrugModel> drugobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var docconsume = hc.GetAsync("Drug");
                docconsume.Wait();

                var readdata = docconsume.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displayresults = readdata.Content.ReadAsAsync<IList<DrugModel>>();
                    displayresults.Wait();
                    drugobj = displayresults.Result;
                }
                return View(drugobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        //Forgain key part.
        // GET: PresciptionMVC
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                HealthCareDBEntities1 db = new HealthCareDBEntities1();
                List<Generic> gli = db.Generics.ToList();
                ViewBag.Genericlist = new SelectList(gli, "Generic_id", "GenericName");

                List<Brand> bli = db.Brands.ToList();
                ViewBag.Brandlist = new SelectList(bli, "Brand_id", "BrandName");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
           
        }
        //////////////////////

        [HttpPost]
        public ActionResult Index(DrugModel dm)
        {
            if (Session["UserID"] != null)
            {
                try
                {
                    HealthCareDBEntities1 db = new HealthCareDBEntities1();
                    //FK
                    List<Generic> gli = db.Generics.ToList();
                    ViewBag.Genericlist = new SelectList(gli, "Generic_id", "GenericName");

                    List<Brand> bli = db.Brands.ToList();
                    ViewBag.Brandlist = new SelectList(bli, "Brand_id", "BrandName");
                    ////
                    Drug d = new Drug();
                    d.Name = dm.Name;
                    d.Strength = dm.Strength;
                    d.Type = dm.Type;

                    //Fk
                    d.Generic_id = dm.Generic_id;
                    d.Brand_id = dm.Brand_id;
                    ////
                    db.Drugs.Add(d);
                    db.SaveChanges();
                    //int lastestpro_id = pd.Pro_id;
                    return RedirectToAction("IndexList");

                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        public ActionResult Edit(int id)
        {
            if (Session["UserID"] != null)
            {
                DrugModel Drugobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Drug?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<DrugModel>();
                    displaydata.Wait();
                    Drugobj = displaydata.Result;
                }
                return View(Drugobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(DrugModel dc)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/Drug");
                var insertrecord = hc.PutAsJsonAsync<DrugModel>("Drug", dc);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexList");
                }
                else
                {
                    ViewBag.message = "Drug Record Not Update ... !";
                }
                return View(dc);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }


        public ActionResult Details(int id)
        {
            if (Session["UserID"] != null)
            {
                DrugModel Drugobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Drug?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<DrugModel>();
                    displaydata.Wait();
                    Drugobj = displaydata.Result;
                }
                return View(Drugobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }



        public ActionResult Delete(int id)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/Drug");

                var delrecord = hc.DeleteAsync("Drug/" + id.ToString());
                delrecord.Wait();

                var displaydata = delrecord.Result;
                if (displaydata.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexList");
                }
                return View("IndexList");
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }
    }
}