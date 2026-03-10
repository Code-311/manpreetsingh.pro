using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Controllers;

[Route("media")]
public class MediaController : Controller
{
    private readonly ApplicationDbContext _db;
    public MediaController(ApplicationDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index() => View(new ListPageViewModel<MediaItem>
    {
        Title = "Media",
        Items = await _db.MediaItems.Where(x => x.IsPublished).Include(x => x.ThumbnailAsset).OrderBy(x => x.SortOrder).ToListAsync()
    });
}
