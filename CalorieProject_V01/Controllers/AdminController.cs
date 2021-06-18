/**
 * 
 * AdminController class
 * 
 * John McKinney 01/06/2021
 * 
 * 
 * This class defines the actions for the application which are presented to logged in ADMIN users
 * 
 * 
 * 
 */



using CalorieProject_V01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace CalorieProject_V01.Controllers
{
    //only logged in admin users get the funcionality of this controller
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //The image file size is checked before upload on the client side with javascript aswell
        private int MaxFileSize = 512000;
        private CalorieDbContext context = new CalorieDbContext();
        
        // GET: Admin
       

        // this is essentially the Admin Manage Items Main Menu
        // admin users can also view the default main menu
        // this view displays a CREATE NEW button and also edit functionality
        public ActionResult ManageItems()
        {
            //get all the categories
            var categories = context.Categories.ToList();



            // do a reload of all the food items
            // 
            var foodItems = context.FoodItems.ToList();
            foreach (var item in foodItems)
            {
                context.Entry(item).Reload();
            }
            return View(categories);
        }

        //display a list of all the message the users have posted
        public ActionResult ShowMessages()
        {

            var messages = context.Messages.ToList();
            return View(messages);
        }

        public ActionResult ListItems(int? id)
        {
            //get all the menu items for the selected category      
            List<FoodItem> items = context.FoodItems.
                Include(b => b.Category).
                Where(b => b.CategoryId == id).
                ToList();


            ViewBag.Category = context.Categories.Find(id).Name;
            //ViewBag.Categories = context.Categories.ToList();
            return View(items);
        }


        // GET: Member/Create
        public ActionResult Create()
        {
            FoodItem item = new FoodItem();
            ViewBag.CategoryId = new SelectList(context.Categories, "CategoryId", "name");
            return View(item);
        }

        // POST: Member/Create   user may also upload an image of the food item
        // javascript at the front end also checks filesize <= 500kb
        // this would save the user going to the bother of uploading a large file
        // only to have it rejected by the server
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "ItemId, ItemName, Description, Calories, CategoryId")] FoodItem foodItem, HttpPostedFileBase ImgFilePath)
        {

            //admin user is not forced into uploading an image
            //they are allowed to do that later  <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<*********************************************


            if (ModelState.IsValid)
            {
                if (ImgFilePath != null && IsImage(ImgFilePath.FileName) && ImgFilePath.ContentLength < MaxFileSize)
                {

                    ImgFilePath.SaveAs(HttpContext.Server.MapPath("~/Images/") + ImgFilePath.FileName);
                    foodItem.ImgFilePath = ImgFilePath.FileName;
                }
                
                context.FoodItems.Add(foodItem);
                context.SaveChanges();
                return RedirectToAction("ManageItems");
            }
            ViewBag.CategoryId = new SelectList(context.Categories, "CategoryId", "Name", foodItem.CategoryId);
            return View(foodItem);

        }
        // GET: Admin/Edit/5  
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodItem foodItem = context.FoodItems.Find(id);

            //save the file name
            if(foodItem.ImgFilePath != null)
            {
                TempData["OldFileName"] = foodItem.ImgFilePath;
            }
            

            if (foodItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(context.Categories, "CategoryId", "Name", foodItem.CategoryId);
            return View(foodItem);

        }

        // POST: Admin/Edit/5  file edits can replace a file if its already been uploaded
        // however if an edit is commited without specifying a file the the filename will be overwritten    
        // with empty string - the idea would be to see if there was previously a file and keep it (NTS)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId, ItemName, Description, Calories, CategoryId")] FoodItem foodItem, HttpPostedFileBase ImgFilePath)
        {
            
            if (ModelState.IsValid)
            {

                if (ImgFilePath != null && IsImage(ImgFilePath.FileName) && ImgFilePath.ContentLength < MaxFileSize)
                {

                    ImgFilePath.SaveAs(HttpContext.Server.MapPath("~/Images/") + ImgFilePath.FileName);
                    foodItem.ImgFilePath = ImgFilePath.FileName;

                }else if(TempData["OldFileName"] != null) //here if admin user has not specified an image file to upload 
                {                                          //we see if there had already been one uploaded and its filename was 
                                                            // in TempData
                    foodItem.ImgFilePath = (string)TempData["OldFileName"];
                }

                context.Entry(foodItem).State = EntityState.Modified;
                context.SaveChanges();
                Category category = context.Categories.Find(foodItem.CategoryId);
                return RedirectToAction("ListItems", new { id = category.CategoryId } );
            }
            ViewBag.CategoryId = new SelectList(context.Categories, "CategoryId", "Name", foodItem.CategoryId);
            return View(foodItem);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            FoodItem foodItem = context.FoodItems.Find(id);

            if (foodItem == null)
            {
                return HttpNotFound();
            }
            return View(foodItem);
        }


        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodItem foodItem = context.FoodItems.Find(id);
            if (foodItem == null)
            {
                return HttpNotFound();
            }

            return View(foodItem);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")] // this is important, maybe because it's I'm a Londoner
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodItem foodItem = context.FoodItems.Find(id);
            context.FoodItems.Remove(foodItem);
            context.SaveChanges();

            // delete the image from the server if it's found
            // a using directive??
            string fname = HttpContext.Server.MapPath("~/Images/") + foodItem.ImgFilePath;
            if (System.IO.File.Exists(fname))
            {
                System.IO.File.Delete(fname);
            }

            return RedirectToAction("ListItems", new { id = foodItem.CategoryId });
        }

        // GET: Admin/Delete/5
        public ActionResult DeleteMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Message message = context.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            return View(message);
        }

        //hope messages aren't so bad you might have to delete them
        // POST: Admin/DeleteMessage/5
        [HttpPost, ActionName("DeleteMessage")] 
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMessageConfirmed(int id)
        {
            Message message = context.Messages.Find(id);
            context.Messages.Remove(message);
            context.SaveChanges();
            return RedirectToAction("ManageItems");

        }


        // I learned all of this following bit from the internet - god only knows what it means :-)  <------joke
        // courtesy of stackoverflow
        private static bool IsImage(string filename)
        {
            return AllowedFormats.Any(ext => filename.EndsWith(ext,
                       StringComparison.OrdinalIgnoreCase));
        }

        public static List<string> AllowedFormats
        {
            get { return new List<string>() { ".jpg", ".png", ".jpeg" }; }
        }

    }
}