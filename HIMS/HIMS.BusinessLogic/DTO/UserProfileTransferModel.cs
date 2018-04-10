using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BusinessLogic.DTO
{
    public class UserProfileTransferModel
    {
        public System.String Address { get; set; }
        public System.DateTime BirthDate { get; set; }
        public System.Int32 Direction { get; set; }
        public System.String Education { get; set; }
        public System.String Email { get; set; }
        public System.String LastName { get; set; }
        public Nullable<System.Double> MathScore { get; set; }
        public System.String MobilePhone { get; set; }
        public System.String Name { get; set; }
        public System.String Sex { get; set; }
        public System.String Skype { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.Double> UniversityAverageScore { get; set; }
        public System.Int32 UserId { get; set; }
        public System.String Password { get; set; }
    }
}
