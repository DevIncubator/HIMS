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
        private readonly IVUserTaskService _service;
        private readonly IUserProfileService _serviceU;

        public UserTaskController(IVUserTaskService service, IUserProfileService serviceU)
        {
            _service = service;
            _serviceU = serviceU;

        }

        public ActionResult GetTasksForUser(int? id)
        {
            if (id != null)
            {
                IEnumerable<UserTaskTransferModel> userDtos = _service.GetAllTasksForUser(id);
                var userName = _serviceU.GetUserProfile(id).Name;
                var tasks = new UserTasksListViewModel
                    {
                        UserTasksList = Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(userDtos),
                        UserName = userName
                    };
                return View(tasks);
            }
            else return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult SetTaskAsSuccess(int userId, int taskId, bool isSuccess)
        {
            _service.UpdateTaskStatusForUser(userId, taskId, isSuccess);
            var tasksDTO = _service.GetAllTasksForUser(userId).ToList();
            var tasks = Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(tasksDTO);
            return View("GetTasksForUser", tasks);
        }
       
    }
}