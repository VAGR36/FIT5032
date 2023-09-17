using FIT5032_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace FIT5032_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private UserEntities db = new UserEntities();
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(USER user)
        {

            var account = db.USERS.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if (account != null)
            {
                // Login Success
                // Redirect to home page
                Session["UserId"] = account.ID.ToString();
                Session["Username"] = account.Username.ToString();
                Session["Role"] = account.Role.ToString();
                if (Session["Role"].ToString() == "ADMIN")
                {
                    return RedirectToAction("Create", "USERs");
                }
                else if (Session["Role"].ToString() == "STAFF")
                {
                    return RedirectToAction("StaffPage", "USERs");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user type");
                }
                
            }
            else
            {
                // Login Fail
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View();
        }


    }
}