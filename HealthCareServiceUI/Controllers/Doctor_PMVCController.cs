using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthCareServiceUI.Models;
using System.Net.Http;

namespace HealthCareServiceUI.Controllers
{
    public class Doctor_PMVCController : Controller
    {
        // GET: Doctor_PMVC
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                    IEnumerable<Doctor_PClass> docobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var docconsume = hc.GetAsync("Doctor_P");
                docconsume.Wait();
            
                var readdata = docconsume.Result;

                    if (readdata.IsSuccessStatusCode)
                    {
                        var displayresults = readdata.Content.ReadAsAsync<IList<Doctor_PClass>>();
                        displayresults.Wait();
                        docobj = displayresults.Result;
                    }
                    return View(docobj);
            }
            else
            {
                return RedirectToAction("Index","Register");
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
                return RedirectToAction("Index","Register");
            }
        }
        [HttpPost]
        public ActionResult AddIndex(Doctor_PClass dc)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    HttpClient hc = new HttpClient();
                    hc.BaseAddress = new Uri("https://localhost:44302/api/Doctor_P");

                    var insertdata = hc.PostAsJsonAsync<Doctor_PClass>("Doctor_P", dc);
                    insertdata.Wait();

                    var savedata = insertdata.Result;
                    if (savedata.IsSuccessStatusCode)
                    {
                        ViewBag.message = "The Doctor " + dc.DoctorFullName + " Is Saved Successfully .. !";

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
                Doctor_PClass doctorobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Doctor_P?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<Doctor_PClass>();
                    displaydata.Wait();
                    doctorobj = displaydata.Result;
                }
                return View(doctorobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
           
        }

        [HttpPost]
        public ActionResult Edit(Doctor_PClass dc)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/Doctor_P");
                var insertrecord = hc.PutAsJsonAsync<Doctor_PClass>("Doctor_P", dc);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Doctor Record Not Update ... !";
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
                Doctor_PClass doctorobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Doctor_P?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<Doctor_PClass>();
                    displaydata.Wait();
                    doctorobj = displaydata.Result;
                }
                return View(doctorobj);
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
                hc.BaseAddress = new Uri("https://localhost:44302/api/Doctor_P");

                var delrecord = hc.DeleteAsync("Doctor_P/" + id.ToString());
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