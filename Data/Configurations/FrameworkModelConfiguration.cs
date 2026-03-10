using manpreetsingh.pro.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace manpreetsingh.pro.Data.Configurations;

public class FrameworkModelConfiguration : IEntityTypeConfiguration<FrameworkModel>
{
    public void Configure(EntityTypeBuilder<FrameworkModel> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Summary).HasMaxLength(400).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => new { x.IsPublished, x.PublishedOnUtc });
    }
}
