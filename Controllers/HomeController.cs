using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controllers;

public class HomeController : Controller
{
    public ViewResult Index()
    {
        ViewData["Current"] = "Index";
        ViewData["Title"] = "Home";
        return View();
    }

    [Route("Contact")]
    public ViewResult Contact()
    {
        ViewData["Current"] = "Contact";
        ViewData["Title"] = "Contact";
        return View();
    }
}
