using AutoMapper;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Interfaces;
using HIMS.WebMVC.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HIMS.WebMVC.Controllers
{
    public class UserProgressController : Controller
    {
        private readonly IVUserProgressService _vUserProgressService;
        private readonly IVUserProfileService _vUserProfileService;

        public UserProgressController(IVUserProgressService vUserProgressService, IVUserProfileService vUserProfileService)
        {
            _vUserProgressService = vUserProgressService;
            _vUserProfileService = vUserProfileService;
        }

        [HttpPost]
        public ActionResult Index(int? userId)
        {
            IEnumerable<VUserProgressTransferModel> userProgressDTO = null;
            string userName = "";

            if (userId.HasValue)
            {
                userProgressDTO = _vUserProgressService.GetProgressByUserId(userId.Value);
                userName = _vUserProgressService.GetUserNameById(userId.Value);
            }
            else
            {
                var userIdentityName = User.Identity.Name;
                var user = _vUserProfileService.GetVUserProfile(userIdentityName);
                if (user != null)
                {
                    userProgressDTO = _vUserProgressService.GetProgressByUserId(user.UserId);
                    userName = _vUserProgressService.GetUserNameById(user.UserId);
                }
            }

            var userProgress = new UserProgressListViewModel
            {
                UserProgressList = Mapper.Map<IEnumerable<VUserProgressTransferModel>, List<UserProgressViewModel>>(userProgressDTO),
                UserName = userName
            };

            ViewBag.Title = $"{userName}'s progress";
            return View(userProgress);
        }
    }
}