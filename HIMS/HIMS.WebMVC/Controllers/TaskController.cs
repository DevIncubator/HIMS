﻿using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.WebMVC.Models;
using HIMS.WebMVC.Utils;

namespace HIMS.WebMVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IVUserProfileService _vUserProfileService;

        public TaskController(ITaskService taskService,
            IVUserProfileService vUserProfileService)
        {
            _taskService = taskService;
            _vUserProfileService = vUserProfileService;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            IEnumerable<TaskTransferModel> taskDtos = _taskService.GetAllTasks();
            var tasks = new TaskListViewModel
            {
                Tasks = Mapper.Map<IEnumerable<TaskTransferModel>, List<TaskViewModel>>(taskDtos),
              
            };

            return View(tasks);
        }

        public ActionResult Create()
        {
            ViewBag.UserList = GetUsers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Description, Start, Deadline")]TaskViewModel task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var taskDto = Mapper.Map<TaskViewModel, TaskTransferModel>(task);
                    _taskService.SaveTask(taskDto);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(task);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskTransferModel taskDto = _taskService.GetTask(id);

            if (taskDto == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserList = GetUsers();
            var task = Mapper.Map<TaskTransferModel, TaskViewModel>(taskDto);
            return View(task);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskDto = _taskService.GetTask(id);

            if (TryUpdateModel(taskDto))
                //"",
                //new string[] { nameof(sampleDto.Name), nameof(sampleDto.Description) }))
            {
                try
                {
                    _taskService.UpdateTask(taskDto);

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            var task = Mapper.Map<TaskTransferModel, TaskViewModel>(taskDto);
            return View(task);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskTransferModel taskDto = _taskService.GetTask(id);

            if (taskDto == null)
            {
                return HttpNotFound();
            }

            var task = Mapper.Map<TaskTransferModel, TaskViewModel>(taskDto);
            return View(task);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }

            TaskTransferModel taskDto = _taskService.GetTask(id);

            if (taskDto == null)
            {
                return HttpNotFound();
            }

            var task = Mapper.Map<TaskTransferModel, TaskViewModel>(taskDto);
            return View(task);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _taskService.DeleteTask(id);
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetUsers()
        {
            
            var users = Mapper.Map<IEnumerable<VUserProfileTransferModel>, List<UserProfileGridViewModel>>(_vUserProfileService.GetVUserProfiles());
            List<SelectListItem> selectItems = new List<SelectListItem>();
            foreach (var item in users)
            {
                selectItems.Add(new SelectListItem { Text = item.FullName, Value = item.UserId.ToString() });
            }
            return selectItems;
        }
    }
}
