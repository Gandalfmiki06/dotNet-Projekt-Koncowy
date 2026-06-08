using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    private IEnumerable<SelectListItem> GetRoles()
    {
        return _roleManager.Roles
            .Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            });
    }

    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var result = new List<UserListItemViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            result.Add(new UserListItemViewModel
            {
                Id = user.Id,
                UserName = user.UserName!,
                FullName = user.FullName,
                Role = roles.First()
            });
        }

        return View(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateUserViewModel
        {
            AvailableRoles = GetRoles()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.AvailableRoles = GetRoles();
            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            model.AvailableRoles = GetRoles();

            foreach (var error in result.Errors)
            {
                if (error.Code.Contains("Password"))
                    ModelState.AddModelError(nameof(model.Password), error.Description);
                else if (error.Code.Contains("Email"))
                    ModelState.AddModelError(nameof(model.Email), error.Description);
                else if (error.Code.Contains("UserName"))
                    ModelState.AddModelError(nameof(model.UserName), error.Description);
                else
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        await _userManager.AddToRoleAsync(user, model.Role);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.First();

        return View(new EditUserViewModel
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            FullName = user.FullName,
            Role = role,
            AvailableRoles = GetRoles()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.AvailableRoles = GetRoles();
            return View(model);
        }

        var user = await _userManager.FindByIdAsync(model.Id);

        if (user == null)
            return NotFound();

        var isSelfEdit = user.Id == _userManager.GetUserId(User);

        if (isSelfEdit)
        {
            var currentRole = await _userManager.GetRolesAsync(user);

            if (currentRole.FirstOrDefault() != model.Role)
            {
                ModelState.AddModelError(string.Empty, "Nie możesz zmienić własnej roli.");
                model.AvailableRoles = GetRoles();
                return View(model);
            }
        }

        user.UserName = model.UserName;
        user.Email = model.Email;
        user.FullName = model.FullName;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            model.AvailableRoles = GetRoles();

            foreach (var error in result.Errors)
            {
                if (error.Code.Contains("Email"))
                    ModelState.AddModelError(nameof(model.Email), error.Description);
                else if (error.Code.Contains("UserName"))
                    ModelState.AddModelError(nameof(model.UserName), error.Description);
                else
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        var currentRoles = await _userManager.GetRolesAsync(user);

        if (currentRoles.Any())
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

        await _userManager.AddToRoleAsync(user, model.Role);

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> IsLastAdminAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        if (!roles.Contains("Admin"))
            return false;

        var allAdmins = await _userManager.GetUsersInRoleAsync("Admin");

        return allAdmins.Count == 1;
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        var currentUser = await _userManager.GetUserAsync(User);

        if (currentUser != null && currentUser.Id == user.Id)
        {
            TempData["Error"] = "Nie możesz usunąć własnego konta.";
            return RedirectToAction(nameof(Index));
        }

        if (await IsLastAdminAsync(user))
        {
            TempData["Error"] = "Nie możesz usunąć ostatniego administratora.";
            return RedirectToAction(nameof(Index));
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
            return RedirectToAction(nameof(Index));

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> ResetPassword(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        return View(new ResetPasswordViewModel
        {
            UserId = user.Id,
            UserName = user.UserName!
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _userManager.FindByIdAsync(model.UserId);

        if (user == null)
            return NotFound();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(
            user,
            token,
            model.NewPassword
        );

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(nameof(model.NewPassword), error.Description);

            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }
}