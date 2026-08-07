using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controllers;

public class HomeController : Controller
{
    public ViewResult Index()
    {
        return View();
    }
}
