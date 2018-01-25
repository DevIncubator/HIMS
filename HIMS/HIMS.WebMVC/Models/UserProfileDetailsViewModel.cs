using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIMS.WebMVC.Models
{
    public class UserProfileDetailsViewModel
    {
        [HiddenInput]
        public System.Int32 UserId { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Name must be maximum 25 characters long.")]
        public System.String Name { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Last Name must be maximum 25 characters long.")]
        public System.String LastName { get; set; }

        [Required]
        [Display(Name = "Direction")]
        public System.Int32 DirectionId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(125, ErrorMessage = "Email must be maximum 125 characters long.")]
        public System.String Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Address must be maximum 100 characters long.")]
        public System.String Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BirthDate { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Education must be maximum 250 characters long.")]
        public System.String Education { get; set; }

        //not required
        [Range(0, 10, ErrorMessage = "Math Score must be in range from 0 to 10")]
        [Display(Name = "Math Score")]
        public Nullable<System.Double> MathScore { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Please, enter phone number in format +375291112233 (Max 15 characters long)")]
        public System.String MobilePhone { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Please, enter sex in format M(Male) or F(Female)")]
        public System.String Sex { get; set; }

        //not required
        [StringLength(50, ErrorMessage = "Skype must be maximum 50 characters long.")]
        public System.String Skype { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime StartDate { get; set; }

        //not required
        [Range(0, 10, ErrorMessage = "University Average Score must be in range from 0 to 10")]
        [Display(Name = "University Average Score")]
        public Nullable<System.Double> UniversityAverageScore { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Password must be from 3 to 15 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}