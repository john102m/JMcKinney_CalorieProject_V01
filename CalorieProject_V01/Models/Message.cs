/**
 * 
 * Message class
 * 
 * John McKinney 01/06/2021
 * 
 * 
 * This class defines the properties of a message
 * 
 * 
 */




using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalorieProject_V01.Models
{
    public class Message
    {

        //email enquiry form at it's simplest - non logged in and non members may use it

        [Key]
        public int MessageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string MessagePost { get; set; }

        [Display(Name = "Receive newsletter")]
        public bool Subscribe { get; set; }

        [Display(Name = "Date Posted")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")] 
        public DateTime DatePosted { get; set; }

    }
}