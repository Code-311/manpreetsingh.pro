using manpreetsingh.pro.Models;

namespace manpreetsingh.pro.ViewModels;

public class MediaPageViewModel
{
    public string Title { get; set; } = "Media";
    public string Intro { get; set; } = string.Empty;
    public IReadOnlyList<MediaItem> Items { get; set; } = [];
}
