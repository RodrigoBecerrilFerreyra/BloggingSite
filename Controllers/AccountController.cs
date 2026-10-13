using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogSite.Models;
using BlogSite.ViewModels;

namespace BlogSite.Controllers;

public class AccountController(SignInManager<User> sim) : Controller
{
    private readonly SignInManager<User> _sim = sim;

    [HttpGet]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        // https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-10.0
        if (!ModelState.IsValid)
        {
            return View();
        }

        // attempt to sign user in
        // https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.signinmanager-1.passwordsigninasync?view=aspnetcore-10.0#microsoft-aspnetcore-identity-signinmanager-1-passwordsigninasync(system-string-system-string-system-boolean-system-boolean)
        var signInResult = await _sim.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, true);
        if (signInResult.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _sim.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
