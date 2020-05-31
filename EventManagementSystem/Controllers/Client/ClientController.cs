using EventManagementSystem.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace EventManagementSystem.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddClient(Client client)
        {
            Database1Entities1 db = new Database1Entities1();
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index", new { status = "success" });
            }
                
            return RedirectToAction("Index");
            
        }

        [HttpGet]
        public ActionResult AllClient()
        {
            Database1Entities1 db = new Database1Entities1();
            List<Client> clientList = db.Clients.ToList();
            return View(clientList);
        }

        /*
            Delete controller with email as parameter 
        */
        [HttpPost]
        public ActionResult DeleteClient(string email)
        {
            Database1Entities1 db = new Database1Entities1();

            
            var client = (from d in db.Clients
                          where d.email == email
                          select d).Single();
            if (client != null)
            {
                db.Clients.Remove(client);
                db.SaveChanges();
                return RedirectToAction("AllClient", new { status = "success" });
            } else
            {
                return RedirectToAction("AllClient", new { status = "failed" });
            }
        }


        /*
            get client  with email 
        */
        [HttpGet]
        public ActionResult GetClient(string email)
        {
            Database1Entities1 db = new Database1Entities1();

            var client = (from d in db.Clients
                          where d.email == email
                          select d).SingleOrDefault();

            if(client != null)
            {
                return Json(new { success = true}, JsonRequestBehavior.AllowGet);
            }
             else
            {
                return Json(new { error = false}, JsonRequestBehavior.AllowGet);
                
            }
        }

        /*
            update client information 
        */
        [HttpGet]
        public ActionResult UpdateClient(string email, string address, string phone)     
        {
            Database1Entities1 db = new Database1Entities1(); ;
            var client = db.Clients.SqlQuery("select * from Client where email = @email", new SqlParameter("@email",email))
                                    .FirstOrDefault();
           if (client != null)
            {
                client.address = address;
                client.phone = phone;
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                /*return RedirectToAction("AllClient", new { status = "successEdit" });*/
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = false}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}