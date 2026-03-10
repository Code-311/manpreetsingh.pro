using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Controllers;

[Route("models")]
public class ModelsController : Controller
{
    private readonly ApplicationDbContext _db;
    public ModelsController(ApplicationDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index() => View(new ListPageViewModel<FrameworkModel>
    {
        Title = "Models",
        Items = await _db.FrameworkModels.Where(x => x.IsPublished).OrderBy(x => x.SortOrder).ToListAsync()
    });

    [HttpGet("{slug}")]
    public async Task<IActionResult> Detail(string slug)
    {
        var model = await _db.FrameworkModels.Include(x => x.DiagramAsset).FirstOrDefaultAsync(x => x.Slug == slug && x.IsPublished);
        return model is null ? NotFound() : View(new DetailPageViewModel<FrameworkModel> { Item = model });
    }
}
