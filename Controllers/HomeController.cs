using manpreetsingh.pro.Data;
using manpreetsingh.pro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("/")]
    public async Task<IActionResult> Index()
    {
        var vm = new HomePageViewModel
        {
            SelectedWritings = await _db.Essays.Where(x => x.IsPublished).OrderBy(x => x.SortOrder).ThenByDescending(x => x.PublishedOnUtc).Take(3).ToListAsync(),
            FeaturedModels = await _db.FrameworkModels.Where(x => x.IsPublished).OrderBy(x => x.SortOrder).Take(3).ToListAsync(),
            Tools = await _db.Tools.Where(x => x.IsPublished).OrderBy(x => x.SortOrder).Take(2).ToListAsync(),
            MediaItems = await _db.MediaItems.Where(x => x.IsPublished).OrderBy(x => x.SortOrder).Take(2).ToListAsync(),
            AboutPage = await _db.StaticPages.FirstOrDefaultAsync(x => x.Slug == "about" && x.IsPublished),
            ContactPage = await _db.StaticPages.FirstOrDefaultAsync(x => x.Slug == "contact" && x.IsPublished)
        };

        return View(vm);
    }
}
