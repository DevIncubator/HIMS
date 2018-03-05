using System;
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

        public TaskTrackController(ITaskTrackService taskTrackService, IVUserTrackService vUerTrackService)
        {
            _taskTrackService = taskTrackService;
            _vUserTrackService = vUerTrackService;
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

            var item = Mapper.Map<VUserTrackTransferModel, TaskTrackViewModel>(taskTrackDTO);
            return View(item);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditTaskTrack(int? id)//update TaskTrack
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

            var taskTrack = Mapper.Map<VUserTrackTransferModel, TaskTrackViewModel>(taskTrackDTO);
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

            TaskTrackTransferModel taskTrackDTO = _taskTrackService.GetTaskTrack(id);

            if (taskTrackDTO == null)
            {
                return HttpNotFound();
            }

            var taskTrack = Mapper.Map<TaskTrackTransferModel, TaskTrackViewModel>(taskTrackDTO);

            return View(taskTrack);

        }

        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Delete(int id)
        {
            try
            {
                _taskTrackService.DeleteTaskTrack(id);
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
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

            var taskTrack = Mapper.Map<VUserTrackTransferModel, TaskTrackViewModel>(taskTrackDto);
            return View(taskTrack);
        }

        public ActionResult Create(int taskId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Note")] TaskTrackViewModel taskTrack)
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
            var taskTracksDTO = _vUserTrackService.GetVUserTrack(userId);
            var taskTracks = new TaskTracksListViewModel
            {
                TaskTracks = Mapper.Map<IEnumerable<VUserTrackTransferModel>, List<TaskTrackViewModel>>(taskTracksDTO)
            };
            return View(taskTracks);
        }


    }
}