using manpreetsingh.pro.Areas.Admin.ViewModels;
using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.Services.Markdown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class StaticPagesController : Controller
{
    private readonly ApplicationDbContext _db; private readonly IMarkdownRenderer _md;
    public StaticPagesController(ApplicationDbContext db, IMarkdownRenderer md){_db=db;_md=md;}
    public async Task<IActionResult> Index()=>View(await _db.StaticPages.OrderBy(x=>x.Slug).ToListAsync());
    public IActionResult Create()=>View("Edit",new StaticPageEditViewModel());
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(StaticPageEditViewModel vm){var p=new StaticPage();Map(vm,p);_db.Add(p);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    public async Task<IActionResult> Edit(int id){var p=await _db.StaticPages.FindAsync(id);if(p==null)return NotFound();return View(new StaticPageEditViewModel{Id=p.Id,Slug=p.Slug,Title=p.Title,MarkdownBody=p.MarkdownBody,SeoTitle=p.SeoTitle,SeoDescription=p.SeoDescription,IsPublished=p.IsPublished});}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, StaticPageEditViewModel vm){var p=await _db.StaticPages.FindAsync(id);if(p==null)return NotFound();Map(vm,p);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Delete(int id){var p=await _db.StaticPages.FindAsync(id);if(p!=null){_db.Remove(p);await _db.SaveChangesAsync();}return RedirectToAction(nameof(Index));}
    void Map(StaticPageEditViewModel vm, StaticPage p){p.Slug=vm.Slug;p.Title=vm.Title;p.MarkdownBody=vm.MarkdownBody;p.RenderedHtml=_md.Render(vm.MarkdownBody);p.SeoTitle=vm.SeoTitle;p.SeoDescription=vm.SeoDescription;p.IsPublished=vm.IsPublished;p.PublishedOnUtc=vm.IsPublished?DateTime.UtcNow:null;p.UpdatedUtc=DateTime.UtcNow;}
}
