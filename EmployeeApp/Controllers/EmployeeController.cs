using EmployeeApp.Models;
using EmployeeApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EmployeeAppDbContext _context;

        public EmployeeController(ILogger<HomeController> logger, EmployeeAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Controller Action for Adding Employee and Salary Details
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to entity objects and save them to the database
                var employee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };



                // Assuming _context is your DbContext instance
                _context.Employees.Add(employee);
                _context.SaveChanges();
                //once saved, the employee record has it's employee id
                var salary = new EmployeeSalary
                {
                    Salary = model.Salary,
                    EmployeeId = employee.EmployeeId

                };
                _context.EmployeeSalaries.Add(salary);
                _context.SaveChanges();


                return RedirectToAction("Index", "Home"); // Redirect to the employee list page
            }

            // If ModelState is not valid, return the view with validation errors
            return View(model);
        }


        // Controller Action for Updating Employee Details
        [HttpGet]
        public IActionResult UpdateEmployee(int id)
        {
            // Retrieve the employee and their salary from the database based on the provided id
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            var salary = _context.EmployeeSalaries.FirstOrDefault(es => es.EmployeeId == id);

            if (employee == null || salary == null)
            {
                return NotFound(); // Handle if employee or salary not found
            }

            // Map the database entities to the view model
            var model = new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = salary.Salary
                // Map other properties as needed
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the employee and their salary from the database
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == model.EmployeeId);
                var salary = _context.EmployeeSalaries.FirstOrDefault(es => es.EmployeeId == model.EmployeeId);

                if (employee == null || salary == null)
                {
                    return NotFound(); // Handle if employee or salary not found
                }

                // Update employee details
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                // Update other properties as needed

                // Update salary details
                salary.Salary = model.Salary;
                // Update other salary properties as needed

                // Save changes to the database
                _context.SaveChanges();

                return RedirectToAction("Index", "Home"); // Redirect to the employee list page after updating
            }

            // If ModelState is not valid, return the view with validation errors
            return View(model);
        }

    }
}
