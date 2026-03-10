using manpreetsingh.pro.Models;

namespace manpreetsingh.pro.ViewModels;

public class ToolsPageViewModel
{
    public string Title { get; set; } = "Tools";
    public string Intro { get; set; } = string.Empty;
    public IReadOnlyList<ToolItem> Items { get; set; } = [];
}
