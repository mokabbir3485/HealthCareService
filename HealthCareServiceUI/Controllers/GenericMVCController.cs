using HealthCareServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HealthCareServiceUI.Controllers
{
    public class GenericMVCController : Controller
    {
        // GET: GenericMVC
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<GenericClass> genericdobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var docconsume = hc.GetAsync("Generic");
                docconsume.Wait();

                var readdata = docconsume.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displayresults = readdata.Content.ReadAsAsync<IList<GenericClass>>();
                    displayresults.Wait();
                    genericdobj = displayresults.Result;
                }
                return View(genericdobj);
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
        public ActionResult AddIndex(GenericClass gc)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {

                    HttpClient hc = new HttpClient();
                    hc.BaseAddress = new Uri("https://localhost:44302/api/Generic");

                    var insertdata = hc.PostAsJsonAsync<GenericClass>("Generic", gc);
                    insertdata.Wait();

                    var savedata = insertdata.Result;
                    if (savedata.IsSuccessStatusCode)
                    {
                        ViewBag.message = "The Generic " + gc.GenericName + " Is Saved Successfully .. !";

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
                GenericClass genericobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Generic?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<GenericClass>();
                    displaydata.Wait();
                    genericobj = displaydata.Result;
                }
                return View(genericobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(GenericClass gc)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/Generic");
                var insertrecord = hc.PutAsJsonAsync<GenericClass>("Generic", gc);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Generic Record Not Update ... !";
                }
                return View(gc);
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
                GenericClass Genericobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Generic?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<GenericClass>();
                    displaydata.Wait();
                    Genericobj = displaydata.Result;
                }
                return View(Genericobj);
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
                hc.BaseAddress = new Uri("https://localhost:44302/api/Generic");

                var delrecord = hc.DeleteAsync("Generic/" + id.ToString());
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