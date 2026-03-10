namespace manpreetsingh.pro.Models.Domain;

public class Tool : PublishableEntityBase
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string MarkdownBody { get; set; } = string.Empty;
    public string RenderedHtml { get; set; } = string.Empty;
    public string ToolType { get; set; } = string.Empty;
    public string EstimatedDuration { get; set; } = string.Empty;
    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }

    public List<ToolQuestion> Questions { get; set; } = new();
}
