namespace manpreetsingh.pro.Areas.Admin.ViewModels;

public class MediaItemEditViewModel
{
    public int? Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string MarkdownBody { get; set; } = string.Empty;
    public string MediaType { get; set; } = string.Empty;
    public string? Duration { get; set; }
    public int? VideoAssetId { get; set; }
    public int? ThumbnailAssetId { get; set; }
    public int SortOrder { get; set; }
    public bool IsPublished { get; set; }
}
