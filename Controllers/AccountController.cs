using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogSite.Data;
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
}
