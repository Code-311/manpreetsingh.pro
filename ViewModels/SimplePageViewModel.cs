namespace manpreetsingh.pro.ViewModels;

public class SimplePageViewModel
{
    public string Title { get; set; } = string.Empty;
    public string Intro { get; set; } = string.Empty;
    public IReadOnlyList<string> Details { get; set; } = [];
}
