# Manpreet Singh — How Organizations Really Work

An ASP.NET Core MVC (.NET 8) website focused on explaining why complex organizations behave the way they do.

## Stack
- ASP.NET Core MVC + Razor Views
- Plain CSS (design tokens + responsive layout)
- Vanilla JavaScript (theme/accent controls + restrained reveal motion)

## Sitemap
- /
- /writings
- /writings/{slug}
- /models
- /models/{slug}
- /tools
- /tools/{slug}
- /media
- /about
- /contact

## Run locally
```bash
dotnet restore
dotnet run
```

## UI behavior
- System color mode is respected by default (`prefers-color-scheme`).
- Users can override light/dark mode in the header; preference is saved in `localStorage`.
- Users can switch among curated accent colors; preference is saved in `localStorage`.
- Motion is subtle and respects `prefers-reduced-motion`.

## CI/CD
- `ci` workflow runs on pushes to `main`, pull requests to `main`, and manual dispatch.
- `release` workflow runs on `v*` tags and manual dispatch, publishes to `artifacts/publish`, and uploads that directory as the release artifact.
- To trigger a tagged release artifact:

```bash
git tag v0.1.0
git push origin v0.1.0
```
