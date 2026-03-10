using manpreetsingh.pro.Areas.Admin.ViewModels;
using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.Services.Markdown;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class MediaItemsController : Controller
{
    private readonly ApplicationDbContext _db; private readonly IMarkdownRenderer _md;
    public MediaItemsController(ApplicationDbContext db, IMarkdownRenderer md){_db=db;_md=md;}
    public async Task<IActionResult> Index()=>View(await _db.MediaItems.OrderBy(x=>x.SortOrder).ToListAsync());
    public IActionResult Create()=>View("Edit",new MediaItemEditViewModel());
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(MediaItemEditViewModel vm){var m=new MediaItem();Map(vm,m);_db.Add(m);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    public async Task<IActionResult> Edit(int id){var m=await _db.MediaItems.FindAsync(id);if(m==null)return NotFound();return View(new MediaItemEditViewModel{Id=m.Id,Slug=m.Slug,Title=m.Title,Summary=m.Summary,MarkdownBody=m.MarkdownBody,MediaType=m.MediaType,Duration=m.Duration,VideoAssetId=m.VideoAssetId,ThumbnailAssetId=m.ThumbnailAssetId,SortOrder=m.SortOrder,IsPublished=m.IsPublished});}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id, MediaItemEditViewModel vm){var m=await _db.MediaItems.FindAsync(id);if(m==null)return NotFound();Map(vm,m);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Delete(int id){var m=await _db.MediaItems.FindAsync(id);if(m!=null){_db.Remove(m);await _db.SaveChangesAsync();}return RedirectToAction(nameof(Index));}
    void Map(MediaItemEditViewModel vm, MediaItem m){m.Slug=vm.Slug;m.Title=vm.Title;m.Summary=vm.Summary;m.MarkdownBody=vm.MarkdownBody;m.RenderedHtml=_md.Render(vm.MarkdownBody);m.MediaType=vm.MediaType;m.Duration=vm.Duration;m.VideoAssetId=vm.VideoAssetId;m.ThumbnailAssetId=vm.ThumbnailAssetId;m.SortOrder=vm.SortOrder;m.IsPublished=vm.IsPublished;m.PublishedOnUtc=vm.IsPublished?DateTime.UtcNow:null;m.UpdatedUtc=DateTime.UtcNow;}
}
