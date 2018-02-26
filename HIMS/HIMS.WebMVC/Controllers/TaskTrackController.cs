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

        public TaskTrackController(ITaskTrackService taskTrackService)
        {
            _taskTrackService = taskTrackService;
        }

        public ActionResult EditTaskTrack(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskTrackTransferModel taskTrackDTO = _taskTrackService.GetTaskTrack(id);

            if (taskTrackDTO == null)
            {
                return HttpNotFound();
            }

            var item = Mapper.Map<TaskTrackTransferModel, TaskTrackViewModel>(taskTrackDTO);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTaskTrack()//update TaskTrack
        {
            return View();
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

            TaskTrackTransferModel taskTrackDTO = _taskTrackService.GetTaskTrack(id);

            if (taskTrackDTO == null)
            {
                return HttpNotFound();
            }

            var taskTrack = Mapper.Map<TaskTrackTransferModel, TaskTrackViewModel>(taskTrackDTO);
            return View(taskTrack);
        }
        public ActionResult Index(int userId)
        {
            IEnumerable<TaskTrackTransferModel> taskTrackDTOs = _taskTrackService.GetTaskTracks(userId);
            var taskTracks = new 
            return View();
        }


    }
}