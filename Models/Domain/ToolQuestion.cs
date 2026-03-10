namespace manpreetsingh.pro.Models.Domain;

public class ToolQuestion : EntityBase
{
    public int ToolId { get; set; }
    public Tool? Tool { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string? HelpText { get; set; }
    public int SortOrder { get; set; }
    public int MinScore { get; set; }
    public int MaxScore { get; set; }
}
