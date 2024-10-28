using Company.Data.Models;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger _logger;

		public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger)
		{
			_userManager = userManager;
			_logger = logger;
		}
		public async Task<IActionResult> Index(string searchInp)
		{
			List<ApplicationUser> users;
			if (string.IsNullOrEmpty(searchInp))
			{
				users = await _userManager.Users.ToListAsync();
			}
			else
			{
				users = await _userManager.Users
					.Where(user => user.NormalizedEmail.Trim()
					.Contains(searchInp.Trim().ToUpper())).ToListAsync();
			}
				
			return View(users);
		}

		public async Task<IActionResult> Details(string? id, string viewName = "Details")
		{
			var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
				return NotFound();
            }
			if (viewName == "Update")
			{
				var userModel = new UserUpdateViewModel
				{
					Id = user.Id,
					UserName = user.UserName
				};

                return View(viewName, userModel);
            }

			return View(viewName, user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string? id, UserUpdateViewModel model)
        {
			if(id != model.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByIdAsync(id);

					if(user is null)
					{
						return NotFound();
					}

					user.UserName = model.UserName;
					user.NormalizedUserName = model.UserName.ToUpper();

					var result = await _userManager.UpdateAsync(user);

					if (result.Succeeded)
					{
						_logger.LogInformation("User Updated Successfully");
						return RedirectToAction(nameof(Index));
					}

				}
				catch (Exception ex)
				{
					_logger.LogInformation(ex.Message);
				}
			}

			return View(model);
        }
    }
}
