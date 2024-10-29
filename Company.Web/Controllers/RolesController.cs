using Company.Data.Models;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RolesController> _logger;

        public RolesController(RoleManager<IdentityRole> roleManager
            , UserManager<ApplicationUser> userManager
            , ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = model.Name
                };

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach(var err in result.Errors)
                {
                    _logger.LogInformation(err.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();



            var roleModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(viewName, roleModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string? id, RoleViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    if (role is null)
                        return NotFound();

                    role.Name = model.Name;
                    role.NormalizedName = model.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Role is Updated Successfully");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _logger.LogInformation("User is not updated");
                        return View(model);
                    }
                }

                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
                return NotFound();
            try
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Tje Role is removed Successfully");
                    return RedirectToAction(nameof(Index));
                }

                foreach (var err in result.Errors)
                {
                    _logger.LogError(err.Description);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
