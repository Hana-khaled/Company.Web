using Company.Data.Models;
using Company.Service.Dto;
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

            ViewBag.Departments = _departmentService.GetAll() ?? new List<DepartmentDto>();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeDto employee)
        {
            //if (employee.DepartmentId <= 0)
            //{
            //    ModelState.AddModelError("DepartmentId", "A valid department must be selected.");
            //}
            //else
            //{
            //    var department = _departmentService.GetById(employee.DepartmentId);
            //    if (department != null)
            //    {
            //        employee.Department = department;
            //    }
            //}
            if (ModelState.IsValid)
            {
                _employeeService.Add(employee);
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("EmployeeError", "ValidationError");

            ViewBag.Departments = _departmentService.GetAll();
            return View(employee);

            //if (!ModelState.IsValid)
            //{
            //    // ModelState is invalid; you can log or inspect its entries here
            //    foreach (var entry in ModelState)
            //    {
            //        var key = entry.Key; // The name of the property
            //        var errors = entry.Value.Errors; // List of errors for that property

            //        if (errors.Count > 0)
            //        {
            //            foreach (var error in errors)
            //            {
            //                // Log or inspect the error message
            //                var errorMessage = error.ErrorMessage;
            //                // You can output these values or handle them as needed
            //                Console.WriteLine($"Property: {key}, Error: {errorMessage}");
            //            }
            //        }
            //    }

            //    // Return the view with the current model to display validation messages
            //    return View(employee);
            //}

            //// Proceed to save the employee if ModelState is valid
            //// Code to save the employee
            //_employeeService.Add(employee);
            //return RedirectToAction("Index");

        }
    }
}
