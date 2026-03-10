using manpreetsingh.pro.Data;
using manpreetsingh.pro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Controllers;

public class PagesController : Controller
{
    private readonly ApplicationDbContext _db;
    public PagesController(ApplicationDbContext db) => _db = db;

    [HttpGet("about")]
    public async Task<IActionResult> About()
    {
        var page = await _db.StaticPages.FirstOrDefaultAsync(x => x.Slug == "about" && x.IsPublished);
        return page is null ? NotFound() : View("Page", new StaticPageViewModel { Page = page });
    }

    [HttpGet("contact")]
    public async Task<IActionResult> Contact()
    {
        var page = await _db.StaticPages.FirstOrDefaultAsync(x => x.Slug == "contact" && x.IsPublished);
        return page is null ? NotFound() : View("Page", new StaticPageViewModel { Page = page });
    }
}
