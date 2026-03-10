using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Controllers;

[Route("tools")]
public class ToolsController : Controller
{
    private readonly ApplicationDbContext _db;
    public ToolsController(ApplicationDbContext db) => _db = db;

    [HttpGet("")]
    public async Task<IActionResult> Index() => View(new ListPageViewModel<Tool>
    {
        Title = "Tools",
        Items = await _db.Tools.Where(x => x.IsPublished).Include(x => x.Questions.OrderBy(q => q.SortOrder)).OrderBy(x => x.SortOrder).ToListAsync()
    });

    [HttpGet("{slug}")]
    public async Task<IActionResult> Detail(string slug)
    {
        var tool = await _db.Tools.Include(x => x.Questions.OrderBy(q => q.SortOrder)).FirstOrDefaultAsync(x => x.Slug == slug && x.IsPublished);
        return tool is null ? NotFound() : View(new DetailPageViewModel<Tool> { Item = tool });
    }
}
