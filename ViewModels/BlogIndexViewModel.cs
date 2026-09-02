using System.Collections.Generic;
using BlogSite.Models;

namespace BlogSite.ViewModels;

public class BlogIndexViewModel
{
    public List<BlogPost> Posts {get; set;} = [];
    public List<Category> Categories {get; set;} = [];
    public List<Tag> Tags {get; set;} = [];
}
