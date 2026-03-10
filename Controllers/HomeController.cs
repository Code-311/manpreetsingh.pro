using manpreetsingh.pro.Models;
using manpreetsingh.pro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace manpreetsingh.pro.Controllers;

public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        var model = new HomePageViewModel
        {
            HeroTitle = "HOW ORGANIZATIONS REALLY WORK",
            HeroSubtext = "Understanding the forces behind decisions, hierarchy, delay, and execution.",
            WhyGapIntro = "Most organizational behavior looks personal from the outside but structural from the inside.",
            WhyGapPoints =
            [
                "Leaders often act inside constraints they rarely state out loud.",
                "Managers absorb the consequences without visibility into those constraints.",
                "That distance between decision logic and lived reality is the Why Gap."
            ],
            FeaturedModels = GetModels(),
            SelectedWritings = GetWritings(),
            Tools = GetTools(),
            MediaItems = GetMedia(),
            AboutSummary = "Manpreet Singh writes and teaches about how real systems shape real behavior inside complex organizations.",
            ContactSummary = "For speaking, advisory work, and private workshops focused on operating reality, reach out directly."
        };

        return View(model);
    }

    [HttpGet("/writings")]
    public IActionResult Writings() => View(new WritingsPageViewModel
    {
        Intro = "Essays for middle and senior managers trying to make sense of pressure, delay, and institutional behavior.",
        Items = GetWritings()
    });

    [HttpGet("/writings/{slug}")]
    public IActionResult Writing(string slug)
    {
        var item = GetWritings().FirstOrDefault(x => x.Slug == slug);
        if (item is null)
        {
            return NotFound();
        }

        return View("ContentDetail", new ContentDetailViewModel
        {
            SectionLabel = "Writing",
            Title = item.Title,
            Summary = item.Summary,
            Points =
            [
                "Where decision rights actually sit is usually different from the org chart.",
                "Most slowdowns are produced by risk routing, not individual resistance.",
                "Repair starts with naming the system pressure before naming the people."
            ]
        });
    }

    [HttpGet("/models")]
    public IActionResult Models() => View(new ModelsPageViewModel
    {
        Intro = "Simple explanatory models for diagnosing recurring organizational patterns.",
        Items = GetModels()
    });

    [HttpGet("/models/{slug}")]
    public IActionResult Model(string slug)
    {
        var item = GetModels().FirstOrDefault(x => x.Slug == slug);
        if (item is null)
        {
            return NotFound();
        }

        return View("ContentDetail", new ContentDetailViewModel
        {
            SectionLabel = "Model",
            Title = item.Name,
            Summary = item.Summary,
            Points = [item.CoreQuestion, "Use this model before prescribing behavior changes."]
        });
    }

    [HttpGet("/tools")]
    public IActionResult Tools() => View(new ToolsPageViewModel
    {
        Intro = "Practical checks to apply the models in real operating contexts.",
        Items = GetTools()
    });

    [HttpGet("/tools/{slug}")]
    public IActionResult Tool(string slug)
    {
        var item = GetTools().FirstOrDefault(x => x.Slug == slug);
        if (item is null)
        {
            return NotFound();
        }

        return View("ContentDetail", new ContentDetailViewModel
        {
            SectionLabel = "Tool",
            Title = item.Name,
            Summary = item.Summary,
            Points =
            [
                "Use in team reviews, operating retros, or planning sessions.",
                "Focus on constraints, sequencing, and decision visibility."
            ]
        });
    }

    [HttpGet("/media")]
    public IActionResult Media() => View(new MediaPageViewModel
    {
        Intro = "Short talks and video explanations for teams that need shared language quickly.",
        Items = GetMedia()
    });

    [HttpGet("/about")]
    public IActionResult About() => View(new SimplePageViewModel
    {
        Title = "About",
        Intro = "This platform exists to explain why complex organizations behave the way they do.",
        Details =
        [
            "Audience: middle and senior managers.",
            "Method: plain language, structural analysis, practical models.",
            "Tone: calm, direct, non-jargon."
        ]
    });

    [HttpGet("/contact")]
    public IActionResult Contact() => View(new SimplePageViewModel
    {
        Title = "Contact",
        Intro = "For speaking, advisory work, and workshops.",
        Details =
        [
            "Email: hello@manpreetsingh.pro",
            "Location: Global / Remote",
            "Response window: 3–5 business days"
        ]
    });

    private static List<ModelItem> GetModels() =>
    [
        new() { Name = "The Why Gap", Slug = "the-why-gap", Summary = "Explains the distance between visible decisions and invisible constraints.", CoreQuestion = "What constraint is driving this decision that frontline managers cannot see?" },
        new() { Name = "The Friction Map", Slug = "the-friction-map", Summary = "Surfaces where work slows, who absorbs cost, and where accountability breaks.", CoreQuestion = "Where does work stall repeatedly, and who carries the hidden load?" },
        new() { Name = "The Visibility Trap", Slug = "the-visibility-trap", Summary = "Shows how reporting systems reward legibility over reality.", CoreQuestion = "What appears healthy in dashboards but fails in daily execution?" },
        new() { Name = "Quiet Erosion", Slug = "quiet-erosion", Summary = "Describes how standards decay gradually under sustained pressure.", CoreQuestion = "Which standard is being quietly traded away to keep throughput?" }
    ];

    private static List<WritingItem> GetWritings() =>
    [
        new() { Title = "The Why Gap Inside Organizations", Slug = "the-why-gap-inside-organizations", Summary = "Why decision logic becomes opaque as hierarchy grows.", PublishedOn = "2026-02-10", ReadTime = "8 min" },
        new() { Title = "Why Large Organizations Move Slowly", Slug = "why-large-organizations-move-slowly", Summary = "Delay is usually a design property, not a motivation problem.", PublishedOn = "2026-01-21", ReadTime = "7 min" },
        new() { Title = "Why Leaders Centralize Decisions", Slug = "why-leaders-centralize-decisions", Summary = "Centralization often signals risk concentration, not ego.", PublishedOn = "2025-12-18", ReadTime = "6 min" },
        new() { Title = "Why Good Managers Get Trapped in Bad Systems", Slug = "why-good-managers-get-trapped-in-bad-systems", Summary = "Competent people still fail in misaligned structures.", PublishedOn = "2025-11-09", ReadTime = "9 min" },
        new() { Title = "How Standards Erode Quietly", Slug = "how-standards-erode-quietly", Summary = "How small exceptions normalize under pressure.", PublishedOn = "2025-10-03", ReadTime = "5 min" }
    ];

    private static List<ToolItem> GetTools() =>
    [
        new() { Name = "Why Gap Check", Slug = "why-gap-check", Summary = "A guided prompt set to expose hidden decision constraints." },
        new() { Name = "Friction Map Check", Slug = "friction-map-check", Summary = "A simple mapping exercise for recurring delays and handoff failures." }
    ];

    private static List<MediaItem> GetMedia() =>
    [
        new() { Title = "Why organizations stall even when everyone is busy", Format = "Talk", Duration = "22 min", Summary = "A practical explanation of delay mechanics across layers." },
        new() { Title = "The Why Gap in executive communication", Format = "Video", Duration = "14 min", Summary = "How language hides constraints and distorts alignment." }
    ];
}
