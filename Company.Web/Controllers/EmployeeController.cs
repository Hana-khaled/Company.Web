using Company.Data.Models;
using Company.Service.Interfaces;
using Company.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService) 
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        [HttpGet]
        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var empList = _employeeService.GetAll();
                return View(empList);
            }
            else
            {
                var emp = _employeeService.GetEmployeeByName(searchInp);
                return View();
            }
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Message = "Hello from viewBag";
            //ViewData["TxtMessage"] = "Hello from viewData";

            ViewBag.Departments = _departmentService.GetAll() ?? new List<Department>();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            
                if (ModelState.IsValid)
                {
                    _employeeService.Add(employee);
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("EmployeeError", "ValidationError");

                ViewBag.Departments = _departmentService.GetAll();
                return View(employee);
            

        }
    }
}
