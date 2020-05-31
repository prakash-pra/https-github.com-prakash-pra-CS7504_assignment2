using EventManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagementSystem.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEvents(string name, string email, string guestnumber, string amount)
        {
            var db = new Database1Entities1();
            var createdOn = DateTime.Today;
            var commandText = "INSERT into Registration (name, email, payment, guestnumber, createdOn) VALUES (@name,@email,@payment,@guestnumber,@createdOn)";
            var names = new SqlParameter("@name", name);
            var emails = new SqlParameter("@email", email);
            var payment = new SqlParameter("@payment", guestnumber);
            var guest = new SqlParameter("@guestnumber", amount);
            var createOn = new SqlParameter("@createdOn", createdOn);
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand(commandText,names, emails, payment, guest, createOn);
               
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
               
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
 
        }
    }
}