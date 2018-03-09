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

        public ActionResult Index(int? userId)
        {
            IEnumerable<VUserProgressTransferModel> userProgressDTO;

            if (userId.HasValue)
            {
                userProgressDTO = _vUserProgressService.GetProgressByUserId(userId.Value);
            }
            else
            {
                var userIdentityName = User.Identity.Name;
                var user = _vUserProfileService.GetVUserProfile(userIdentityName);
                userProgressDTO = _vUserProgressService.GetProgressByUserId(user.UserId);
            }

            var userProgress = new UserProgressListViewModel
            {
                UserProgressList = Mapper.Map<IEnumerable<VUserProgressTransferModel>, List<UserProgressViewModel>>(userProgressDTO)
            };

            return View(userProgress);
        }
    }
}