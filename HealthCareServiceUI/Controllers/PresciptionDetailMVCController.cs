using HealthCareServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HealthCareServiceUI.Controllers
{
    public class PresciptionDetailMVCController : Controller
    {
        // GET: PresciptionDetailMVC
        public ActionResult IndexList()
        {
            if (Session["UserID"] != null)
            {
                IEnumerable<PresciptionDetailModel> predobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var docconsume = hc.GetAsync("PresciptionDetail");
                docconsume.Wait();

                var readdata = docconsume.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displayresults = readdata.Content.ReadAsAsync<IList<PresciptionDetailModel>>();
                    displayresults.Wait();
                    predobj = displayresults.Result;
                }
                return View(predobj);
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
                List<Presciption> pli = db.Presciptions.ToList();
                ViewBag.Presciptionlist = new SelectList(pli, "Prescip_id", "Prescip_id");

                List<Drug> dli = db.Drugs.ToList();
                ViewBag.Druglist = new SelectList(dli, "Drug_id", "Name");

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }
        //////////////////////

        [HttpPost]
        public ActionResult Index(PresciptionDetailModel predm)
        {
            if (Session["UserID"] != null)
            {
                try
                {
                    HealthCareDBEntities1 db = new HealthCareDBEntities1();
                    //FK
                    List<Presciption> pli = db.Presciptions.ToList();
                    ViewBag.Presciptionlist = new SelectList(pli, "Prescip_id", "Prescip_id");

                    List<Drug> dli = db.Drugs.ToList();
                    ViewBag.Druglist = new SelectList(dli, "Drug_id", "Name");
                    ////
                    PresciptionDetail pred = new PresciptionDetail();
                    pred.Doze = predm.Doze;
                    pred.Duration = predm.Duration;

                    //Fk
                    pred.Prescip_id = predm.Prescip_id;
                    pred.Drug_id = predm.Drug_id;
                    ////
                    db.PresciptionDetails.Add(pred);
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
                PresciptionDetailModel PrescipDobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("PresciptionDetail?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<PresciptionDetailModel>();
                    displaydata.Wait();
                    PrescipDobj = displaydata.Result;
                }
                return View(PrescipDobj);
            }
            else
            {
                return RedirectToAction("Index", "Register");
            }
            
        }

        [HttpPost]
        public ActionResult Edit(PresciptionDetailModel pdm)
        {
            if (Session["UserID"] != null)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/PresciptionDetail");
                var insertrecord = hc.PutAsJsonAsync<PresciptionDetailModel>("PresciptionDetail", pdm);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexList");
                }
                else
                {
                    ViewBag.message = "Presciption Detail Record Not Update ... !";
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
                PresciptionDetailModel PrescipDobj = null;

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44302/api/");

                var consumeapi = hc.GetAsync("PresciptionDetail?id=" + id.ToString());
                consumeapi.Wait();

                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<PresciptionDetailModel>();
                    displaydata.Wait();
                    PrescipDobj = displaydata.Result;
                }
                return View(PrescipDobj);
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
                hc.BaseAddress = new Uri("https://localhost:44302/api/PresciptionDetail");

                var delrecord = hc.DeleteAsync("PresciptionDetail/" + id.ToString());
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