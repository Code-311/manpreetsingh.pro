using manpreetsingh.pro.Models;

namespace manpreetsingh.pro.ViewModels;

public class ModelsPageViewModel
{
    public string Title { get; set; } = "Models";
    public string Intro { get; set; } = string.Empty;
    public IReadOnlyList<ModelItem> Items { get; set; } = [];
}
