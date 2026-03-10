using Markdig;

namespace manpreetsingh.pro.Services.Markdown;

public class MarkdownRenderer : IMarkdownRenderer
{
    private readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
        .DisableHtml()
        .UseAdvancedExtensions()
        .Build();

    public string Render(string markdown) => string.IsNullOrWhiteSpace(markdown)
        ? string.Empty
        : Markdig.Markdown.ToHtml(markdown, _pipeline);
}
