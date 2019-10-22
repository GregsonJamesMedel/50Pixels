using _50Pixels.Models;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
                string photoFileName = "no-photo.png";

                if (vm.Photo != null)
                    photoFileName = _fileProcessor.SavePhoto(vm.Photo, "ProfilePics");

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

                if (returnUrl != null && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }

        [Authorize]
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
            vm.LikedPhotos = _photoService.GetLikedPhotos(id);
            vm.Photos = _photoService.GetPhotosByUploaderId(id);
            vm.IsCurrentUserProfile = appUser.Result.Id == _userSessionService.GetCurrentUserID();

            return View(vm);
        }

        public IActionResult Manage(string Id)
        {
            var model = new ManageAccountVM();
            model.ApplicationUser = _userManager.FindByIdAsync(Id).Result;

            return View(model);
        }

        public async Task<IActionResult> ChangeProfilePhoto(ManageAccountVM vm)
        {
            var userId = _userSessionService.GetCurrentUserID();
            var user = _userManager.FindByIdAsync(userId).Result;
            var newPhotoPath = _fileProcessor.ChangePhoto(user.PhotoPath, vm.Photo);

            user.PhotoPath = newPhotoPath;
            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
                return RedirectToAction("Manage", "Account", new { Id = userId });

            return View(vm);
        }

        public async Task<IActionResult> ChangeAccountDetails(AccountManageChangeAccountDetailsVM vm)
        {
            var userId = _userSessionService.GetCurrentUserID();

            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(userId).Result;

                user.Firstname = vm.Firstname;
                user.Lastname = vm.Lastname;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Profile", "Account", new { id = userId });

            }
            return RedirectToAction("Manage", new { Id = userId });
        }

        public async Task<IActionResult> ChangePassword(AccountManageChangePasswordVM vm)
        {
            var userId = _userSessionService.GetCurrentUserID();

            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(userId).Result;
                var result = await _userManager.ChangePasswordAsync(user,vm.CurrentPassword,vm.Password);

                if (result.Succeeded)
                    return RedirectToAction("Profile", "Account", new { id = userId });

            }
            return RedirectToAction("Manage", new { Id = userId });
        }
    }
}