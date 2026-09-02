using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogSite.ViewModels;
using Microsoft.EntityFrameworkCore;
using BlogSite.Data;
using System.Linq;

namespace BlogSite.Controllers;

public class BlogController(BlogDbContext context) : Controller
{
    private readonly BlogDbContext _context = context;

    public async Task<IActionResult> Index()
    {
        ViewData["Current"] = "Blog";
        ViewData["Title"] = "Blog";

        var data = new BlogIndexViewModel
        {
            Posts =  await _context.BlogPosts
                .Where(p => p.IsPublished)
                //.Include(p => p.Category)
                //.Include(p => p.Tags)
                .OrderByDescending(p => p.CreatedDate)
                .Take(5)
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
