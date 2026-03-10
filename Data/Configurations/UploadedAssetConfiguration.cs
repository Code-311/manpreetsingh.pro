using manpreetsingh.pro.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace manpreetsingh.pro.Data.Configurations;

public class UploadedAssetConfiguration : IEntityTypeConfiguration<UploadedAsset>
{
    public void Configure(EntityTypeBuilder<UploadedAsset> builder)
    {
        builder.Property(x => x.FileName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.ContentType).HasMaxLength(120).IsRequired();
        builder.Property(x => x.AssetKind).HasMaxLength(40).IsRequired();
    }
}
