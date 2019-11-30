using HealthCareServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace HealthCareServiceUI.Controllers
{
    public class BrandMVCController : Controller
    {
        // GET: BrandMVC
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<BrandClass> brandobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var docconsume = hc.GetAsync("Brand");
                docconsume.Wait();

                var readdata = docconsume.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displayresults = readdata.Content.ReadAsAsync<IList<BrandClass>>();
                    displayresults.Wait();
                    brandobj = displayresults.Result;
                }
                return View(brandobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }

            
        }


        public ActionResult AddIndex()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
        }
        [HttpPost]
        public ActionResult AddIndex(BrandClass bc)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {


                    HttpClient hc = new HttpClient();
                    hc.BaseAddress = new Uri("https://localhost:44302/api/Brand");

                    var insertdata = hc.PostAsJsonAsync<BrandClass>("Brand", bc);
                    insertdata.Wait();

                    var savedata = insertdata.Result;
                    if (savedata.IsSuccessStatusCode)
                    {
                        ViewBag.message = "The Brand " + bc.BrandName + " Is Saved Successfully .. !";

                    }
                }
                return RedirectToAction("Index");
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
                BrandClass brandobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Brand?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<BrandClass>();
                    displaydata.Wait();
                    brandobj = displaydata.Result;
                }
                return View(brandobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(BrandClass bc)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/Brand");
                var insertrecord = hc.PutAsJsonAsync<BrandClass>("Brand", bc);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Brand Record Not Update ... !";
                }
                return View(bc);
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
                BrandClass brandobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Brand?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<BrandClass>();
                    displaydata.Wait();
                    brandobj = displaydata.Result;
                }
                return View(brandobj);
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
                hc.BaseAddress = new Uri("https://localhost:44302/api/Brand");

                var delrecord = hc.DeleteAsync("Brand/" + id.ToString());
                delrecord.Wait();

                var displaydata = delrecord.Result;
                if (displaydata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }
    }
}