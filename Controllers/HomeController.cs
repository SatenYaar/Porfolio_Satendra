using Microsoft.AspNetCore.Mvc;
using Porfolio_Satendra.Data;
using Porfolio_Satendra.Models;
using System.Diagnostics;

namespace Porfolio_Satendra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, UserDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Users.Add(user); 
                    _dbContext.SaveChanges();

                    TempData["SuccessMessage"] = "User has been successfully added.";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "An error occurred while adding the user.";
                }

                return RedirectToAction("Success");
            }
            else
            {
                                    TempData["ErrorMessage"] = "An error occurred while adding the user.";

                return View(user); 
            }
        }

        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}