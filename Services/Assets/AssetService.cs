using manpreetsingh.pro.Data;
using manpreetsingh.pro.Models.Domain;

namespace manpreetsingh.pro.Services.Assets;

public class AssetService : IAssetService
{
    private readonly ApplicationDbContext _db;

    public AssetService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<UploadedAsset> SaveAsync(IFormFile file, string? altText, string? caption, string assetKind)
    {
        await using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        var asset = new UploadedAsset
        {
            FileName = Path.GetFileName(file.FileName),
            ContentType = file.ContentType,
            Data = ms.ToArray(),
            Length = file.Length,
            AltText = altText,
            Caption = caption,
            AssetKind = assetKind,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        };

        _db.UploadedAssets.Add(asset);
        await _db.SaveChangesAsync();
        return asset;
    }
}
