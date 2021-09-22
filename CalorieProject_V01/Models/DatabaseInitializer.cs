/**
 * 
 * Database Initializer class
 * 
 * John McKinney 01/06/2021
 * 
 * 
 * This class initilaizes the database
 * The database is seeded with several food items and categories 
 * It is also seeded with an admin user and a member user
 * Also included in the seed are acouple of test messages
 * 
 */

using System;
using System.Collections.Generic;
using System.Data.Entity;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Linq;
using System.Web;
using System.Data.Entity.Validation;

namespace CalorieProject_V01.Models
{
    //DropCreateDatabaseAlways
    //DropCreateDatabaseIfModelChanges
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<CalorieDbContext>
        
    {
        protected override void Seed(CalorieDbContext context)
        {
            if (!context.Users.Any())  //,,,  
            {

                //================================================================
                // create roles and store in ASpNet Roles
                //=================================================================
                // must create a role manager object to create roles
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    //creat an admin role
                    roleManager.Create(new IdentityRole("Admin"));
                }


                if (!roleManager.RoleExists("Member"))
                {
                    //creat an admin role
                    roleManager.Create(new IdentityRole("Member"));
                }

                context.SaveChanges();


                //seed the categories table
                //create a   few catogories
                var cat1 = new Category() { Name = "Fruit", ImgFilePath = "fruit.png" };
                var cat2 = new Category() { Name = "Cereal", ImgFilePath = "cereal.png" };
                var cat3 = new Category() { Name = "Meat", ImgFilePath = "meat.png" };
                var cat4 = new Category() { Name = "Fish", ImgFilePath = "fish.png" };
                var cat5 = new Category() { Name = "Dairy", ImgFilePath = "dairy.png" };
                var cat6 = new Category() { Name = "Vegetables", ImgFilePath = "vegetables.png" };


                context.Categories.Add(cat1);
                context.Categories.Add(cat2);
                context.Categories.Add(cat3);
                context.Categories.Add(cat4);
                context.Categories.Add(cat5);
                context.Categories.Add(cat6);

                context.SaveChanges();

                //seed db with food items

                var item1 = new FoodItem()
                {
                    ItemName = "Apple",
                    Description = "Lots of fibre",
                    Calories = 52,
                    ImgFilePath = "apple.png",
                    Category = cat1

                };

                var item2 = new FoodItem()
                {
                    ItemName = "Banana",
                    Description = "Important source of potassium",
                    Calories = 89,
                    ImgFilePath = "banana.png",
                    Category = cat1

                };
                var item3 = new FoodItem()
                {
                    ItemName = "Orange",
                    Description = "Important source of vitamin C",
                    Calories = 47,
                    ImgFilePath = "orange.png",
                    Category = cat1

                };

                var item4 = new FoodItem()
                {
                    ItemName = "Salmon",
                    Description = "Important source of omega 3",
                    Calories = 208,
                    ImgFilePath = "salmon.png",
                    Category = cat4

                };
                var item5 = new FoodItem()
                {
                    ItemName = "Tuna",
                    Description = "Important source of protein",
                    Calories = 132,
                    ImgFilePath = "tuna.png",
                    Category = cat4

                };
                var item6 = new FoodItem()
                {
                    ItemName = "Cod",
                    Description = "Fresh from the North Sea",
                    Calories = 190,
                    ImgFilePath = "cod.png",
                    Category = cat4

                };
                var item7 = new FoodItem()
                {
                    ItemName = "Carrots",
                    Description = "Good for the eyesight",
                    Calories = 82,
                    ImgFilePath = "carrots.png",
                    Category = cat6

                };
                var item8 = new FoodItem()
                {
                    ItemName = "Chicken Breast",
                    Description = "Cooked in oven",
                    Calories = 189,
                    ImgFilePath = "chickenbreast.png",
                    Category = cat3

                };
                var item9 = new FoodItem()
                {
                    ItemName = "Cheese",
                    Description = "Low fat variety",
                    Calories = 282,
                    ImgFilePath = "cheese.png",
                    Category = cat5

                };
                var item10 = new FoodItem()
                {
                    ItemName = "Cornflakes",
                    Description = "Delicious with low fat milk",
                    Calories = 152,
                    ImgFilePath = "cornflakes.png",
                    Category = cat2

                };
                var item11 = new FoodItem()
                {
                    ItemName = "Muesli",
                    Description = "Full of fruit and nuts",
                    Calories = 182,
                    ImgFilePath = "muesli.png",
                    Category = cat2

                };

                var item12 = new FoodItem()
                {
                    ItemName = "Bass",
                    Description = "Fresh from the Atlantic Sea",
                    Calories = 190,
                    ImgFilePath = "bass.png",
                    Category = cat4

                };
                var item13 = new FoodItem()
                {
                    ItemName = "Sardines",
                    Description = "Full of omega 3 oil",
                    Calories = 190,
                    ImgFilePath = "sardines.png",
                    Category = cat4

                };
                var item14 = new FoodItem()
                {
                    ItemName = "Pear",
                    Description = "Very Juicy",
                    Calories = 82,
                    ImgFilePath = "pear.png",
                    Category = cat1

                };
                var item15 = new FoodItem()
                {
                    ItemName = "Mango",
                    Description = "Exotic tropical fruit",
                    Calories = 82,
                    ImgFilePath = "mango.png",
                    Category = cat1

                };

                var item16 = new FoodItem()
                {
                    ItemName = "Peach",
                    Description = "Extremely juicy",
                    Calories = 182,
                    ImgFilePath = "peach.png",
                    Category = cat1

                };
                var item17 = new FoodItem()
                {
                    ItemName = "Potatoes",
                    Description = "Source of carbohydrates",
                    Calories = 282,
                    ImgFilePath = "potatoes.png",
                    Category = cat6

                }; 
                var item18 = new FoodItem()
                {
                    ItemName = "Green Beans",
                    Description = "Source of vitamin E",
                    Calories = 110,
                    ImgFilePath = "greenbeans.png",
                    Category = cat6

                };
                var item19 = new FoodItem()
                {
                    ItemName = "Brussels Sprouts",
                    Description = "Great source of iron",
                    Calories = 180,
                    ImgFilePath = "brusselssprouts.png",
                    Category = cat6

                }; 
                var item20 = new FoodItem()
                {
                    ItemName = "Broccoli",
                    Description = "Broccoli is a good source of fibre and protein, and contains iron, potassium and calcium",
                    Calories = 110,
                    ImgFilePath = "broccoli.png",
                    Category = cat6

                };
                var item21 = new FoodItem()
                {
                    ItemName = "Spinach",
                    Description = "Spinach is an extremely nutrient-rich vegetable. It packs high amounts of carotenoids, vitamin C, vitamin K, folic acid, iron, and calcium.",
                    Calories = 100,
                    ImgFilePath = "spinach.png",
                    Category = cat6

                };
                var item22 = new FoodItem()
                {
                    ItemName = "Beef",
                    Description = "Roasted in oven",
                    Calories = 220,
                    ImgFilePath = "beef.png",
                    Category = cat3

                };
                var item23 = new FoodItem()
                {
                    ItemName = "Pork",
                    Description = "Great with crackling",
                    Calories = 320,
                    ImgFilePath = "pork.png",
                    Category = cat3

                };
                var item24 = new FoodItem()
                {
                    ItemName = "Lamb",
                    Description = "Great with mint",
                    Calories = 320,
                    ImgFilePath = "lamb.png",
                    Category = cat3

                };
                var item25 = new FoodItem()
                {
                    ItemName = "Smoked Mackerel",
                    Description = "Oily fish very tasty",
                    Calories = 190,
                    ImgFilePath = "smackerel.png",
                    Category = cat4

                };
                context.FoodItems.Add(item1);
                context.FoodItems.Add(item2);
                context.FoodItems.Add(item3);
                context.FoodItems.Add(item4);
                context.FoodItems.Add(item5);
                context.FoodItems.Add(item6);
                context.FoodItems.Add(item7);
                context.FoodItems.Add(item8);
                context.FoodItems.Add(item9);
                context.FoodItems.Add(item10);
                context.FoodItems.Add(item11);
                context.FoodItems.Add(item12);
                context.FoodItems.Add(item13); 
                context.FoodItems.Add(item14);
                context.FoodItems.Add(item15);
                context.FoodItems.Add(item16);
                context.FoodItems.Add(item17);
                context.FoodItems.Add(item18);
                context.FoodItems.Add(item19);
                context.FoodItems.Add(item20);
                context.FoodItems.Add(item21); 
                context.FoodItems.Add(item22); 
                context.FoodItems.Add(item23);
                context.FoodItems.Add(item24);
                context.FoodItems.Add(item25);
                //save categories to db
                context.SaveChanges();


                //the user manager object allows creating users and storing them in the database
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

                if (userManager.FindByName("admin") == null)
                {

                    // remember the getters and setters?

                    userManager.PasswordValidator = new PasswordValidator()
                    {
                        RequireDigit = false,
                        RequiredLength = 1,
                        RequireLowercase = false,
                        RequireUppercase = false
                    };


                    //creat an admin user
                    var user = new User()
                    {
                        UserName = "admin",
                        Email = "admin@calorie.com"

                    };

                    //add hashed password to user
                    userManager.Create(user, "1000LittleDoggies");
                    //add the user to the role Admin
                    userManager.AddToRole(user.Id, "Admin");
                }

                context.SaveChanges();

                if (userManager.FindByName("johnny") == null)
                {

                    // remember the getters and setters?

                    userManager.PasswordValidator = new PasswordValidator()
                    {
                        RequireDigit = false,
                        RequiredLength = 1,
                        RequireLowercase = false,
                        RequireUppercase = false
                    };


                    //creat a member
                    var user = new User()
                    {
                        UserName = "johnny",
                        Email = "john@calorie.com",

                        TotalCalories = 0
                    };

                    //add hashed password to user
                    userManager.Create(user, "password");
                    //add the user to the role Admin
                    userManager.AddToRole(user.Id, "Member");
                }

                context.SaveChanges();

                Message message = new Message()
                {
                    Name = "John Doe",
                    Email = "john@gmail.com",
                    MessagePost = "Hi I was thinking of joining. Can I get a discount?",
                    Subscribe = false,
                    DatePosted = DateTime.Now

                };
                context.Messages.Add(message);

                Message message2 = new Message()
                {
                    Name = "Jane Doe",
                    Email = "jane@gmail.com",
                    MessagePost = "Please send me loads of emails",
                    Subscribe = true,
                    DatePosted = DateTime.Now

                };
                context.Messages.Add(message2);

                context.SaveChanges();
                base.Seed(context);

            }
        }
    }
}