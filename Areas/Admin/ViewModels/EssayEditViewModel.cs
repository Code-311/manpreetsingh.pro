namespace manpreetsingh.pro.Areas.Admin.ViewModels;

public class EssayEditViewModel
{
    public int? Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string MarkdownBody { get; set; } = string.Empty;
    public int ReadTimeMinutes { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public int? HeroAssetId { get; set; }
    public int SortOrder { get; set; }
    public bool IsPublished { get; set; }
}
