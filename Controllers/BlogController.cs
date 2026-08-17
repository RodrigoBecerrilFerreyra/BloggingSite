using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controllers;

public class BlogController : Controller
{
    public IActionResult Index()
    {
        ViewData["Current"] = "Blog";
        return View();
    }
}
