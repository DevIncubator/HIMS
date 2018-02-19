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
    class DirectionRepository : IRepository<Direction>
    {
        private HIMSDataContext db;

        public DirectionRepository(HIMSDataContext context)
        {
            this.db = context;
        }

        public IEnumerable<Direction> GetAll()
        {
            return db.Directions;
        }

        public Direction Get(int id)
        {
            return db.Directions.Find(id);
        }

        public void Create(Direction item)
        {
            db.Directions.Add(item);
        }

        public void Delete(int id)
        {
            Direction item = db.Directions.Find(id);
            if (item != null)
                db.Directions.Remove(item);
        }

        public IEnumerable<Direction> Find(Func<Direction, bool> predicate)
        {
            return db.Directions.Where(predicate).ToList();
        }
    }
}
