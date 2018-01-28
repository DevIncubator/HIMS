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
	class VTaskRepository : IRepository <VTask>
	{
		private HIMSDataContext db;

		public VTaskRepository(HIMSDataContext context)
		{
			this.db = context;
		}

		public IEnumerable<VTask> GetAll()
		{
			return db.VTasks;
		}

		public VTask Get(int id)
		{
			return db.VTasks.Find(id);
		}

		public IEnumerable<VTask> Find(Func<VTask, Boolean> predicate)
		{
			return db.VTasks.Where(predicate).ToList();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Create(VTask item)
		{
			throw new NotImplementedException();
		}
	}
}
