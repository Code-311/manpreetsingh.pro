using manpreetsingh.pro.Areas.Admin.ViewModels;
using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.Services.Markdown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class EssaysController : Controller
{
    private readonly ApplicationDbContext _db; private readonly IMarkdownRenderer _markdown;
    public EssaysController(ApplicationDbContext db, IMarkdownRenderer markdown) { _db = db; _markdown = markdown; }
    public async Task<IActionResult> Index() => View(await _db.Essays.OrderBy(x => x.SortOrder).ToListAsync());
    public IActionResult Create() => View("Edit", new EssayEditViewModel());
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Create(EssayEditViewModel vm){ if(!ModelState.IsValid) return View("Edit",vm); var e=new Essay(); Map(vm,e); _db.Add(e); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
    public async Task<IActionResult> Edit(int id){ var e=await _db.Essays.FindAsync(id); if(e==null) return NotFound(); return View(new EssayEditViewModel{Id=e.Id,Slug=e.Slug,Title=e.Title,Summary=e.Summary,MarkdownBody=e.MarkdownBody,ReadTimeMinutes=e.ReadTimeMinutes,Category=e.Category,SeoTitle=e.SeoTitle,SeoDescription=e.SeoDescription,HeroAssetId=e.HeroAssetId,SortOrder=e.SortOrder,IsPublished=e.IsPublished}); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, EssayEditViewModel vm){ var e=await _db.Essays.FindAsync(id); if(e==null) return NotFound(); Map(vm,e); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
    [HttpPost, ValidateAntiForgeryToken] public async Task<IActionResult> Delete(int id){ var e=await _db.Essays.FindAsync(id); if(e!=null){_db.Remove(e); await _db.SaveChangesAsync();} return RedirectToAction(nameof(Index)); }
    private void Map(EssayEditViewModel vm, Essay e){ e.Slug=vm.Slug; e.Title=vm.Title; e.Summary=vm.Summary; e.MarkdownBody=vm.MarkdownBody; e.RenderedHtml=_markdown.Render(vm.MarkdownBody); e.ReadTimeMinutes=vm.ReadTimeMinutes; e.Category=vm.Category; e.SeoTitle=vm.SeoTitle; e.SeoDescription=vm.SeoDescription; e.HeroAssetId=vm.HeroAssetId; e.SortOrder=vm.SortOrder; e.IsPublished=vm.IsPublished; e.PublishedOnUtc=vm.IsPublished?DateTime.UtcNow:null; e.UpdatedUtc=DateTime.UtcNow; }
}
