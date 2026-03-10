using manpreetsingh.pro.Models;

namespace manpreetsingh.pro.ViewModels;

public class HomePageViewModel
{
    public string SiteTitle { get; set; } = string.Empty;
    public string HeroKicker { get; set; } = string.Empty;
    public string HeroStatement { get; set; } = string.Empty;
    public string HeroSubline { get; set; } = string.Empty;
    public string ManifestoTitle { get; set; } = string.Empty;
    public IReadOnlyList<string> ManifestoLines { get; set; } = []; 
    public IReadOnlyList<FeaturedWorkItem> FeaturedWorks { get; set; } = []; 
    public IReadOnlyList<ArchiveModule> ArchiveModules { get; set; } = []; 
    public string AboutTitle { get; set; } = string.Empty;
    public string AboutBody { get; set; } = string.Empty;
    public IReadOnlyList<string> Capabilities { get; set; } = [];
    public IReadOnlyList<EssayItem> Essays { get; set; } = []; 
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string ContactLocation { get; set; } = string.Empty;
}
