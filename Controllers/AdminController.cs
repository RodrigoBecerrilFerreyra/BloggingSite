using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogSite.Data;
using BlogSite.ViewModels;
using BlogSite.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Controllers;

[Authorize]
public class AdminController(BlogDbContext context) : Controller
{
    private readonly BlogDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        ViewData["Current"] = "Admin";

        // create a list of 10 most recent blogposts, all categories, and all tags
        var data = new BlogIndexViewModel()
        {
            Posts = await _context.BlogPosts
            .Where(p => p.IsPublished)
            .OrderByDescending(p => p.CreatedDate)
            .Take(10)
            .ToListAsync(),

            Categories = await _context.Categories
            .OrderBy(c => c.Id)
            .ToListAsync(),

            Tags = await _context.Tags
            .OrderBy(t => t.Id)
            .ToListAsync()
        };

        return View(data);
    }
}
