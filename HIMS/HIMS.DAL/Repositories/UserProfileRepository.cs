using HIMS.Data;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.DAL.Repositories
{
    public class UserProfileRepository : IRepository<UserProfile>
    {
        private HIMSDataContext _db;

        public UserProfileRepository(HIMSDataContext context)
        {
            _db = context;
        }

        public void Create(UserProfile item)
        {
            _db.UserProfiles.Add(new UserProfile
            {
                UserId = 0,
                Name = item.Name,
                LastName = item.LastName,
                DirectionId = item.DirectionId,
                Education = item.Education,
                Address = item.Address,
                Email = item.Email,
                BirthDate = item.BirthDate,
                MathScore = item.MathScore,
                MobilePhone = item.MobilePhone,
                Sex = item.Sex,
                Skype = item.Skype,
                StartDate = item.StartDate,
                UniversityAverageScore = item.UniversityAverageScore,
            });
        }

        public void Delete(int id)
        {
            UserProfile userProfile = _db.UserProfiles.Find(id);
            if (userProfile != null)
                _db.CallSpDeleteUser(id); //directly call the stored-procedure-method from LLBLGen auto generated class HIMSDataContext
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return _db.UserProfiles.Where(predicate);
        }

        public UserProfile Get(int id)
        {
            return _db.UserProfiles.Find(id);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return _db.UserProfiles;
        }
    }
}
