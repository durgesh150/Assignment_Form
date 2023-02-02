using Assignment_Form.Models;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Configuration;

namespace Assignment_Form.Controllers
{
    public class DashboardController : Controller
    {
        public MyRestaurantdatabseContext dbobj = new MyRestaurantdatabseContext();

        public ActionResult UpdateUserDetails(string username)
        {
            var user = dbobj.User.FirstOrDefault(u => u.Username == username);
            return View(user);
        }
        [HttpPost]
        public ActionResult UpdateUserDetails(User model)
        {
            var existingdata = dbobj.User.FirstOrDefault(u => u.Username == model.Username);
            if (existingdata != null)
            {
                using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ForConnectingstrings"].ConnectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("UPDATE users SET FirstName=@firstname, LastName=@lastname, Gender=@gender, Email=@email WHERE Username=@username", connection))
                    {
                        command.Parameters.AddWithValue("@firstname", model.FirstName);
                        command.Parameters.AddWithValue("@lastname", model.LastName);
                        command.Parameters.AddWithValue("@gender", model.Gender);
                        command.Parameters.AddWithValue("@email", model.Email);
                        command.Parameters.AddWithValue("@username", model.Username);
                        command.ExecuteNonQuery();
                    }
                }
                dbobj.SaveChanges();   
            }
            string check = (string)Session["username"];
            if (check=="durgeshadmin")
            {

                return RedirectToAction("displayusers", "Dashboard");
            }
            else
            {
                return RedirectToAction("displayoneuser", "Dashboard");
            }
        }


        // GET: Dashboard
        public ViewResult Displayusers() //Display all the users method
        {
            return View(dbobj);
        }
        [ValidateInput(false)]
        public ViewResult Deleteusers(string username) // Delete User get method
        {
            var user = dbobj.User.FirstOrDefault(u => u.Username == username);
            return View(user);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Deleteusers(User model)  // Delete User post method
        {
            var access = dbobj.User.Where(u => u.Username == model.Username).FirstOrDefault();
            if (access != null)
            {
                dbobj.Database.ExecuteSqlCommand("DELETE FROM users WHERE Username = @username", new MySqlParameter("username", model.Username));
                dbobj.SaveChanges();
                return RedirectToAction("displayusers", "dashboard");

            }
            else
            {
                ModelState.AddModelError("Username", "Username doesn't exist");
                return View();
            }
        }
        public ActionResult Updatepassword()  // Update User password get method
        {

            return View();
        }
        [HttpPost]
        public ActionResult Updatepassword(User model) // Update User password get method
        {

            User existingData = dbobj.User.Find(model.Username);
            if (existingData != null)
            {
                using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["ForConnectingstrings"].ConnectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("UPDATE users SET Password=@password, ConfirmPassword=@confirmpassword WHERE Username=@username", connection))
                    {
                        command.Parameters.AddWithValue("@password", model.Password);
                        command.Parameters.AddWithValue("@confirmpassword", model.ConfirmPassword);
                        command.Parameters.AddWithValue("@username", model.Username);
                        command.ExecuteNonQuery();
                    }
                }
                dbobj.SaveChanges();

                return RedirectToAction("Login", "Home");
            }
            else
            {
                ModelState.AddModelError("ConfirmPassword", "Username Not Found or no data to update");
                return View();
            }
        }
        public ActionResult Displayoneuser(User userobj) //Display one the users method
        {
            
               
                return View(userobj);
       

        }


    }
}