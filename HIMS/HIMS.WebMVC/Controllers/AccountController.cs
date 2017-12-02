using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Security.Claims;
using HIMS.BusinessLogic.DTO;
using HIMS.BusinessLogic.Infrastructure;
using HIMS.BusinessLogic.Interfaces;
using HIMS.BusinessLogic.Services;
using HIMS.WebMVC.Models;


namespace HIMS.WebMVC.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                var userDto = new UserTransferModel { Email = viewModel.Email, Password = viewModel.Password };
                ClaimsIdentity claim = await _userService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Invalid login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Sample");
                }
            }
            return View(viewModel);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Sample");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                var userDto = new UserTransferModel
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    Address = viewModel.Address,
                    Name = viewModel.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await _userService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(viewModel);
        }
        private async Task SetInitialDataAsync()
        {
            await _userService.SetInitialData(new UserTransferModel
            {
                Email = "alex.meleschenko@gmail.com",
                UserName = "Alex",
                Password = "superadmin",
                Name = "Alex",
                Address = "Revoljucionnaja 11-301",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}