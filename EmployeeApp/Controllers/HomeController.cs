using EmployeeApp.Models;
using EmployeeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EmployeeAppDbContext _context;

        //let us inject the database context dependency
        public HomeController(ILogger<HomeController> logger, EmployeeAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            var employees = _context.Employees
               .Join(
                   _context.EmployeeSalaries,
                   e => e.EmployeeId,
                   es => es.EmployeeId,
                   (e, es) => new EmployeeViewModel
                   {
                       EmployeeId = e.EmployeeId,
                       FirstName = e.FirstName,
                       LastName = e.LastName,
                       Salary = es.Salary
                   }
               )
               .ToList();

            return View(employees);
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