using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthCareServiceUI.Models;
using System.Net.Http;

namespace HealthCareServiceUI.Controllers
{
    public class ProfessionalDetailMVCController : Controller
    {
        public ActionResult IndexList()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<ProfessionalDetailModel> proDobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var docconsume = hc.GetAsync("ProfessionalDetail");
                docconsume.Wait();

                var readdata = docconsume.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displayresults = readdata.Content.ReadAsAsync<IList<ProfessionalDetailModel>>();
                    displayresults.Wait();
                    proDobj = displayresults.Result;
                }
                return View(proDobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }


        // GET: ProfessionalDetailMVC
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                HealthCareDBEntities1 db = new HealthCareDBEntities1();
                List<Doctor_P> li = db.Doctor_P.ToList();
                ViewBag.doctor_list = new SelectList(li, "Doctor_id", "DoctorFullName");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        [HttpPost]
        public ActionResult Index(ProfessionalDetailModel pdm)
        {
            if (Session["UserID"] != null)
            {
                try
                {
                    HealthCareDBEntities1 db = new HealthCareDBEntities1();
                    List<Doctor_P> li = db.Doctor_P.ToList();
                    ViewBag.doctor_list = new SelectList(li, "Doctor_id", "DoctorFullName");
                    ProfessionalDetail pd = new ProfessionalDetail();
                    pd.Specialization = pdm.Specialization;
                    pd.Doctor_id = pdm.Doctor_id;
                    db.ProfessionalDetails.Add(pd);
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
                ProfessionalDetailModel ProfDobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("ProfessionalDetail?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<ProfessionalDetailModel>();
                    displaydata.Wait();
                    ProfDobj = displaydata.Result;
                }
                return View(ProfDobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(ProfessionalDetailModel pdm)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/ProfessionalDetail");
                var insertrecord = hc.PutAsJsonAsync<ProfessionalDetailModel>("ProfessionalDetail", pdm);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexList");
                }
                else
                {
                    ViewBag.message = "Professional Details Record Not Update ... !";
                }
                return View(pdm);
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
                ProfessionalDetailModel ProfDobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("ProfessionalDetail?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<ProfessionalDetailModel>();
                    displaydata.Wait();
                    ProfDobj = displaydata.Result;
                }
                return View(ProfDobj);
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
                hc.BaseAddress = new Uri("https://localhost:44302/api/ProfessionalDetail");

                var delrecord = hc.DeleteAsync("ProfessionalDetail/" + id.ToString());
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