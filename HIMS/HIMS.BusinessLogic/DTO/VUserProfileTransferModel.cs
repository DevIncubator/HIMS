using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BusinessLogic.DTO
{
    public class VUserProfileTransferModel
    {
        public System.String Address { get; set; }
        public Nullable<System.Int32> Age { get; set; }
        public System.String Direction { get; set; }
        public System.String Education { get; set; }
        public System.String Email { get; set; }
        public System.String FullName { get; set; }
        public Nullable<System.Double> MathScore { get; set; }
        public System.String MobilePhone { get; set; }
        public System.String Sex { get; set; }
        public System.String Skype { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.Double> UniversityAverageScore { get; set; }
        public System.Int32 UserId { get; set; }
        public string Password { get; set; }
    }
}
