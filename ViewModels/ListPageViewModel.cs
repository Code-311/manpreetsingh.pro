namespace manpreetsingh.pro.ViewModels;

public class ListPageViewModel<T>
{
    public string Title { get; set; } = string.Empty;
    public IReadOnlyList<T> Items { get; set; } = [];
}
