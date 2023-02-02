using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment_Form.Models;


namespace Assignment_Form.Controllers
{
    public class HomeController : Controller
    {
        MyRestaurantdatabseContext dbobj = new MyRestaurantdatabseContext();
        // GET: Home
        public ActionResult Login() // Login User get method
        {
            Session["username"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(User model) // Login User post method
        { 
            var access = dbobj.User.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if(model.Username=="durgeshadmin" && model.Password=="durgeshpass")
            {
                Session["username"] = model.Username;
                return RedirectToAction("Displayusers", "Dashboard");
            }
            else if (access != null)
            {
                model = access;
                Session["username"] =model.Username;
                return RedirectToAction("Displayoneuser", "Dashboard", model);
            }
            else
            {  
                ModelState.AddModelError("Password", "Invalid username or password");
                return View();
            }
        }



        public ActionResult Signup() // Signup User get method
        {
            Session["username"] ="";
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model) // signup User post method
        {
            if (ModelState.IsValid)
            {
                bool exists = dbobj.User.Any(m => m.Username == model.Username);
                if (exists)
                {
                    ModelState.AddModelError("Username", "Username already exist");
                    return View();
                }
                dbobj.User.Add(model);
                dbobj.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }

    }
}