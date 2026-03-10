using manpreetsingh.pro.Models;

namespace manpreetsingh.pro.ViewModels;

public class WritingsPageViewModel
{
    public string Title { get; set; } = "Writings";
    public string Intro { get; set; } = string.Empty;
    public IReadOnlyList<WritingItem> Items { get; set; } = [];
}
