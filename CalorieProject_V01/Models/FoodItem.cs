/**
 * 
 * FoodItem class
 * 
 * John McKinney 01/06/2021
 * 
 * 
 * This class defines the properties of an individual food item
 * 
 * 
 */




using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CalorieProject_V01.Models
{
    public class FoodItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [Display(Name ="Food Item")]
        public string ItemName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(1, 1000,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Calories { get; set; }

       
        //[Display(Name = "Image File")]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        //[Required(ErrorMessage = "Please choose file to upload.")]
        public string ImgFilePath { get; set; }

        //navigation property
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}