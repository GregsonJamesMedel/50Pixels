using System.Threading.Tasks;
using _50Pixels.Models;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFileProcessor _fileProcessor;
        private readonly IPhotoService _photoService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                IHttpContextAccessor httpContextAccessor,
                                IPhotoService photoService,
                                IFileProcessor fileProcessor)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._photoService = photoService;
            this._fileProcessor = fileProcessor;
        }

        [HttpGet]
        public IActionResult SignUp() => View();
        

        [HttpPost]
        public async Task<IActionResult> SignUp(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //save profile pic
                string photoFileName = "";

                if (vm.Photo != null)
                    photoFileName = _fileProcessor.SavePhoto(vm.Photo,"ProfilePics");

                var user = new ApplicationUser()
                {
                    Email = vm.Email,
                    UserName = vm.Email,
                    Firstname = vm.Firstname,
                    Lastname = vm.Lastname,
                    PhotoPath = photoFileName
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
        public IActionResult SignIn() => View();
        

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