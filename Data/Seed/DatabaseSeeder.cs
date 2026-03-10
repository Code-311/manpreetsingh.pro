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

    public DatabaseSeeder(
        ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IMarkdownRenderer markdown)
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
        {
            await _roleManager.CreateAsync(new IdentityRole(AdminRole));
        }

        var admin = await _userManager.FindByNameAsync("admin");
        if (admin is null)
        {
            var password = _configuration["Seed:AdminPassword"]
                           ?? throw new InvalidOperationException("Seed:AdminPassword not configured");

            admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@manpreetsingh.pro",
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(admin, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(";", result.Errors.Select(e => e.Description)));
            }
        }

        if (!await _userManager.IsInRoleAsync(admin, AdminRole))
        {
            await _userManager.AddToRoleAsync(admin, AdminRole);
        }

        if (!await _db.Essays.AnyAsync())
        {
            var essays = new[]
            {
                SeedEssay(
                    "the-why-gap-inside-organizations",
                    "The Why Gap Inside Organizations",
                    "Execution slows when teams cannot explain why a decision was made.",
                    "Decision Flow",
                    8,
                    "The Why Gap appears when strategic intent is not translated into local meaning. Managers inherit targets but not rationale, so teams follow process while losing purpose."),
                SeedEssay(
                    "why-large-organizations-move-slowly",
                    "Why Large Organizations Move Slowly",
                    "Scale introduces layers of review that quietly compound delay.",
                    "Execution",
                    9,
                    "Large systems optimize for risk control, not speed. Every handoff protects someone, but each protection adds wait time. Delay is often a design outcome, not a staffing problem."),
                SeedEssay(
                    "why-leaders-centralize-decisions",
                    "Why Leaders Centralize Decisions",
                    "When uncertainty rises, leaders pull choices upward.",
                    "Hierarchy",
                    7,
                    "Centralization is rarely about ego alone. It is a response to unclear standards and fragmented accountability. The cost is slower learning at the edge."),
                SeedEssay(
                    "why-good-managers-get-trapped-in-bad-systems",
                    "Why Good Managers Get Trapped in Bad Systems",
                    "Strong individual judgment cannot offset weak system design.",
                    "Management",
                    8,
                    "Good managers patch issues daily, but patches can hide structural faults. Without authority to simplify process, capable people become maintainers of friction."),
                SeedEssay(
                    "how-standards-erode-quietly",
                    "How Standards Erode Quietly",
                    "Standards collapse through small exceptions, not dramatic events.",
                    "Operations",
                    6,
                    "Most declines start as pragmatic shortcuts. Over time, exceptions become norms and quality drifts. Recovery requires visible rules and consistent enforcement.")
            };

            _db.Essays.AddRange(essays);
        }

        if (!await _db.FrameworkModels.AnyAsync())
        {
            _db.FrameworkModels.AddRange(
                SeedModel("the-why-gap", "The Why Gap", "A map of where intent gets diluted between executive decision and frontline action."),
                SeedModel("the-friction-map", "The Friction Map", "A simple way to trace delay across approvals, handoffs, and dependencies."),
                SeedModel("the-visibility-trap", "The Visibility Trap", "How reporting systems can increase activity data while reducing operational truth."),
                SeedModel("quiet-erosion", "Quiet Erosion", "A framework for spotting gradual decline in standards before results collapse.")
            );
        }

        if (!await _db.Tools.AnyAsync())
        {
            var whyGap = SeedTool("why-gap-check", "Why Gap Check", "Diagnostic", "15 minutes", "Fast prompt set for assessing whether teams understand intent, tradeoffs, and constraints.");
            var frictionMap = SeedTool("friction-map-check", "Friction Map Check", "Workshop", "25 minutes", "Structured review to identify where work waits, loops, or dies in review cycles.");

            _db.Tools.AddRange(whyGap, frictionMap);
            await _db.SaveChangesAsync();

            if (!await _db.ToolQuestions.AnyAsync())
            {
                _db.ToolQuestions.AddRange(
                    new ToolQuestion { ToolId = whyGap.Id, Prompt = "Can frontline teams explain why this priority exists?", MinScore = 1, MaxScore = 5, SortOrder = 1 },
                    new ToolQuestion { ToolId = whyGap.Id, Prompt = "Are tradeoffs explicit when deadlines conflict?", MinScore = 1, MaxScore = 5, SortOrder = 2 },
                    new ToolQuestion { ToolId = frictionMap.Id, Prompt = "Where does work wait more than two business days?", MinScore = 1, MaxScore = 5, SortOrder = 1 },
                    new ToolQuestion { ToolId = frictionMap.Id, Prompt = "Which approval step adds risk reduction versus routine delay?", MinScore = 1, MaxScore = 5, SortOrder = 2 }
                );
            }
        }

        if (!await _db.MediaItems.AnyAsync())
        {
            _db.MediaItems.AddRange(
                SeedMedia("the-why-gap-explained", "The Why Gap Explained", "Short briefing on why strategy loses meaning as it moves through hierarchy."),
                SeedMedia("why-large-organizations-move-slowly", "Why Large Organizations Move Slowly", "A concise walkthrough of review layers, delay loops, and hidden queue time."),
                SeedMedia("why-leaders-lose-visibility", "Why Leaders Lose Visibility", "Explains how polished reporting can hide operational reality.")
            );
        }

        if (!await _db.StaticPages.AnyAsync())
        {
            _db.StaticPages.AddRange(
                SeedPage(
                    "about",
                    "About",
                    "This site examines **how organizations really work**.\n\nIt focuses on the gap between formal design and daily execution: what gets delayed, who decides, and why outcomes drift.\n\nThe audience is middle and senior managers responsible for real delivery."),
                SeedPage(
                    "contact",
                    "Contact",
                    "For editorial requests, questions, or speaking inquiries, use your preferred channel and reference the topic clearly.\n\nPlease include organization context, decision level, and timeline constraints so responses can stay practical.")
            );
        }

        await _db.SaveChangesAsync();
    }

    private Essay SeedEssay(string slug, string title, string summary, string category, int readTimeMinutes, string body)
    {
        var markdown = $"{summary}\n\n{body}\n\nManagers do not need more slogans. They need clearer mechanisms for decision quality and execution speed.";
        return new Essay
        {
            Slug = slug,
            Title = title,
            Summary = summary,
            Category = category,
            ReadTimeMinutes = readTimeMinutes,
            MarkdownBody = markdown,
            RenderedHtml = _markdown.Render(markdown),
            IsPublished = true,
            PublishedOnUtc = DateTime.UtcNow,
            SortOrder = 0
        };
    }

    private FrameworkModel SeedModel(string slug, string title, string summary)
    {
        var markdown = $"{summary}\n\nUse this model to support diagnosis first, intervention second.";
        return new FrameworkModel
        {
            Slug = slug,
            Title = title,
            Summary = summary,
            MarkdownBody = markdown,
            RenderedHtml = _markdown.Render(markdown),
            IsPublished = true,
            PublishedOnUtc = DateTime.UtcNow,
            SortOrder = 0
        };
    }

    private Tool SeedTool(string slug, string title, string toolType, string duration, string summary)
    {
        var markdown = $"{summary}\n\nRun this with your team and document disagreements before action planning.";
        return new Tool
        {
            Slug = slug,
            Title = title,
            Summary = summary,
            ToolType = toolType,
            EstimatedDuration = duration,
            MarkdownBody = markdown,
            RenderedHtml = _markdown.Render(markdown),
            IsPublished = true,
            PublishedOnUtc = DateTime.UtcNow,
            SortOrder = 0
        };
    }

    private MediaItem SeedMedia(string slug, string title, string summary)
    {
        var markdown = $"{summary}\n\nDesigned for leaders who need a quick, direct framing before deeper reading.";
        return new MediaItem
        {
            Slug = slug,
            Title = title,
            Summary = summary,
            MediaType = "video",
            MarkdownBody = markdown,
            RenderedHtml = _markdown.Render(markdown),
            IsPublished = true,
            PublishedOnUtc = DateTime.UtcNow,
            SortOrder = 0
        };
    }

    private StaticPage SeedPage(string slug, string title, string markdown)
    {
        return new StaticPage
        {
            Slug = slug,
            Title = title,
            MarkdownBody = markdown,
            RenderedHtml = _markdown.Render(markdown),
            IsPublished = true,
            PublishedOnUtc = DateTime.UtcNow
        };
    }
}
