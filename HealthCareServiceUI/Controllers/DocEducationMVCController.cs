using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HealthCareServiceUI.Models;
using System.Net.Http;
namespace HealthCareServiceUI.Controllers
{
    public class DocEducationMVCController : Controller
    {
        // GET: DocEducationMVC
        public ActionResult IndexList()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<DocEducationModel> proDobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var docconsume = hc.GetAsync("DocEducation");
                docconsume.Wait();

                var readdata = docconsume.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displayresults = readdata.Content.ReadAsAsync<IList<DocEducationModel>>();
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

        //Forgain key part.
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
        //////////////////////
        
        [HttpPost]
        public ActionResult Index(DocEducationModel dem)
        {
            if (Session["UserID"] != null)
            {
                try
                {
                    HealthCareDBEntities1 db = new HealthCareDBEntities1();
                    //FK
                    List<Doctor_P> li = db.Doctor_P.ToList();
                    ViewBag.doctor_list = new SelectList(li, "Doctor_id", "DoctorFullName");
                    ////
                    DocEducation de = new DocEducation();
                    de.InstituteName = dem.InstituteName;
                    de.Degree = dem.Degree;
                    de.PassingYear = dem.PassingYear;
                    de.Doctor_id = dem.Doctor_id;
                    db.DocEducations.Add(de);
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
                DocEducationModel deobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("DocEducation?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<DocEducationModel>();
                    displaydata.Wait();
                    deobj = displaydata.Result;
                }
                return View(deobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(DocEducationModel dec)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/DocEducation");
                var insertrecord = hc.PutAsJsonAsync<DocEducationModel>("DocEducation", dec);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexList");
                }
                else
                {
                    ViewBag.message = "Education Record Not Update ... !";
                }
                return View(dec);
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
                DocEducationModel DocEduobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("DocEducation?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<DocEducationModel>();
                    displaydata.Wait();
                    DocEduobj = displaydata.Result;
                }
                return View(DocEduobj);
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
                hc.BaseAddress = new Uri("https://localhost:44302/api/DocEducation");

                var delrecord = hc.DeleteAsync("DocEducation/" + id.ToString());
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