using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HIMS.WebMVC.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly IUserProfileService _userProfileService;
        private readonly IVUserProfileService _vUserProfileService;
        private readonly IDirectionService _directionService;

        public UserProfileController(IUserProfileService userProfileService, 
                                     IVUserProfileService vUserProfileService,
                                     IDirectionService directionService)
        {
            _userProfileService = userProfileService;
            _vUserProfileService = vUserProfileService;
            _directionService = directionService;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            IEnumerable<VUserProfileTransferModel> vUserProfileDtos = _vUserProfileService.GetVUserProfiles();
            List<UserProfileGridViewModel> userProfiles = 
                Mapper.Map<IEnumerable<VUserProfileTransferModel>, List<UserProfileGridViewModel>>(vUserProfileDtos);

            return View(userProfiles);
        }

        public ActionResult Create()
        {
            ViewBag.DirectionId = GetDirections();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfileDetailsViewModel userProfile) //[Bind(Include = "Name, Description")]
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userProfileDto = Mapper.Map<UserProfileDetailsViewModel, UserProfileTransferModel>(userProfile);
                    _userProfileService.SaveUserProfile(userProfileDto);
                    return RedirectToAction("Index");
                }
                   
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.DirectionId = GetDirections();
            return View(userProfile);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserProfileTransferModel userProfileDto = _userProfileService.GetUserProfile(id);

            if (userProfileDto == null)
            {
                return HttpNotFound();
            }

            var userProfile = Mapper.Map<UserProfileTransferModel, UserProfileDetailsViewModel>(userProfileDto);

            ViewBag.DirectionId = GetDirections();

            return View(userProfile);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userProfileDto = _userProfileService.GetUserProfile(id);

            if (TryUpdateModel(userProfileDto))//, "",
                //new string[] { nameof(userProfileDto.Name), nameof(userProfileDto.Description) }))
            {
                try
                {
                    _userProfileService.UpdateUserProfile(userProfileDto);

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewBag.DirectionId = GetDirections();
            var userProfile = Mapper.Map<UserProfileTransferModel, UserProfileDetailsViewModel>(userProfileDto);
            return View(userProfile);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserProfileTransferModel userProfileDto = _userProfileService.GetUserProfile(id);

            if (userProfileDto == null)
            {
                return HttpNotFound();
            }

            var userProfile = Mapper.Map<UserProfileTransferModel, UserProfileDetailsViewModel>(userProfileDto);

            var direction = Mapper.Map<DirectionTransferModel, DirectionViewModel>(_directionService.GetDirection(userProfileDto.DirectionId));
            
            ViewBag.Direction = direction.Name;

            return View(userProfile);
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

            UserProfileTransferModel userProfileDto = _userProfileService.GetUserProfile(id);

            if (userProfileDto == null)
            {
                return HttpNotFound();
            }

            var userProfile = Mapper.Map<UserProfileTransferModel, UserProfileDetailsViewModel>(userProfileDto);

            var direction = Mapper.Map<DirectionTransferModel, DirectionViewModel>(_directionService.GetDirection(userProfileDto.DirectionId));

            ViewBag.Direction = direction.Name;

            return View(userProfile);
        }

        // POST: UserProfile/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _userProfileService.DeleteUserProfile(id);
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }


        private List<SelectListItem> GetDirections()
        {
            //get Directions for DropDownList
            var directions = Mapper.Map<IEnumerable<DirectionTransferModel>, List<DirectionViewModel>>(_directionService.GetDirections());
            List<SelectListItem> selectItems = new List<SelectListItem>();
            foreach (var item in directions)
            {
                selectItems.Add(new SelectListItem { Text = item.Name, Value = item.DirectionId.ToString() });
            }
            return selectItems;
        }
    }
}
