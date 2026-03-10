using manpreetsingh.pro.Data;
using Microsoft.AspNetCore.Mvc;

namespace manpreetsingh.pro.Controllers;

[Route("assets")]
public class AssetsController : Controller
{
    private readonly ApplicationDbContext _db;
    public AssetsController(ApplicationDbContext db) => _db = db;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var asset = await _db.UploadedAssets.FindAsync(id);
        return asset is null ? NotFound() : File(asset.Data, asset.ContentType, enableRangeProcessing: true);
    }
}
