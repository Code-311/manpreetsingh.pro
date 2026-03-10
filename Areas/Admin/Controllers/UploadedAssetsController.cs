using manpreetsingh.pro.Data;
using manpreetsingh.pro.Services.Assets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class UploadedAssetsController : Controller
{
    private readonly ApplicationDbContext _db; private readonly IAssetService _assets;
    public UploadedAssetsController(ApplicationDbContext db, IAssetService assets){_db=db;_assets=assets;}
    public async Task<IActionResult> Index()=>View(await _db.UploadedAssets.OrderByDescending(x=>x.CreatedUtc).ToListAsync());
    [HttpGet] public IActionResult Create()=>View();
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(IFormFile file, string? altText, string? caption, string assetKind="image")
    {
        if (file == null || file.Length == 0){ModelState.AddModelError("file","File is required."); return View();}
        await _assets.SaveAsync(file, altText, caption, assetKind);
        return RedirectToAction(nameof(Index));
    }
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Delete(int id){var a=await _db.UploadedAssets.FindAsync(id);if(a!=null){_db.Remove(a);await _db.SaveChangesAsync();}return RedirectToAction(nameof(Index));}
}
