using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using _50Pixels.Models;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                IHttpContextAccessor httpContextAccessor,
                                IPhotoService photoService,
                                IHostingEnvironment hostingEnvironment)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._photoService = photoService;
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //save profile pic
                string uniqueFileName = "";

                if (vm.Photo != null)
                {
                    string uploadsFoler = Path.Combine(_hostingEnvironment.WebRootPath, "ProfilePics");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;
                    string filePath = Path.Combine(uploadsFoler, uniqueFileName);
                    vm.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                var user = new ApplicationUser()
                {
                    Email = vm.Email,
                    UserName = vm.Email,
                    Firstname = vm.Firstname,
                    Lastname = vm.Lastname,
                    PhotoPath = uniqueFileName
                };

                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AccountSignInViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, isPersistent: false, false);
                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }

        public async Task<IActionResult> SignOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile(string id)
        {
            var appUser = _userManager.FindByIdAsync(id);
            var vm = new AccountProfileViewModel();
            vm.ApplicationUser = appUser.Result;
            vm.Photos = _photoService.GetPhotosByUploaderId(id);
            return View(vm);
        }

    }
}