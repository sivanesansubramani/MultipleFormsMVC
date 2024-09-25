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

            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = @"INSERT INTO Bio (FirstName, LastName, Age, Email, Gender, MobileNumber)
                                VALUES (@FirstName, @LastName, @Age, @Email, @Gender, @MobileNumber)";

                foreach (var form in forms)
                {
                    db.Execute(sqlQuery, form);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}