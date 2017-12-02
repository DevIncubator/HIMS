using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.WebMVC.Models;

namespace HIMS.WebMVC.Controllers
{
    public class SampleController : Controller
    {
        ISampleService _sampleService;

        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            IEnumerable<SampleTransferModel> sampleDtos = _sampleService.GetSamples();
            var samples = Mapper.Map<IEnumerable<SampleTransferModel>, List<SampleViewModel>>(sampleDtos);

            return View(samples);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Description")]SampleViewModel sample)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sampleDto = Mapper.Map<SampleViewModel, SampleTransferModel>(sample);
                    _sampleService.SaveSample(sampleDto);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(sample);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SampleTransferModel sampleDto = _sampleService.GetSample(id);

            if (sampleDto == null)
            {
                return HttpNotFound();
            }

            var sample = Mapper.Map<SampleTransferModel, SampleViewModel>(sampleDto);
            return View(sample);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sampleDto = _sampleService.GetSample(id);

            if (TryUpdateModel(sampleDto, "",
                new string[] { nameof(sampleDto.Name), nameof(sampleDto.Description) }))
            {
                try
                {
                    _sampleService.UpdateSample(sampleDto);

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            var sample = Mapper.Map<SampleTransferModel, SampleViewModel>(sampleDto);
            return View(sample);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SampleTransferModel sampleDto = _sampleService.GetSample(id);

            if (sampleDto == null)
            {
                return HttpNotFound();
            }

            var sample = Mapper.Map<SampleTransferModel, SampleViewModel>(sampleDto);
            return View(sample);
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

            SampleTransferModel sampleDto = _sampleService.GetSample(id);

            if (sampleDto == null)
            {
                return HttpNotFound();
            }

            var sample = Mapper.Map<SampleTransferModel, SampleViewModel>(sampleDto);

            return View(sample);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _sampleService.DeleteSample(id);
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}