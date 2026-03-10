using manpreetsingh.pro.Models;
using manpreetsingh.pro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace manpreetsingh.pro.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var model = new HomePageViewModel
        {
            SiteTitle = "Atelier Vertical",
            HeroKicker = "Independent Practice · Edition 05",
            HeroStatement = "SPACES, SYSTEMS, AND STORIES SHAPED IN MONOCHROME.",
            HeroSubline = "An editorial portfolio for digital objects, identity frameworks, and interaction direction.",
            ManifestoTitle = "Manifesto",
            ManifestoLines =
            [
                "Design should frame attention before it asks for emotion.",
                "Clarity is constructed through rhythm: margin, measure, and motion.",
                "Every interface is an argument for what matters most.",
                "Restraint is not absence; restraint is precision."
            ],
            FeaturedWorks =
            [
                new FeaturedWorkItem
                {
                    Title = "Signal Residence",
                    Category = "Digital Direction",
                    Year = "2026",
                    Description = "Identity-led site architecture for a boutique architecture collective.",
                    ImageUrl = "/img/placeholder-1.svg",
                    AltText = "Monochrome abstract image for Signal Residence project"
                },
                new FeaturedWorkItem
                {
                    Title = "Field Notes Archive",
                    Category = "Editorial Platform",
                    Year = "2025",
                    Description = "A modular publication system for long-form writing and visual notes.",
                    ImageUrl = "/img/placeholder-2.svg",
                    AltText = "Monochrome abstract image for Field Notes Archive project"
                },
                new FeaturedWorkItem
                {
                    Title = "Frame Utility Co.",
                    Category = "Brand Experience",
                    Year = "2025",
                    Description = "Product storytelling and conversion narratives for a technical goods label.",
                    ImageUrl = "/img/placeholder-3.svg",
                    AltText = "Monochrome abstract image for Frame Utility project"
                }
            ],
            ArchiveModules =
            [
                new ArchiveModule { Code = "A-01", Label = "Type Specimen Site", Discipline = "Typography", Year = "2024", Status = "Published" },
                new ArchiveModule { Code = "A-02", Label = "Spatial Narrative Deck", Discipline = "Strategy", Year = "2024", Status = "Commissioned" },
                new ArchiveModule { Code = "A-03", Label = "Product Catalog System", Discipline = "Commerce", Year = "2023", Status = "Published" },
                new ArchiveModule { Code = "A-04", Label = "Studio Journal Engine", Discipline = "Editorial", Year = "2023", Status = "Archived" }
            ],
            AboutTitle = "About",
            AboutBody = "Atelier Vertical is an independent frontend and interaction studio exploring disciplined visual systems for contemporary brands. Work spans strategy, editorial direction, interface implementation, and motion choreography.",
            Capabilities = ["Interface Art Direction", "Design Systems", "Razor Frontend Architecture", "Motion Prototyping"],
            Essays =
            [
                new EssayItem { Title = "On Designing Quiet Interfaces", Date = "Jan 14, 2026", ReadTime = "06 min", Category = "Thoughts" },
                new EssayItem { Title = "Metadata as Narrative", Date = "Dec 08, 2025", ReadTime = "04 min", Category = "Writing" },
                new EssayItem { Title = "The Discipline of a Grid", Date = "Oct 22, 2025", ReadTime = "08 min", Category = "Lecture" }
            ],
            ContactEmail = "studio@atelier-vertical.example",
            ContactPhone = "+1 (555) 102-4477",
            ContactLocation = "Berlin · Remote"
        };

        return View(model);
    }
}
