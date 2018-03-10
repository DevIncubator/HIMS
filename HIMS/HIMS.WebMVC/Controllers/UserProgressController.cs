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

        public UserProgressController(IVUserProgressService vUserProgressService)
        {
            _vUserProgressService = vUserProgressService;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index(int? userId)
        {
            UserProgressListViewModel userProgress = new UserProgressListViewModel();

            if (userId.HasValue)
            {
                IEnumerable<VUserProgressTransferModel> userProgressDTO = _vUserProgressService.GetProgressByUserId(userId.Value);
                string userName = _vUserProgressService.GetUserNameById(userId.Value);

                userProgress.UserProgressList = Mapper.Map<IEnumerable<VUserProgressTransferModel>, List<UserProgressViewModel>>(userProgressDTO);
                userProgress.UserName = userName;
            }

            return View(userProgress);
        }
    }
}