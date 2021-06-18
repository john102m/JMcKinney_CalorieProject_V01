/**
 * 
 * Userclass
 * 
 * John McKinney 01/06/2021
 * 
 * 
 * This class defines the properties of an individual user
 * 
 * 
 */


using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CalorieProject_V01.Models
{
    // You can add profile data for the user by adding more properties User class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {

        //override the user property because using it for login
        [Required]
        [Display(Name = "Username")]
        public override string UserName { get; set; }

        // since we're only storing one piece of info about the user just add it as a property here
        [Display(Name = "Total Calories")]
        public int TotalCalories { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}