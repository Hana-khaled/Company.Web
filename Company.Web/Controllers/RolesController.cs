using Company.Data.Models;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            // Checking if the role exists or not
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            ViewBag.RoleId = roleId;

            // Getting all the users
            var users = await _userManager.Users.ToListAsync();
            // declaring the usersInrole list that we want to return
            var usersInRole = new List<UserInRoleViewModel>();

           foreach(var user in users)
            {
                // Mapping Application User to UserInRoleViewModel
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                // Checking if the current user is in the specified role or not to handle IsSelected property
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;
                // Adding the current UserInRole to the list of usersInRole
                usersInRole.Add(userInRole);
            }

           //return the resulted list
           return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);

                    if(appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.AddToRoleAsync(appUser, role.Name);

                        else if(!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }

                }
                return RedirectToAction(nameof(Update), new {id = roleId});
            }
            return View(users);
        }


    }
}
