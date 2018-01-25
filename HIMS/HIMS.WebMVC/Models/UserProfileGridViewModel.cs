using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIMS.WebMVC.Models
{
    public class UserProfileGridViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public System.Int32 UserId { get; set; }
        [Display(Name = "Full Name")]
        public System.String FullName { get; set; }
        public System.String Direction { get; set; }
        public System.String Education { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime Start { get; set; }
        public Nullable<System.Int32> Age { get; set; }
    }
}