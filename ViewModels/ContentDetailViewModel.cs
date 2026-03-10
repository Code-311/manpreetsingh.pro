namespace manpreetsingh.pro.ViewModels;

public class ContentDetailViewModel
{
    public string SectionLabel { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public IReadOnlyList<string> Points { get; set; } = [];
}
