# Atelier Vertical — ASP.NET Core MVC Frontend

A production-ready ASP.NET Core MVC frontend that recreates an editorial monochrome portfolio language with disciplined layout rhythm, oversized typography, metadata accents, and refined motion.

## Stack
- ASP.NET Core MVC (.NET 8)
- Razor Views + Partial Views
- Plain CSS
- Vanilla JavaScript

## Project Structure

```text
/Controllers
    HomeController.cs
/Models
    ArchiveModule.cs
    EssayItem.cs
    FeaturedWorkItem.cs
/ViewModels
    HomePageViewModel.cs
/Views
    /Home
        Index.cshtml
    /Shared
        _Layout.cshtml
        _Header.cshtml
        _Hero.cshtml
        _Manifesto.cshtml
        _FeaturedWorks.cshtml
        _ConceptSection.cshtml
        _ArchiveIndex.cshtml
        _AboutSection.cshtml
        _ThoughtsSection.cshtml
        _ContactSection.cshtml
        _Footer.cshtml
    _ViewImports.cshtml
    _ViewStart.cshtml
/wwwroot
    /css
        site.css
    /js
        site.js
    /img
        placeholder-1.svg
        placeholder-2.svg
        placeholder-3.svg
        placeholder-4.svg
Program.cs
manpreetsingh.pro.csproj
```

## Run locally

```bash
dotnet restore
dotnet run
```

Then open `http://localhost:5000` or the URL printed by Kestrel.

## Notes
- Homepage content is powered through `HomePageViewModel` and hardcoded in `HomeController` for easy migration to a CMS/database.
- Includes accessibility basics: semantic sections, alt text, keyboard-focus styling, skip link, and contrast-conscious monochrome theme.
- Animations are handled with IntersectionObserver-based reveal effects and tuned hover transitions.

## CI/CD
- `ci` workflow runs on pushes to `main`, pull requests targeting `main`, and manual dispatch. It restores dependencies, builds in Release, and runs tests in Release when `*.Tests.csproj` projects are present.
- `release` workflow runs on version tags matching `v*` and manual dispatch. It restores, builds, and publishes the MVC app to `artifacts/publish`, then uploads that publish directory as a GitHub Actions artifact.
- To create a release artifact from git tags:

```bash
git tag v0.1.0
git push origin v0.1.0
```

