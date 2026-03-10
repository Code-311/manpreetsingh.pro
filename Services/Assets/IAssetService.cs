using manpreetsingh.pro.Models.Domain;

namespace manpreetsingh.pro.Services.Assets;

public interface IAssetService
{
    Task<UploadedAsset> SaveAsync(IFormFile file, string? altText, string? caption, string assetKind);
}
