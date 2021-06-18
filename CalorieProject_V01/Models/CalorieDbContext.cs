
/**
 * 
 * CaloriedDbContext
 * 
 * John McKinney 01/06/2021
 * 
 * 
 *  This class declares the DbSets to be used in the app
 *  IMPORTANTLY It also names the database  connection string - "DefaultConnection"
 * 
 *   Categories ,  FoodItems,  Messages
 * 
 */



using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace CalorieProject_V01.Models
{
    public class CalorieDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Message> Messages { get; set; }

        public CalorieDbContext()
                : base("DefaultConnection", throwIfV1Schema: false)
            {
                Database.SetInitializer(new DatabaseInitializer());
            }

            public static CalorieDbContext Create()
            {
                return new CalorieDbContext();
            }
     
    }
}