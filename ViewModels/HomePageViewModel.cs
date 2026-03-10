using manpreetsingh.pro.Models.Domain;

namespace manpreetsingh.pro.ViewModels;

public class HomePageViewModel
{
    public IReadOnlyList<Essay> SelectedWritings { get; set; } = [];
    public IReadOnlyList<FrameworkModel> FeaturedModels { get; set; } = [];
    public IReadOnlyList<Tool> Tools { get; set; } = [];
    public IReadOnlyList<MediaItem> MediaItems { get; set; } = [];
    public StaticPage? AboutPage { get; set; }
    public StaticPage? ContactPage { get; set; }
}
