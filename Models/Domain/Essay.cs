namespace manpreetsingh.pro.Models.Domain;

public class Essay : PublishableEntityBase
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string MarkdownBody { get; set; } = string.Empty;
    public string RenderedHtml { get; set; } = string.Empty;
    public int ReadTimeMinutes { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public int? HeroAssetId { get; set; }
    public UploadedAsset? HeroAsset { get; set; }
}
