using manpreetsingh.pro.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace manpreetsingh.pro.Data.Configurations;

public class ToolConfiguration : IEntityTypeConfiguration<Tool>
{
    public void Configure(EntityTypeBuilder<Tool> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(200).IsRequired();
        builder.Property(x => x.ToolType).HasMaxLength(100);
        builder.Property(x => x.EstimatedDuration).HasMaxLength(60);
        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => new { x.IsPublished, x.PublishedOnUtc });
    }
}
