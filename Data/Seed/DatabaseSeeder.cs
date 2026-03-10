using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.Models.Identity;
using manpreetsingh.pro.Services.Markdown;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Data.Seed;

public class DatabaseSeeder
{
    public const string AdminRole = "Admin";
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IMarkdownRenderer _markdown;

    public DatabaseSeeder(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMarkdownRenderer markdown)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _markdown = markdown;
    }

    public async Task SeedAsync()
    {
        await _db.Database.EnsureCreatedAsync();

        if (!await _roleManager.RoleExistsAsync(AdminRole))
            await _roleManager.CreateAsync(new IdentityRole(AdminRole));

        var admin = await _userManager.FindByNameAsync("admin");
        if (admin is null)
        {
            var password = _configuration["Seed:AdminPassword"] ?? throw new InvalidOperationException("Seed:AdminPassword not configured");
            admin = new ApplicationUser { UserName = "admin", Email = "admin@manpreetsingh.pro", EmailConfirmed = true };
            var result = await _userManager.CreateAsync(admin, password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(";", result.Errors.Select(e => e.Description)));
        }

        if (!await _userManager.IsInRoleAsync(admin, AdminRole))
            await _userManager.AddToRoleAsync(admin, AdminRole);

        if (!await _db.Essays.AnyAsync())
        {
            _db.Essays.AddRange(
                SeedEssay("the-why-gap-inside-organizations", "The Why Gap Inside Organizations", "Execution slows when the purpose behind work gets lost.", "Why Gap", 8),
                SeedEssay("why-large-organizations-move-slowly", "Why Large Organizations Move Slowly", "Scale creates friction that leaders often underestimate.", "Speed", 9),
                SeedEssay("why-leaders-centralize-decisions", "Why Leaders Centralize Decisions", "Control rises as uncertainty rises.", "Hierarchy", 7),
                SeedEssay("why-good-managers-get-trapped-in-bad-systems", "Why Good Managers Get Trapped in Bad Systems", "Competence alone cannot fix structural drag.", "Systems", 9),
                SeedEssay("how-standards-erode-quietly", "How Standards Erode Quietly", "Small exceptions compound into systemic decline.", "Standards", 6)
            );
        }

        if (!await _db.FrameworkModels.AnyAsync())
        {
            _db.FrameworkModels.AddRange(
                SeedModel("the-why-gap", "The Why Gap", "A model for diagnosing meaning loss between strategy and execution."),
                SeedModel("the-friction-map", "The Friction Map", "A structured map of delays, approvals, and bottlenecks."),
                SeedModel("the-visibility-trap", "The Visibility Trap", "When metrics increase visibility but reduce truth."),
                SeedModel("quiet-erosion", "Quiet Erosion", "Why standards decay without dramatic failure.")
            );
        }

        if (!await _db.Tools.AnyAsync())
        {
            _db.Tools.AddRange(
                SeedTool("why-gap-check", "Why Gap Check", "Diagnostic", "15 minutes"),
                SeedTool("friction-map-check", "Friction Map Check", "Workshop", "25 minutes")
            );
        }

        if (!await _db.MediaItems.AnyAsync())
        {
            _db.MediaItems.AddRange(
                SeedMedia("the-why-gap-explained", "The Why Gap Explained"),
                SeedMedia("why-large-organizations-move-slowly", "Why Large Organizations Move Slowly")
            );
        }

        if (!await _db.StaticPages.AnyAsync())
        {
            _db.StaticPages.AddRange(
                SeedPage("about", "About", "# About\n\nThis publication studies **how organizations really work** beyond formal charts and slogans."),
                SeedPage("contact", "Contact", "# Contact\n\nFor editorial inquiries, use the contact channel at your organization level."));
        }

        await _db.SaveChangesAsync();
    }

    private Essay SeedEssay(string slug, string title, string summary, string category, int readTime)
    {
        var md = $"# {title}\n\n{summary}\n\nOrganizations rarely fail for lack of ideas; they fail when incentives, hierarchy, and information collide.";
        return new Essay { Slug = slug, Title = title, Summary = summary, Category = category, ReadTimeMinutes = readTime, MarkdownBody = md, RenderedHtml = _markdown.Render(md), IsPublished = true, PublishedOnUtc = DateTime.UtcNow };
    }

    private FrameworkModel SeedModel(string slug, string title, string summary)
    {
        var md = $"# {title}\n\n{summary}\n\nUse this model to analyze decisions, delay, and execution constraints.";
        return new FrameworkModel { Slug = slug, Title = title, Summary = summary, MarkdownBody = md, RenderedHtml = _markdown.Render(md), IsPublished = true, PublishedOnUtc = DateTime.UtcNow };
    }

    private Tool SeedTool(string slug, string title, string type, string duration)
    {
        var md = $"# {title}\n\nA practical tool for middle and senior managers.";
        return new Tool { Slug = slug, Title = title, Summary = "Structured assessment for managers.", ToolType = type, EstimatedDuration = duration, MarkdownBody = md, RenderedHtml = _markdown.Render(md), IsPublished = true, PublishedOnUtc = DateTime.UtcNow };
    }

    private MediaItem SeedMedia(string slug, string title)
    {
        var md = $"# {title}\n\nShort-form explanation of organizational behavior.";
        return new MediaItem { Slug = slug, Title = title, Summary = "Editorial media brief.", MediaType = "video", MarkdownBody = md, RenderedHtml = _markdown.Render(md), IsPublished = true, PublishedOnUtc = DateTime.UtcNow };
    }

    private StaticPage SeedPage(string slug, string title, string markdown)
    {
        return new StaticPage { Slug = slug, Title = title, MarkdownBody = markdown, RenderedHtml = _markdown.Render(markdown), IsPublished = true, PublishedOnUtc = DateTime.UtcNow };
    }
}
