using manpreetsingh.pro.Areas.Admin.ViewModels;
using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.Services.Markdown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class ToolsController : Controller
{
    private readonly ApplicationDbContext _db; private readonly IMarkdownRenderer _md;
    public ToolsController(ApplicationDbContext db, IMarkdownRenderer md){_db=db;_md=md;}
    public async Task<IActionResult> Index()=>View(await _db.Tools.OrderBy(x=>x.SortOrder).ToListAsync());
    public IActionResult Create()=>View("Edit",new ToolEditViewModel());
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(ToolEditViewModel vm){var t=new Tool();Map(vm,t);_db.Add(t);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    public async Task<IActionResult> Edit(int id){var t=await _db.Tools.FindAsync(id);if(t==null)return NotFound();return View(new ToolEditViewModel{Id=t.Id,Slug=t.Slug,Title=t.Title,Summary=t.Summary,MarkdownBody=t.MarkdownBody,ToolType=t.ToolType,EstimatedDuration=t.EstimatedDuration,SeoTitle=t.SeoTitle,SeoDescription=t.SeoDescription,SortOrder=t.SortOrder,IsPublished=t.IsPublished});}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, ToolEditViewModel vm){var t=await _db.Tools.FindAsync(id);if(t==null)return NotFound();Map(vm,t);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Delete(int id){var t=await _db.Tools.FindAsync(id);if(t!=null){_db.Remove(t);await _db.SaveChangesAsync();}return RedirectToAction(nameof(Index));}
    void Map(ToolEditViewModel vm, Tool t){t.Slug=vm.Slug;t.Title=vm.Title;t.Summary=vm.Summary;t.MarkdownBody=vm.MarkdownBody;t.RenderedHtml=_md.Render(vm.MarkdownBody);t.ToolType=vm.ToolType;t.EstimatedDuration=vm.EstimatedDuration;t.SeoTitle=vm.SeoTitle;t.SeoDescription=vm.SeoDescription;t.SortOrder=vm.SortOrder;t.IsPublished=vm.IsPublished;t.PublishedOnUtc=vm.IsPublished?DateTime.UtcNow:null;t.UpdatedUtc=DateTime.UtcNow;}
}
