using asp_net_mvc_core_adminlte_taghelpers.Models;
using asp_net_mvc_core_adminlte_taghelpers.Viewmodels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace asp_net_mvc_core_adminlte_taghelpers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private object _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var viewmodel = new HomeIndexViewModel();
            viewmodel.BusinessCount = 1234;
            viewmodel.CompetenciesCount = 1234;
            viewmodel.ContactsCount = 1234;
            viewmodel.ProductCategoriesCount = 1234;
            viewmodel.ProductsCount = 1234;
            viewmodel.ProjectMileStonesCount = 1234;
            viewmodel.ProjectsCount = 1234;
            viewmodel.EmployeesCount = 1234;
            viewmodel.IsIngelogd = true;
            viewmodel.IsUitgelogd = false;
            return View(viewmodel);
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