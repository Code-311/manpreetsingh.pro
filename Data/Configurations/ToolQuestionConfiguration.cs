using manpreetsingh.pro.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace manpreetsingh.pro.Data.Configurations;

public class ToolQuestionConfiguration : IEntityTypeConfiguration<ToolQuestion>
{
    public void Configure(EntityTypeBuilder<ToolQuestion> builder)
    {
        builder.Property(x => x.Prompt).HasMaxLength(500).IsRequired();
        builder.HasOne(x => x.Tool).WithMany(x => x.Questions).HasForeignKey(x => x.ToolId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new { x.ToolId, x.SortOrder });
    }
}
