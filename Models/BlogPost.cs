using System;
using System.Collections.Generic;

namespace BlogSite.Models;

public class BlogPost
{
    public int Id {get; set;}
    public required string Title {get; set;}
    public required string Author {get; set;}
    public required int CategoryId {get; set;}
    public Category? Category {get; set;}
    public string? Slug {get; set;}
    public required string Content {get; set;}
    public bool IsPublished {get; set;}
    public DateTime CreatedDate {get; set;}
    public DateTime? UpdatedDate {get; set;}
    public DateTime? PublishedDate {get; set;}
    public ICollection<Tag> Tags {get; set;} = new List<Tag>();
}

public class Tag
{
    public int Id {get; set;}
    public required string Name {get; set;}
    public ICollection<BlogPost> Posts {get; set;} = new List<BlogPost>();
}

public class Category
{
    public int Id {get; set;}
    public required string Name {get; set;}
    public ICollection<BlogPost> Posts {get; set;} = new List<BlogPost>();
}
