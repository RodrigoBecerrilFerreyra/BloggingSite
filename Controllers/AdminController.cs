using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogSite.Data;

namespace BlogSite.Controllers;

[Authorize]
public class AdminController(BlogDbContext context) : Controller
{
    private readonly BlogDbContext _context = context;
}
