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
        private readonly IVUserProfileService _vUserProfileService;

        public UserTaskController(IVUserTaskService service, IUserProfileService serviceU, IVUserProfileService vUserProfileService)
        {
            _service = service;
            _serviceU = serviceU;
            _vUserProfileService = vUserProfileService;

        }

        public ActionResult GetTasksForUser(int? id)
        {
            UserTasksListViewModel tasks;

            var userIdentityName = User.Identity.Name;
            var user = _vUserProfileService.GetVUserProfile(userIdentityName);

            if (id.HasValue)
            {
                if (user.UserId != id.Value && !User.IsInRole("admin"))
                    return new HttpStatusCodeResult(401);

                var userDtos = _service.GetAllTasksForUser(id).ToList();
                var userName = _serviceU.GetUserProfile(id).Name;
                tasks = new UserTasksListViewModel
                    {
                        UserTasksList = Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(userDtos),
                        UserName = userName
                    };
            }
            else
            { 
                IEnumerable<UserTaskTransferModel> userDtos = _service.GetAllTasksForUser(user.UserId);
                var userName = user.FullName;
                tasks = new UserTasksListViewModel
                {
                    UserTasksList = Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(userDtos),
                    UserName = userName
                };
            }
            return View(tasks);
        }

        [Authorize(Roles = "admin")]
        //[HttpPost]
        public ActionResult SetTaskAsSuccess(int userId, int taskId, bool isSuccess)
        {
            _service.UpdateTaskStatusForUser(userId, taskId, isSuccess);
            var tasksDTO = _service.GetAllTasksForUser(userId).ToList();

            var userName = _serviceU.GetUserProfile(userId).Name;
            var tasks = new UserTasksListViewModel
            {
                UserTasksList = Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(tasksDTO),
                UserName = userName
            };

            //var tasks = Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(tasksDTO);
            return View("GetTasksForUser",tasks);
        }
       
    }
}