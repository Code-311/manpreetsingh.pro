using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Controllers;

[Route("writings")]
public class WritingsController : Controller
{
    private readonly ApplicationDbContext _db;
    public WritingsController(ApplicationDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index() => View(new ListPageViewModel<Essay>
    {
        Title = "Writings",
        Items = await _db.Essays.Where(x => x.IsPublished).OrderBy(x => x.SortOrder).ThenByDescending(x => x.PublishedOnUtc).ToListAsync()
    });

    [HttpGet("{slug}")]
    public async Task<IActionResult> Detail(string slug)
    {
        var essay = await _db.Essays.Include(x => x.HeroAsset).FirstOrDefaultAsync(x => x.Slug == slug && x.IsPublished);
        return essay is null ? NotFound() : View(new DetailPageViewModel<Essay> { Item = essay });
    }
}
