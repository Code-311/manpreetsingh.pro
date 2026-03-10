namespace manpreetsingh.pro.Models.Domain;

public class FrameworkModel : PublishableEntityBase
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string MarkdownBody { get; set; } = string.Empty;
    public string RenderedHtml { get; set; } = string.Empty;
    public int? DiagramAssetId { get; set; }
    public UploadedAsset? DiagramAsset { get; set; }
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
}
