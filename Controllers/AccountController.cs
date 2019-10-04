using System.Linq;
using System.Threading.Tasks;
using _50Pixels.Models;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace _50Pixels.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFileProcessor _fileProcessor;
        private readonly IPhotoService _photoService;
        private readonly IUserSessionService _userSessionService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                IHttpContextAccessor httpContextAccessor,
                                IUserSessionService userSessionService,
                                IPhotoService photoService,
                                IFileProcessor fileProcessor)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._photoService = photoService;
            this._fileProcessor = fileProcessor;
            this._userSessionService = userSessionService;
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
        public async Task<IActionResult> SignIn(AccountSignInViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, isPersistent: false, false);

                if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }

        public async Task<IActionResult> SignOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile(string id, int page = 1)
        {
            var appUser = _userManager.FindByIdAsync(id);
            var vm = new AccountProfileViewModel();

            vm.ApplicationUser = appUser.Result;

            var photos = _photoService.GetPhotosByUploaderId(id);
            vm.Photos = PagingList.Create(photos,photos.Count(),page);
            
            vm.IsCurrentUserProfile = appUser.Result.Id == _userSessionService.GetCurrentUserID();
            return View(vm);
        }
    }
}