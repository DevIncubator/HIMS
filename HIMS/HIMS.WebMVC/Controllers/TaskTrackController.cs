﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.WebMVC.Models;
using System.Data.Entity.Infrastructure;

namespace HIMS.WebMVC.Controllers
{
    public class TaskTrackController : Controller
    {
        private readonly ITaskTrackService _taskTrackService;
        private readonly IVUserTrackService _vUserTrackService;
        private readonly IVUserProfileService _vUserProfileService;
        private readonly IVUserTaskService _vUserTaskService;
        private readonly IUserTaskService _userTaskTService;

        public TaskTrackController(ITaskTrackService taskTrackService, IVUserTrackService vUerTrackService, IVUserProfileService vUserProfileService, IVUserTaskService vUserTaskService, IUserTaskService userTaskService)
        {
            _taskTrackService = taskTrackService;
            _vUserTrackService = vUerTrackService;
            _vUserProfileService = vUserProfileService;
            _vUserTaskService = vUserTaskService;
            _userTaskTService = userTaskService;
        }

        public ActionResult Edit(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskTrackDTO = _vUserTrackService.Get(id.Value);

            if (taskTrackDTO == null)
            {
                return HttpNotFound();
            }

            var item = Mapper.Map<VUserTrackTransferModel, TaskTrackEditViewModel>(taskTrackDTO);
            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditTaskTrack(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }

            var taskTrackDTO = _vUserTrackService.Get(id);

            if (TryUpdateModel(taskTrackDTO, "",
                new string[] {nameof(taskTrackDTO.TrackNote) }))
            {
                try
                {
                    _vUserTrackService.UpdateUserTrack(taskTrackDTO);
                    return RedirectToAction("Index");

                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            var taskTrack = Mapper.Map<VUserTrackTransferModel, TaskTrackEditViewModel>(taskTrackDTO);
            return View(taskTrack);
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

            VUserTrackTransferModel userTrack = _vUserTrackService.GetByTaskTrack(id.Value);

            if (userTrack == null)
            {
                return HttpNotFound();
            }

            var taskTrack = Mapper.Map<VUserTrackTransferModel, VUserTrackViewModel>(userTrack);

            return View(taskTrack);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var item = _taskTrackService.GetTaskTrack(id);
                if (item != null)
                {
                    _taskTrackService.DeleteTaskTrack(id);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
            }
            catch (RetryLimitExceededException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var taskTrackDto = _vUserTrackService.Get(id.Value);

            if (taskTrackDto == null)
            {
                return HttpNotFound();
            }

            var taskTrack = Mapper.Map<VUserTrackTransferModel, TaskTrackEditViewModel>(taskTrackDto);
            return View(taskTrack);
        }
        
        public ActionResult Create(int? taskId)
        {
            if (taskId != null)
            {
                var userIdentityName = User.Identity.Name;
                var user = _vUserProfileService.GetVUserProfile(userIdentityName);
                var task = _vUserTaskService.GetTaskForUser(user.UserId, taskId.Value);
                var userTaskModel = _userTaskTService.Get(user.UserId, task.TaskId);
                TaskTrackViewModel viewModel = new TaskTrackViewModel
                {
                    Name = task.TaskName,
                    TrackDate = DateTime.Now,
                    TaskId = task.TaskId,
                    UserId = user.UserId,
                    UserTaskId = userTaskModel.UserTaskId
                };
                return View(viewModel);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrackNote, UserTaskId")] TaskTrackViewModel taskTrack)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var taskTrackDTO = Mapper.Map<TaskTrackViewModel, TaskTrackTransferModel>(taskTrack);
                    _taskTrackService.SaveTaskTrack(taskTrackDTO);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(taskTrack);
        }

        public ActionResult Index(int? userId)
        {   
            IEnumerable<VUserTrackTransferModel> taskTracksDto;
            if (userId.HasValue)
            {
                taskTracksDto = _vUserTrackService.GetVUserTrack(userId);
            }
            else
            {
                var userIdentityName = User.Identity.Name;
                var user = _vUserProfileService.GetVUserProfile(userIdentityName);
                taskTracksDto = _vUserTrackService.GetVUserTrack(user.UserId);
            }
            
            var taskTracks = new TaskTracksListViewModel
            {
                TaskTracks = Mapper.Map<IEnumerable<VUserTrackTransferModel>, List<VUserTrackViewModel>>(taskTracksDto)
            };
            return View(taskTracks);
        }


    }
}