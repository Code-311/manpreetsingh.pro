using manpreetsingh.pro.Models;

namespace manpreetsingh.pro.ViewModels;

public class HomePageViewModel
{
    public string HeroTitle { get; set; } = string.Empty;
    public string HeroSubtext { get; set; } = string.Empty;
    public string WhyGapIntro { get; set; } = string.Empty;
    public IReadOnlyList<string> WhyGapPoints { get; set; } = [];
    public IReadOnlyList<ModelItem> FeaturedModels { get; set; } = [];
    public IReadOnlyList<WritingItem> SelectedWritings { get; set; } = [];
    public IReadOnlyList<ToolItem> Tools { get; set; } = [];
    public IReadOnlyList<MediaItem> MediaItems { get; set; } = [];
    public string AboutSummary { get; set; } = string.Empty;
    public string ContactSummary { get; set; } = string.Empty;
}
