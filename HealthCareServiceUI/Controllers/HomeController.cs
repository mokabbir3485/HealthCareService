using HealthCareServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthCareServiceUI.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = @"Server=tcp:prescription.database.windows.net,1433;Initial Catalog=HealthCareDB;Persist Security Info=False;User ID=shuvo;Password=Mokabbir3485;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private HealthCareDBEntities1 db = new HealthCareDBEntities1();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(db.Patient_P.ToList());
        }
        public ActionResult IndexPatient()
        {

            return View(db.Patient_P.ToList());
        }
        public ActionResult RegisterPatient()
        {
            return View();
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult RegisterPatient(Patient_P Reg_P)
        {
            if (ModelState.IsValid)
            {
                db.Patient_P.Add(Reg_P);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }


        //
        // GET: patient/Edit/5
        public ActionResult EditPatient(int id)
        {
                
                //RegistrationModel RegistrationModel = new RegistrationModel();
                 DataTable tblRegisterPatient = new DataTable();
            PatientModel patient = new PatientModel();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Patient_P where Patient_id = @Patient_id";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Patient_id", id);
                sqlDa.Fill(tblRegisterPatient);
            }
            if (tblRegisterPatient.Rows.Count == 1)
            {
                patient.Patient_id = Convert.ToInt32(tblRegisterPatient.Rows[0][0].ToString());
                patient.PatientFullName = tblRegisterPatient.Rows[0][1].ToString();
                patient.Gender = tblRegisterPatient.Rows[0][2].ToString();
                patient.Age = Convert.ToInt32(tblRegisterPatient.Rows[0][3].ToString());
                patient.Height = tblRegisterPatient.Rows[0][4].ToString();
                patient.Weight = tblRegisterPatient.Rows[0][5].ToString();
                patient.IsMarried = tblRegisterPatient.Rows[0][6].ToString();
                patient.Mobile = tblRegisterPatient.Rows[0][7].ToString();
                patient.Email = tblRegisterPatient.Rows[0][8].ToString();

                return View(patient);
            }
            else
                return RedirectToAction("RegisterPatient");
        }

        //
        // POST: /patient/Edit/5
        [HttpPost]
        public ActionResult EditPatient(PatientModel patient)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Patient_P SET PatientFullName = @PatientFullName,Gender = @Gender,Age = @Age,Height = @Height,Weight = @Weight,IsMarried = @IsMarried,Mobile = @Mobile,Email = @Email where Patient_id = @Patient_id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Patient_id", patient.Patient_id);
                sqlCmd.Parameters.AddWithValue("@PatientFullName", patient.PatientFullName);
                sqlCmd.Parameters.AddWithValue("@Gender", patient.Gender);
                sqlCmd.Parameters.AddWithValue("@Age", patient.Age);
                sqlCmd.Parameters.AddWithValue("@Height", patient.Height);
                sqlCmd.Parameters.AddWithValue("@Weight", patient.Weight);
                sqlCmd.Parameters.AddWithValue("@IsMarried", patient.IsMarried);
                sqlCmd.Parameters.AddWithValue("@Mobile", patient.Mobile);
                sqlCmd.Parameters.AddWithValue("@Email", patient.Email);

                sqlCmd.ExecuteNonQuery();

            }
            return RedirectToAction("IndexPatient");
        }

        //
        // GET: /Patient/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE from Patient_P  where Patient_id = @Patient_id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Patient_id", id);
                sqlCmd.ExecuteNonQuery();

            }
            return RedirectToAction("IndexPatient");
        }
    }
}
