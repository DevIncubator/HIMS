﻿using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIMS.WebMVC.Controllers
{
    public class UserTaskController : Controller
    {
        private readonly IUserTaskService _userTaskService;

        public UserTaskController(IUserTaskService userTaskService)
        {
            _userTaskService = userTaskService;
        }

        public ActionResult GetTasksForUser([Bind(Include ="id")]int id)
        {
            var tasks = _userTaskService.GetAllUserTasks(id).ToList();
            return View(Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(tasks));
        }

        [HttpPost]
        public ActionResult SetTaskAsSuccess([Bind(Include = "userId, taskId, isSuccess")]int userId, int taskId, bool isSuccess)
        {

            _userTaskService.UpdateUserTaskStatus(userId, taskId, isSuccess);
            var tasks = _userTaskService.GetAllUserTasks(userId).ToList();

            return PartialView("_GetTasksForUser", Mapper.Map<IEnumerable<UserTaskTransferModel>, List<UserTaskViewModel>>(tasks));
        }
        // GET: UserTask
        public ActionResult Index()
        {
            return View();
        }
    }
}