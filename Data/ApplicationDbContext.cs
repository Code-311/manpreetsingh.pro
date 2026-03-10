using manpreetsingh.pro.Models.Domain;
using manpreetsingh.pro.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace manpreetsingh.pro.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Essay> Essays => Set<Essay>();
    public DbSet<FrameworkModel> FrameworkModels => Set<FrameworkModel>();
    public DbSet<Tool> Tools => Set<Tool>();
    public DbSet<ToolQuestion> ToolQuestions => Set<ToolQuestion>();
    public DbSet<MediaItem> MediaItems => Set<MediaItem>();
    public DbSet<UploadedAsset> UploadedAssets => Set<UploadedAsset>();
    public DbSet<StaticPage> StaticPages => Set<StaticPage>();
    public DbSet<SiteSetting> SiteSettings => Set<SiteSetting>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
