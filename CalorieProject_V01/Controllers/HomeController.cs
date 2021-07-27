/**
 * 
 * HomeController class
 * 
 * John McKinney 01/06/2021
 * 
 * 
 * This class defines the actions for the app which are presented to non logged on users
 * and also to users who are logged in as "MEMBERS"
 * 
 * 
 */

using CalorieProject_V01.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CalorieProject_V01.Controllers
{
    public class HomeController : Controller
    {
        private CalorieDbContext context = new CalorieDbContext();

        public ActionResult SplashScreen()
        {
            return View();
        }
        public ActionResult Sounds()
        {
            string pathToPodcasts = "~/Sounds/";
            //scan the drive for mp3 and put in list
            //there are not many mp3 files anyway
            //this way it should still work if some got deleted or new ones added  enumerate is quicker for large directories

            List<Sound> sounds = new List<Sound>();
            string[] podcastFilePaths = Directory.GetFiles(HttpContext.Server.MapPath(pathToPodcasts));

            //create sound object for each file and add it to the list
            int count = 1;          
            foreach (string filePath in podcastFilePaths)
            {
                sounds.Add(new Sound
                {
                    //simply name them episode 1, episode 2  etc
                    Name = "Episode " + (count++).ToString(),
                    //extract the correct path and filename
                    FilePath = pathToPodcasts + Path.GetFileName(filePath)
                });

            }

            return View(sounds);
        }

        //Index is essentially the home page where the list of main categories is displayed

        public ActionResult Index()
        {

            // do a reload of all the food items  
            var foodItems = context.FoodItems.ToList();
            foreach(var item in foodItems)
            {
                context.Entry(item).Reload();
            }
            //get all the categories from the db context
            var categories = context.Categories.ToList();

            string User_name = string.Empty;
            string User_color = string.Empty;


            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                //get cookie 
                User_name = reqCookies["UserName"].ToString();
                User_color = reqCookies["UserColor"].ToString();
            }
            else
            {
                //create a cookie
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo.Secure = true;
                userInfo["UserName"] = "Test User";
                userInfo["UserColor"] = "Black";
                userInfo.Expires.Add(new TimeSpan(0, 1, 0));
                Response.Cookies.Add(userInfo);

            }


            return View(categories);
        }

        //ShowCategoryItems is essentially the SUBMENU where individual items within a category are displayed
        public ActionResult ShowCategoryItems(int? id)
        {
            //get all the menu items for the specified category      
            List<FoodItem> items = context.FoodItems.
                Include(b => b.Category).     //where the letter b is some arbtrarily chosen letter - it represents the <Type> in the list
                Where(b => b.CategoryId == id).   
                ToList();
          
            ViewBag.Category = context.Categories.Find(id).Name;       
            return View(items);
        }

        //only members can have calories added
        
        [Authorize(Roles = "Member")]
        public ActionResult AddCalories(int? id)
        {
            if (ModelState.IsValid)
            {               
                FoodItem foodItem = context.FoodItems.Find(id);             

                var userId = User.Identity.GetUserId();
                var user = context.Users.Find(userId);
                
                // the user model has this property
                user.TotalCalories += foodItem.Calories;

                context.SaveChanges();

                //return view with summary info
                foodItem.Category = context.Categories.Find(foodItem.CategoryId);
                ViewBag.TotalCalories = user.TotalCalories;
                return View(foodItem);
            }
            
            return RedirectToAction("Index");
        }

        //show a view of total calories as a form with the option to reset
        [Authorize(Roles = "Member")]
        public ActionResult DailyTotal()
        {
            var userId = User.Identity.GetUserId();
            var user = context.Users.Find(userId);



            //this View model is useful to display a selection of the user properties
            //without exposing all of them to a view
            //this operation could have been done with ViewBag BUT this way is upgradeable
            //because IN THE FUTURE it may be that the user can adjust the calorie amount
            //instead of only resetting it to zero
            UserViewModel userView = new UserViewModel()
            {
                UserName = user.UserName,
                TotalCalories = user.TotalCalories
            };

            //either or  -at this point you already assigned the calories to the view model so you could use that.
            ViewBag.TotalCalories = user.TotalCalories;
            // exposing the whole of the user to a view??
            return View(userView);
        }

        //user has submitted reset form
        [HttpPost]
        [ValidateAntiForgeryToken]  
        //public ActionResult DailyTotal([Bind(Include = "Id, UserName, TotalCalories, Email, SecurityStamp, PasswordHash")] User user)
        public ActionResult DailyTotal(UserViewModel userView)  //not really sending anything back with this form tbf
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = context.Users.Find(userId);

                user.TotalCalories = 0;


                //Note that when you change the state to Modified all the properties of the entity will 
                //be marked as modified and all the property values will be sent to the database when SaveChanges is called.
                //suspect activity if not sumbitting an email
                //context would need to have all the properties attached from the submitted form

                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("DailyTotal");
        }

        //display food item details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            FoodItem item = context.FoodItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Calorie Counter Web App.";

            return View();
        }
        public ActionResult Developer()
        {
            ViewBag.Message = "Developer Info";

            return View();
        }
        public ActionResult Understand()
        {
            ViewBag.Message = "Understanding Calories";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            Message message = new Message();
            ViewBag.Message = "Johnnyboy";          
            return View(message);
        }

        //if user posts a message it is saved to db
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "Name, Email, MessagePost, Subscribe")] Message message)
        {
            //ever heard of a spambot ??
            //// the message posting form could make use of a captcha
            if (ModelState.IsValid)
            {
                message.DatePosted = DateTime.Now;                           
                context.Messages.Add(message);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
            //return RedirectToAction("Index");
        }
        
    }
}