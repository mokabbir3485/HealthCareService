using HealthCareServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HealthCareServiceUI.Controllers
{
    public class PresciptionMVCController : Controller
    {
        // GET: PresciptionMVC
        public ActionResult IndexList()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<PresciptionModel> PMobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var docconsume = hc.GetAsync("Presciption");
                docconsume.Wait();

                var readdata = docconsume.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displayresults = readdata.Content.ReadAsAsync<IList<PresciptionModel>>();
                    displayresults.Wait();
                    PMobj = displayresults.Result;
                }
                return View(PMobj);
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
                List<Patient_P> pli = db.Patient_P.ToList();
                ViewBag.patient_plist = new SelectList(pli, "Patient_id", "PatientFullName");

                List<Doctor_P> Dli = db.Doctor_P.ToList();
                ViewBag.doctor_plist = new SelectList(Dli, "Doctor_id", "DoctorFullName");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }
        //////////////////////

        [HttpPost]
        public ActionResult Index(PresciptionModel pm)
        {
            if (Session["UserID"] != null)
            {
                try
                {
                    HealthCareDBEntities1 db = new HealthCareDBEntities1();
                    //FK
                    List<Patient_P> pli = db.Patient_P.ToList();
                    ViewBag.patient_list = new SelectList(pli, "Patient_id", "PatientFullName");

                    List<Doctor_P> Dli = db.Doctor_P.ToList();
                    ViewBag.doctor_list = new SelectList(Dli, "Doctor_id", "DoctorFullName");
                    ////
                    Presciption p = new Presciption();
                    p.Date = pm.Date;
                    p.BP = pm.BP;
                    p.Temp = pm.Temp;
                    p.Weight = pm.Weight;
                    p.Visit_no = pm.Visit_no;
                    p.NextDate = pm.NextDate;
                    p.Indication = pm.Indication;
                    //Fk
                    p.Patient_id = pm.Patient_id;
                    p.Doctor_id = pm.Doctor_id;
                    ////
                    db.Presciptions.Add(p);
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
                PresciptionModel Presciptionobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Presciption?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<PresciptionModel>();
                    displaydata.Wait();
                    Presciptionobj = displaydata.Result;
                }
                return View(Presciptionobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(PresciptionModel pm)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/Presciption");
                var insertrecord = hc.PutAsJsonAsync<PresciptionModel>("Presciption", pm);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexList");
                }
                else
                {
                    ViewBag.message = "Presciption Record Not Update ... !";
                }
                return View(pm);
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
                PresciptionModel Presciptionobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("Presciption?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<PresciptionModel>();
                    displaydata.Wait();
                    Presciptionobj = displaydata.Result;
                }
                return View(Presciptionobj);
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
                hc.BaseAddress = new Uri("https://localhost:44302/api/Presciption");

                var delrecord = hc.DeleteAsync("Presciption/" + id.ToString());
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