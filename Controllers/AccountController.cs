using _50Pixels.Models;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace _50Pixels.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserSessionService _userSessionService;
        private readonly IFollowService _followService;
        private readonly IPhotoFileProcessor _photoFileProcessor;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                IUserSessionService userSessionService,
                                IPhotoFileProcessor photoFileProcessor,
                                IFollowService followService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._photoFileProcessor = photoFileProcessor;
            this._userSessionService = userSessionService;
            this._followService = followService;
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
                    photoFileName = this._photoFileProcessor.FileProcessor.SavePhoto(vm.Photo, "ProfilePics");

                var user = new ApplicationUser()
                {
                    Email = vm.Email,
                    UserName = vm.Email,
                    Firstname = vm.Firstname,
                    Lastname = vm.Lastname,
                    PhotoPath = photoFileName
                };

                var result = await this._userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    await this._signInManager.SignInAsync(user, isPersistent: false);
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
        public async Task<IActionResult> SignIn(AccountSignInViewModel vm, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await this._signInManager.PasswordSignInAsync(vm.Email, vm.Password, isPersistent: false, false);

                if(result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        return Redirect(ReturnUrl);
                    
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> SignOff()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile(string id)
        {
            var appUser = this._userManager.FindByIdAsync(id);

            var vm = new AccountProfileViewModel();
            vm.ApplicationUser = appUser.Result;
            vm.LikedPhotos = this._photoFileProcessor.PhotoService.GetLikedPhotos(id);
            vm.Photos = this._photoFileProcessor.PhotoService.GetPhotosByUploaderId(id);
            vm.Following = this._followService.GetFollowing(id);
            vm.Followers = this._followService.GetFollowers(id);
            vm.IsCurrentUserProfile = appUser.Result.Id == this._userSessionService.GetCurrentUserID();

            return View(vm);
        }

        public IActionResult Manage(string Id)
        {
            var model = new ManageAccountVM();
            model.ApplicationUser = this._userManager.FindByIdAsync(Id).Result;

            return View(model);
        }

        public async Task<IActionResult> ChangeProfilePhoto(ManageAccountVM vm)
        {
            var userId = this._userSessionService.GetCurrentUserID();
            var user = this._userManager.FindByIdAsync(userId).Result;
            var newPhotoPath = this._photoFileProcessor.FileProcessor.ChangePhoto(user.PhotoPath, vm.Photo);

            user.PhotoPath = newPhotoPath;
            var updateResult = await this._userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
                return RedirectToAction("Manage", "Account", new { Id = userId });

            return View(vm);
        }

        public async Task<IActionResult> ChangeAccountDetails(AccountManageChangeAccountDetailsVM vm)
        {
            var userId = this._userSessionService.GetCurrentUserID();

            if (ModelState.IsValid)
            {
                var user = this._userManager.FindByIdAsync(userId).Result;

                user.Firstname = vm.Firstname;
                user.Lastname = vm.Lastname;

                var result = await this._userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Profile", "Account", new { id = userId });

            }
            return RedirectToAction("Manage", new { Id = userId });
        }

        public async Task<IActionResult> ChangePassword(AccountManageChangePasswordVM vm)
        {
            var userId = this._userSessionService.GetCurrentUserID();

            if (ModelState.IsValid)
            {
                var user = this._userManager.FindByIdAsync(userId).Result;
                var result = await this._userManager.ChangePasswordAsync(user,vm.CurrentPassword,vm.Password);

                if (result.Succeeded)
                    return RedirectToAction("Profile", "Account", new { id = userId });

            }
            return RedirectToAction("Manage", new { Id = userId });
        }
    }
}