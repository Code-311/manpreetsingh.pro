namespace manpreetsingh.pro.Areas.Admin.ViewModels;

public class StaticPageEditViewModel
{
    public int? Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MarkdownBody { get; set; } = string.Empty;
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public bool IsPublished { get; set; }
}
