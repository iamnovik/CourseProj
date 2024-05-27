using CourseProj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseProj.Data.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> entity)
    {

        entity
            .HasOne(l =>l.Item)
            .WithMany(i => i.Comments)
            .HasForeignKey(ia => ia.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        entity
            .HasOne(ia => ia.AppUser)
            .WithMany(a => a.Comments)
            .HasForeignKey(ia => ia.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity
            .HasGeneratedTsVectorColumn(
                c => c.SearchVector,
                "english",
                c => new { c.Text })
            .HasIndex(c => c.SearchVector)
            .HasMethod("GIN");
    }
}