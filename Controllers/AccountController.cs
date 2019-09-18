using System.Threading.Tasks;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Title = "Sign Up";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AccountRegisterViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser(){
                    Email = vm.Email,
                    UserName = vm.Email
                };

                var result = await _userManager.CreateAsync(user,vm.Password);

                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user,isPersistent: false);
                    return RedirectToAction("Index","Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewBag.Title = "Sign Up";
            return View(vm);
        }
    
        public async Task<IActionResult> SignOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    
    }
}