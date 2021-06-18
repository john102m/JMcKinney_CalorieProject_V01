/**
 * 
 * Caategory class
 * 
 * John McKinney 01/06/2021
 * 
 * 
 * This class defines the fields/properties of the categories
 * 
 */


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalorieProject_V01.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Food Category")]
        public string Name { get; set; }

        public string ImgFilePath { get; set; }

        //navigational property
        public List<FoodItem> MenuItems { get; set; }
    }


}