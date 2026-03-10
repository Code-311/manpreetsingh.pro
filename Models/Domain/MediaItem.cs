namespace manpreetsingh.pro.Models.Domain;

public class MediaItem : PublishableEntityBase
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string MarkdownBody { get; set; } = string.Empty;
    public string RenderedHtml { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty;
    public string? Duration { get; set; }
    public int? VideoAssetId { get; set; }
    public UploadedAsset? VideoAsset { get; set; }
    public int? ThumbnailAssetId { get; set; }
    public UploadedAsset? ThumbnailAsset { get; set; }
}
