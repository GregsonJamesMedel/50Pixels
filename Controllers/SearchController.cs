using System.Linq;
using _50Pixels.Models;
using _50Pixels.Services;
using _50Pixels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _50Pixels.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SearchController(UserManager<ApplicationUser> userManager, IPhotoService photoService)
        {
            this._userManager = userManager;
            this._photoService = photoService;
        }

        public IActionResult Results(SearchResultsViewModel searchVM)
        {
            searchVM.Photos = this._photoService.SearchPhotoByTitle(searchVM.SearchKey);

            if (searchVM.SearchType == 1.ToString())
                    searchVM.ApplicationUsers = this._userManager.Users
                    .Where(x => x.Firstname.Contains(searchVM.SearchKey) || x.Lastname.Contains(searchVM.SearchKey));

            return View(searchVM);
        }
    }
}