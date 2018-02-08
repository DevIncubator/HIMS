using HIMS.Data;
using HIMS.Data.EntityClasses;
using HIMS.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace HIMS.DAL.Repositories
{
	class TaskRepository : IRepository<Task>
	{
		private HIMSDataContext db;

		public TaskRepository(HIMSDataContext context)
		{
			this.db = context;
		}

		public IEnumerable<Task> GetAll()
		{
			return db.Tasks;
		}

		public Task Get(int id)
		{
			return db.Tasks.Find(id);
		}

		public IEnumerable<Task> Find(Func<Task, Boolean> predicate)
		{
			return db.Tasks.Where(predicate).ToList();
		}

		public void Delete(int id)
		{
			Task item = db.Tasks.Find(id);
			if (item != null)
				db.CallSpDeleteTask(id);
		}

		public void Create(Task item)
		{
			db.Tasks.Add(new Task
			{
				TaskId = 0,
				Name = item.Name,
				Description = item.Description,
				StartDate = item.StartDate,
				DeadlineDate = item.DeadlineDate,
			});
		}
	}
}
