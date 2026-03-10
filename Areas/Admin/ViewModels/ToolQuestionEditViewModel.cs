namespace manpreetsingh.pro.Areas.Admin.ViewModels;

public class ToolQuestionEditViewModel
{
    public int? Id { get; set; }
    public int ToolId { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string? HelpText { get; set; }
    public int SortOrder { get; set; }
    public int MinScore { get; set; }
    public int MaxScore { get; set; }
}
