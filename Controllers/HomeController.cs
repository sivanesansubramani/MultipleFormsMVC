using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeFormApplication.Models;
using Dapper;
using System.Data.SqlClient;

namespace EmployeeFormApplication.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = @"Data Source = DESKTOP-Q5KI2MS; Initial Catalog = EnployeeAppTask; Integrated Security = True";


        [HttpGet]
        public ActionResult Index()
        {
            return View(new List<FormModel>());
        }

        [HttpPost]
        public ActionResult Save(List<FormModel> forms)
        {
            if (forms == null || !forms.Any())
            {
                ModelState.AddModelError("", "At least one form is required.");
                return View("Index", forms);
            }

            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = @"INSERT INTO Bio (Name, PhoneNumber, Email, Gender, Skills, Address)
                                    VALUES (@Name, @PhoneNumber, @Email, @Gender, @Skills, @Address)";

                foreach (var form in forms)
                {
                    if (!ModelState.IsValid)
                    {
                        return View("Index", forms);
                    }

                    var skills = form.Skills != null ? string.Join(",", form.Skills) : null;

                    db.Execute(sqlQuery, new
                    {
                        form.Name,
                        form.PhoneNumber,
                        form.Email,
                        form.Gender,
                        Skills = skills,
                        form.Address
                    });
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult list()
        {
            List<SelectDataModel> forms = new List<SelectDataModel>();

            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT Name, PhoneNumber, Email, Gender, REPLACE(Skills,',',' ') as Skills, Address FROM Bio;";
                forms = db.Query<SelectDataModel>(sqlQuery).ToList();
            }

            return View(forms);
        }
    }
}