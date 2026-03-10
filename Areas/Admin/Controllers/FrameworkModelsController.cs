using manpreetsingh.pro.Areas.Admin.ViewModels;
using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.Services.Markdown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class FrameworkModelsController : Controller
{
    private readonly ApplicationDbContext _db; private readonly IMarkdownRenderer _md;
    public FrameworkModelsController(ApplicationDbContext db, IMarkdownRenderer md){_db=db;_md=md;}
    public async Task<IActionResult> Index()=>View(await _db.FrameworkModels.OrderBy(x=>x.SortOrder).ToListAsync());
    public IActionResult Create()=>View("Edit",new FrameworkModelEditViewModel());
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(FrameworkModelEditViewModel vm){var m=new FrameworkModel();Map(vm,m);_db.Add(m);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    public async Task<IActionResult> Edit(int id){var m=await _db.FrameworkModels.FindAsync(id);if(m==null) return NotFound(); return View(new FrameworkModelEditViewModel{Id=m.Id,Slug=m.Slug,Title=m.Title,Summary=m.Summary,MarkdownBody=m.MarkdownBody,SeoTitle=m.SeoTitle,SeoDescription=m.SeoDescription,DiagramAssetId=m.DiagramAssetId,SortOrder=m.SortOrder,IsPublished=m.IsPublished});}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, FrameworkModelEditViewModel vm){var m=await _db.FrameworkModels.FindAsync(id); if(m==null)return NotFound(); Map(vm,m); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Delete(int id){var m=await _db.FrameworkModels.FindAsync(id); if(m!=null){_db.Remove(m); await _db.SaveChangesAsync();} return RedirectToAction(nameof(Index));}
    void Map(FrameworkModelEditViewModel vm, FrameworkModel m){m.Slug=vm.Slug;m.Title=vm.Title;m.Summary=vm.Summary;m.MarkdownBody=vm.MarkdownBody;m.RenderedHtml=_md.Render(vm.MarkdownBody);m.SeoTitle=vm.SeoTitle;m.SeoDescription=vm.SeoDescription;m.DiagramAssetId=vm.DiagramAssetId;m.SortOrder=vm.SortOrder;m.IsPublished=vm.IsPublished;m.PublishedOnUtc=vm.IsPublished?DateTime.UtcNow:null;m.UpdatedUtc=DateTime.UtcNow;}
}
