/**
 * 
 * Userclass
 * 
 * John McKinney 01/06/2021
 * 
 * 
 * This class defines the properties of an individual user view
 * 
 * 
 */


using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieProject_V01.Models
{
    public class UserViewModel
    {
       
        [Display(Name = "Username")]
        public string UserName { get; set; }

        // since we're only storing one piece of info about the user just add it as a property here
        [Display(Name = "Total Calories")]
        public int TotalCalories { get; set; }
    }
}