using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HIMS.WebMVC.Controllers
{
    public class UserTaskController : Controller
    {
        private readonly IUserTaskService _service;

        public UserTaskController(IUserTaskService service)
        {
            _service = service;
        }

        public ActionResult GetTasksForUser(int id)
        {
            if (id != null)
            {
                IEnumerable<UserTaskTransferModel> userDtos = _service.GetAllTasksForUser(id);
                var tasks = new UserTasksListViewModel
                {
                    UserTasksList = Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(userDtos)
                };
                return View(tasks);

            }
            else { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult SetTaskAsSuccess(int userId, int taskId, bool isSuccess)
        {
            _service.UpdateTaskStatusForUser(userId, taskId, isSuccess);
            var tasksDTO = _service.GetAllTasksForUser(userId).ToList();
            var tasks = Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(tasksDTO);
            return PartialView("_GetTasksForUser", tasks);
        }
       
    }
}