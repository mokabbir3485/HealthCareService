using HealthCareServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace HealthCareServiceUI.Controllers
{
    public class RegisterPatientController : Controller
    {
        // GET: RegisterPatient
        HealthCareDBEntities1 db = new HealthCareDBEntities1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Text()
        {
            return View();
        }

        public JsonResult SaveData(SitePatient model)
        {
            model.IsValid = false;
            db.SitePatients.Add(model);
            db.SaveChanges();
            BuildEmailTemplate(model.ID);
            return Json("Registration Successfull", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }
        public JsonResult RegisterConfirm(int regId)
        {
            SitePatient Data = db.SitePatients.Where(x => x.ID == regId).FirstOrDefault();
            Data.IsValid = true;
            db.SaveChanges();
            var msg = "Your Email Is Verified!";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/RegisterPatient/") + "Text" + ".cshtml");
            var regInfo = db.SitePatients.Where(x => x.ID == regID).FirstOrDefault();
            var url = "https://localhost:44302/" + "RegisterPatient/Confirm?regId=" + regID;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your Account Is Successfully Created", body, regInfo.Email);
        }

        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "shuvo3485@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("shuvo3485@gmail.com", "34853485");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult CheckValidUser(SitePatient model)
        {
            //using(HealthCareDBEntities1 hd = new HealthCareDBEntities1())
            //{
            //    var query = hd.SiteUsers.SqlQuery("select COUNT(*) from tbl_login where login_email='" +Email+ "' and login_password='" + password + "'").ToList<SiteUser>();
            //}

            string result = "Fail";
            var DataItem = db.SitePatients.Where(x => x.Email == model.Email && x.Password == model.Password && x.IsValid == true).SingleOrDefault();
            if (DataItem != null)
            {
                Session["UserID"] = DataItem.ID.ToString();
                Session["UserName"] = DataItem.Username.ToString();
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // //////////////////////// Copy this code all method
        public ActionResult AfterLogin()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "RegisterPatient");
            }
        }
        // ////////////////////
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}