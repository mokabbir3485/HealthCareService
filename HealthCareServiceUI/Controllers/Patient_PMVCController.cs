using HealthCareServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HealthCareServiceUI.Controllers
{
    public class Patient_PMVCController : Controller
    {
        // GET: Patient_PMVC
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<Patient_PClass> patobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var docconsume = hc.GetAsync("Patient_P");
                docconsume.Wait();

                var readdata = docconsume.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displayresults = readdata.Content.ReadAsAsync<IList<Patient_PClass>>();
                    displayresults.Wait();
                    patobj = displayresults.Result;
                }
                return View(patobj);
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
        public ActionResult AddIndex(Patient_PClass pc)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    HttpClient hc = new HttpClient();
                    hc.BaseAddress = new Uri("https://localhost:44302/api/Patient_P");

                    var insertdata = hc.PostAsJsonAsync<Patient_PClass>("Patient_P", pc);
                    insertdata.Wait();
                    
                    var savedata = insertdata.Result;
                    if (savedata.IsSuccessStatusCode)
                    {
                        ViewBag.message = "The Patient " + pc.PatientFullName + " Is Saved Successfully .. !";

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
                Patient_PClass Patientobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Patient_P?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<Patient_PClass>();
                    displaydata.Wait();
                    Patientobj = displaydata.Result;
                }
                return View(Patientobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(Patient_PClass pc)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/Patient_P");
                var insertrecord = hc.PutAsJsonAsync<Patient_PClass>("Patient_P", pc);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Patient Record Not Update ... !";
                }
                return View(pc);
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
                Patient_PClass Patientobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Patient_P?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<Patient_PClass>();
                    displaydata.Wait();
                    Patientobj = displaydata.Result;
                }
                return View(Patientobj);
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
                hc.BaseAddress = new Uri("https://localhost:44302/api/Patient_P");

                var delrecord = hc.DeleteAsync("Patient_P/" + id.ToString());
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